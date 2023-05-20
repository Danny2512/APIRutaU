using System.ComponentModel.DataAnnotations;

namespace APIRutaU.Models
{
    public class ValidateOTPViewModel
    {
        public string Email { get; set; }
        public string Cod { get; set; }
    }
}
