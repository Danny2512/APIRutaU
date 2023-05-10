using System.ComponentModel.DataAnnotations;

namespace APIRutaU.Models
{
    public class ValidateOTPViewModel
    {
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string Cod { get; set; }
    }
}
