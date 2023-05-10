using System.ComponentModel.DataAnnotations;

namespace APIRutaU.Models
{
    public class GetOTPViewModel
    {
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string Email { get; set; }
    }
}
