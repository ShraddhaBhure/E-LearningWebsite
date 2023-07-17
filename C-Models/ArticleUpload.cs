using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Models
{
  public  class ArticleUpload:GetIssuesData
    {
        [Key]
        public int ArticleID { get; set; }
        public string ArticleType { get; set; }
        public string Title { get; set; }
        public string AuthorNames { get; set; }
        public string UploadArticleFile { get; set; }
        public string RespectiveIssue { get; set; }
         public string PageNo { get; set; }
        public string ManuscriptNo { get; set; }
        public DateTime? SubmissionDate { get; set; }
      
       
    }
}
