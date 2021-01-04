using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication10.Models
{
    public class UserRoleDetailsModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string AadharNumber { get; set; }
        public string FathersName { get; set; }
        public string MaritalStatus { get; set; }
        [Required]
        public string NewPassword { get; set; }

        [NotMapped] // Does not effect with your database
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
        public string EmailId { get; set; }

    }
}