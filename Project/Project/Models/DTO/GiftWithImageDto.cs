using System.Drawing;

namespace Project.Models.DTO
{
    public class GiftWithImageDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
