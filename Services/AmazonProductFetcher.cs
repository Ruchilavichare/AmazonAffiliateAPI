using AmazonAffiliateAPI.Models;
using AmazonAffiliateAPI.Services;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AmazonAffiliateAPI.Services
{
    public class AmazonProductFetcher
    {
        private readonly HttpClient _httpClient;
        private readonly AmazonAffiliateService _amazonAffiliateService;

        public AmazonProductFetcher(HttpClient httpClient, AmazonAffiliateService amazonAffiliateService)
        {
            _httpClient = httpClient;
            _amazonAffiliateService = amazonAffiliateService;
        }

        public async Task FetchDailyBestSellers()
        {
            string apiUrl = "https://api.amazon.com/best-sellers"; // Replace with actual Amazon API URL
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_ACCESS_TOKEN");

            var response = await _httpClient.GetStringAsync(apiUrl);
            var products = JsonConvert.DeserializeObject<List<AmazonProduct>>(response);

            if (products != null)
            {
                await _amazonAffiliateService.SaveProductsToDatabase(products);
            }
        }
    }
}
