namespace Project.Models
{
    public class Winner
    {
        public int Id { get; set; }
        public int GiftId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Gift Gift { get; set; }
    }
}
