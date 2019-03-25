using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginDemo.Models;
using LoginDemo.Context;

namespace LoginDemo.Controllers
{
    public class LoginController : Controller
    {
        /*private void ExpireAllCookies()
        {
            if (HttpContext != null)
            {
                int cookieCount = HttpContext.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Request.Cookies[i];
                    if (cookie != null)
                    {
                        var expiredCookie = new HttpCookie(cookie.Name)
                        {
                            Expires = DateTime.Now.AddDays(-1),
                            Domain = cookie.Domain
                        };
                        HttpContext.Response.Cookies.Add(expiredCookie); // overwrite it
                    }
                }

                // clear cookies server side
                HttpContext.Request.Cookies.Clear();
            }
        }
        */


        public ActionResult Login()
        {
            if (Session["StateOfLogin"] != null && Session["StateOfLogin"].ToString() == "Logged")
            {
                return RedirectToAction("AfterLogin", "Login");
            }

            Session["StateOfLogin"] = null;
            Session["LoggedUsername"] = null;

            if (Request.Cookies["UsernameCookie"] != null)
            {
                var acc = new Account(Request.Cookies["Usernamecookie"].Value, Request.Cookies["PasswordCookie"].Value, Request.Cookies["IsRememberedCookie"].Value);
                return View(acc);
                //ExpireAllCookies();
            }

            return View();
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account user)
        {
            //this action menthod is for handling POST
            using (var ctx = new LoginContext())
            {
                Account account = ctx.Accounts.Where(p => p.UserName.Equals(user.UserName) && p.Password.Equals(user.Password)).FirstOrDefault();

                if (account != null)
                {
                    if (user.IsRemembered == true)
                    {
                        Response.Cookies["UsernameCookie"].Value = user.UserName;
                        Response.Cookies["PasswordCookie"].Value = user.Password;
                        Response.Cookies["IsRememberedCookie"].Value = user.IsRemembered.ToString();
                    }
                    else
                    {
                        Response.Cookies["UsernameCookie"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["PasswordCookie"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["IsRememberedCookie"].Expires = DateTime.Now.AddDays(-1);
                    }

                    ctx.SaveChanges();
                    Session["LoggedUsername"] = user.UserName;
                    Session["StateOfLogin"] = "Logged";
                    return RedirectToAction("AfterLogin", "Login");
                }
            }

            return View(user);
        }

        public ActionResult LogoutSession()
        {
            Session["StateOfLogin"] = null;
            Session["LoggedUsername"] = null;
            return RedirectToAction("Login", "Login");
        }

        public ActionResult AfterLogin()
        {
            if (Session["LoggedUsername"] == null)
            {
                RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}