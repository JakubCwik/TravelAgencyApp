using System.ComponentModel.DataAnnotations;

namespace BiuroPodrozyAPI.Models
{
    public class UpdateTravelAgencyDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
    }
}
