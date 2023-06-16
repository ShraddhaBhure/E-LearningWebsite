using System;
using System.ComponentModel.DataAnnotations;

namespace C_Models
{
    public class Article
    { 
        [Key]
        public int ArticleID { get; set; }
        public string ArticleType { get; set; }
        public string Title { get; set; }
        public string AuthorNames { get; set; }
        public string Abstract { get; set; }
        public string Keywords { get; set; }
        public string CoverImage { get; set; }
        public string ArticleFileName { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string ConflictOfInterest { get; set; }
        public string EthicalClearance { get; set; }
        public string RegisteredTo { get; set; }
        public string Acknowledgements { get; set; }


    }

}
