using System.ComponentModel.DataAnnotations;
using static refactorme.utils.Enums;

namespace refactorme.ViewModels
{
    public class CalculateWilksModel
    {
        [Required]
        public Gender Gender { get; set; }

        [Required]
        [Range(1, 500)]
        public double BodyWeight { get; set; }

        [Required]
        [Range(1, 1000)]
        public double LiftedWeight { get; set; }
    }
}
