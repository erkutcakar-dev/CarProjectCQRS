using System.ComponentModel.DataAnnotations;

namespace CarProjectCQRS.Entities
{
    public class Distance
    {
        [Key]
        public short DistanceId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string From { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string Destination { get; set; } = string.Empty;
        
        public short DistanceValue { get; set; }
    }
}
