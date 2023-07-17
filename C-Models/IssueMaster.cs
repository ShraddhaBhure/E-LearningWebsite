using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Models
{
    public class IssueMaster
    { 
        [Key]
        public int IssueID { get; set; }
        public string IssueName { get; set; }
        public string Display1Type { get; set; }
        public string Display2Type { get; set; }
        public string Display1Title { get; set; }
        public string Display2Title { get; set; }
        public string Display1Author { get; set; }
        public string Display2Author { get; set; }
        public string IssueDescription { get; set; }
        public string Frontimage { get; set; }
        public string IpAddress { get; set; }
        public DateTime? IssueDate { get; set; }
        public string Filename { get; set; }
    }

}
