using System.Text.Json.Serialization;

namespace Project.Models
{
    public enum role
    {
        Manager,Donor,Custumer
    };
    public class Customer
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public role Role  { get; set; }=role.Custumer;
        [JsonIgnore]
        public IEnumerable<Sale> SaleList { get; set; }
    }
}
