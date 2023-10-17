using System.ComponentModel.DataAnnotations;

namespace DemoProjectMVC.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Country Name")]
        public string CountryName { get; set; }

        [Display(Name ="Country Code")]
        public string CountryCode { get; set; }

        [Display(Name ="Flag")]
        public string CountryFlag { get; set; }

        public ICollection<State> States { get; set; }

        public ICollection<Students> Students { get; set; }
    }
}
