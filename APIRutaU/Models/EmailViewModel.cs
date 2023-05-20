using System.ComponentModel.DataAnnotations;

namespace APIRutaU.Models
{
    public class EmailViewModel
    {
        public string[] ToEmails { get; set; }
        public string[]? CcEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
