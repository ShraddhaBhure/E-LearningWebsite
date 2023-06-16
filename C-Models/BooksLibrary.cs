using System.ComponentModel.DataAnnotations;

namespace C_Models
{
    public  class BooksLibrary
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public decimal Price { get; set; }
        public string BookCategory { get; set; }
    }
}
