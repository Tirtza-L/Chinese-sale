using System.Reflection.Metadata;

namespace Project.Models
{
    public class Gift
    {
        public int Id{ get; set; }
        public int CustomerId{ get; set; }
        public float Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public string Image { get; set; }
        public bool HasWinner { get; set; }=false;
        public Customer Customer { get; set; }
        public Category Category1 { get; set; }
        public IEnumerable<Sale> SaleList { get; set; }

    }
}
