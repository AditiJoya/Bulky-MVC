using System.ComponentModel.DataAnnotations;

namespace DemoProjectMVC.Models
{
    public class Students
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = " Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Country Name")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [Required]
        [Display(Name = "State Name")]
        public int StateId { get; set; }
        public State State { get; set; }

        [Required]
        [Display(Name = "City Name")]
        public int CityId { get; set; }
        public City City { get; set; }

        [Required]
        public string Street { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        public Boolean Ssc { get; set; }
        public Boolean Hsc { get; set; }
        public Boolean Bsc { get; set; }
        public Boolean Msc { get; set; }

        public string Picture { get; set; }
        
    }
}
