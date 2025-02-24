namespace AmazonAffiliateAPI.Models
{
    public class AmazonProduct
    {
        public int Id { get; set; }
        public string ASIN { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsBestSeller { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
