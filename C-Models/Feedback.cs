using System;
using System.ComponentModel.DataAnnotations;

namespace C_Models
{
    public class Feedback
    {
        [Key]
        public int Fid { get; set; }
        public string ClientName { get; set; }
        public string Profession { get; set; }
        public string Course { get; set; }
        public DateTime DateAdded { get; set; }
        public string Details { get; set; }
        public string ClientImage { get; set; }

    }
}
