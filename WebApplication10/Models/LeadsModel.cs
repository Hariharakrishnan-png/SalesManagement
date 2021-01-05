using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication10.Models
{
    public class LeadsModel
    {
        [Display(Name = "LeadId")]
        public long LeadId { get; set; }

        [Display(Name = "Profile Image")]
        [Required(ErrorMessage = " Photo is Required")]
        public string Photo { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = " First Name is Required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = " Last Name is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "DOB is Required ")]
        [Display(Name = "DateOfBirth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = " Gender is Required")]
        public string Gender { get; set; }

        [Display(Name = "Current Address")]
        [Required(ErrorMessage = " Current Address is Required")]
        public string CurrentAddress { get; set; }

        [Display(Name = "Permanent Address")]
        [Required(ErrorMessage = " Permanent Address is Required")]
        public string PermanentAddress { get; set; }

        [Display(Name = "Mobile Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public long MobileNumber { get; set; }

        [Display(Name = "Email Id")]
        [Required(ErrorMessage = " EmailId is Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string EmailId { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = " City is Required")]
        public string City { get; set; }

        [Display(Name = " State")]
        [Required(ErrorMessage = " State is Required")]
        public string State { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = " Country is Required")]
        public string Country { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }

        [Display(Name = "Lead Source")]
        [Required(ErrorMessage = "LeadSource is Required")]
        public string LeadSource { get; set; }

        [Display(Name = "Meeting Date")]
        [Required(ErrorMessage = "MeetingDate is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MeetingDate { get; set; }

        public List<LeadSources> LeadSourceList { get; internal set; }
        public List<Cities> CityList { get; internal set; }
        public List<States> StateList { get; set; }
        public List<Countries> CountryList { get; internal set; }
    }

    public class LeadSources
    {
        public int Id { get; set; }

        public string LeadSource { get; set; }

    }

    public class Cities
    {
        public long Id { get; set; }

        public string CityName { get; set; }

        public long StateId { get; set; }
    }

    public class States
    {
        public long Id { get; set; }

        public string StateName { get; set; }

        public long CountryId { get; set; }
    }

    public class Countries
    {
        public long Id { get; set; }

        public string CountryName { get; set; }

    }
}