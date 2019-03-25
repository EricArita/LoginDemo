using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace LoginDemo.Models
{
    public partial class Account
    {
        public Account() { }

        public Account(string username, string password, string isremembered)
        {
            this.UserName = username;
            this.Password = password;
            this.IsRemembered = isremembered == "True" ? true : false;
        }

        [Key]
        [StringLength(20)]
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Please provide Password", AllowEmptyStrings = true)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsRemembered { get; set; }
        [StringLength(20)]
        public string Note { get; set; }
    }
}
