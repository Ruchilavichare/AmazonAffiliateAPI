using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using AmazonAffiliateAPI.Models;

namespace AmazonAffiliateAPI.Utils
{
    public class AmazonApiHelper
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AmazonApiHelper(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> GetAmazonProductsAsync(string category)
        {
            string accessKey = _config["AmazonAffiliate:AccessKey"];
            string secretKey = _config["AmazonAffiliate:SecretKey"];
            string associateTag = _config["AmazonAffiliate:AssociateTag"];
            string region = _config["AmazonAffiliate:Region"];

            // Simulated API call - replace with real Amazon API request
            string apiUrl = $"https://api.example.com/amazon/{category}?accessKey={accessKey}&associateTag={associateTag}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}