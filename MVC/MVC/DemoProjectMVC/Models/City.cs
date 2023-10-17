using System.ComponentModel.DataAnnotations;

namespace DemoProjectMVC.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="City Name")]
        public string CityName { get; set; }

        [Required]
        [Display(Name = "State Name")]
        public int StateId { get; set; }
        public State State { get; set; }

        public ICollection<Students> Students { get; set; }
    }
}
