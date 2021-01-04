using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication10.Models
{
    public class LoginModel
    {
        public string Adminid { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        //public object Username { get; internal set; }
        public List<LoginModel> usersinfo { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        //[Required(ErrorMessage = "Please enter Email Id.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]

        public string EmailId { get; set; }
        [Required(ErrorMessage = "Please enter Password.")]
        public string UserPassword { get; set; }
        //public string To { get; set; }
        //public string Subject { get; set; }
        //public string Body { get; set; }
        //public HttpPostedFileBase Attachment { get; set; }
        //public string Email { get; set; }
        //public string MailPassword { get; set; }
        //public string MessagePasswordValidator { get; set; }
       public string Email_id { get; set; }
        public string Pass { get; set; }
        public bool IsPasswordChanged { get; set; }




    }

    
}
