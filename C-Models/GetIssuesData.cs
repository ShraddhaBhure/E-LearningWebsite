using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Models
{
    public class GetIssuesData
    {
        [NotMapped]
        public List<string> Display1Types { get; set; }
        [NotMapped] 
        public List<string> Display2Types { get; set; }
    }
}
