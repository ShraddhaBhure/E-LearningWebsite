using System.ComponentModel.DataAnnotations;

namespace C_Models
{
    public class OnlineClass
    {

        
            [Key]
            public int ClassId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string CreatedBy { get; set; }
            public string  Duration { get; set; }
            public string CoverImage { get; set; }
            public decimal Price { get; set; }
            public string CourseCategory { get; set; }
        
    }
}
