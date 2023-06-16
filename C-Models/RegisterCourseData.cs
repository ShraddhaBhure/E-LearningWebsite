using System;
using System.ComponentModel.DataAnnotations;

namespace C_Models
{
    public class RegisterCourseData
    {
        [Key]
        public int Courseid { get; set; }
        public string CourseName { get; set; }
        public string Educator { get; set; }
        public string CourseCategory { get; set; }
        public string Duration { get; set; }
        public decimal CourseFees { get; set; }
        public DateTime LaunchDate { get; set; }
        public string CoverImage { get; set; }
        public byte[] CoverImageData { get; set; }
        
    }
    
}
