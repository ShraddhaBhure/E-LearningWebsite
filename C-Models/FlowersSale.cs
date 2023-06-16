using System.ComponentModel.DataAnnotations;

namespace C_Models
{
    public class FlowersSale
    {    
        [Key]
        public int FlowerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
    }
}
