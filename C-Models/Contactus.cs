using System.ComponentModel.DataAnnotations;

namespace C_Models
{
    public class Contactus
    {
        [Key]
        public int Contactid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
