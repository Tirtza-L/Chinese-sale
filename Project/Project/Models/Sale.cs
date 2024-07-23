namespace Project.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int GiftId { get; set; }
        public bool Status { get; set; }=false;
        public int Count { get; set; } = 1;
        public Customer Customer { get; set; }
        public Gift Gift { get; set; }

    }
}
