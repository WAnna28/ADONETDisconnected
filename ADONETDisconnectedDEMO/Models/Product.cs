using System;

namespace ADONETDisconnectedDEMO.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime IntroductionDate { get; set; }
        public string Url { get; set; }
        public decimal Price { get; set; }
    }
}
