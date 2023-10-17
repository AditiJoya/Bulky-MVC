using System.ComponentModel.DataAnnotations;

namespace DemoProjectMVC.Models
{
    public class State
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="State Name")]
        public string StateName { get; set; }

        [Required]
        [Display(Name ="Country Name")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }
        public ICollection<Students> Students { get; set; }
    }
}
