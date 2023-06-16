using System;
using System.ComponentModel.DataAnnotations;

namespace C_Models
{
    public class Login 
    {
        [Key]
        public int Userid { get; set; }
        public string UserName { get; set; }
        
        public string UserPassword { get; set; }
        public string UserRole { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpiry { get; set; }
          
    }

}
