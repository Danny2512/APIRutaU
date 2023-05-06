using System.ComponentModel.DataAnnotations;

namespace APIRutaU.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string User { get; set; }
        [Required(ErrorMessage = "Es obligatorio el 0.")]
        public string Pass { get; set; }
    }
}
