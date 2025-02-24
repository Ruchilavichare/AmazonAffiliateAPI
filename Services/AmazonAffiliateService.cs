using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AmazonAffiliateAPI.Models;
using AmazonAffiliateAPI.Utils;
using Microsoft.EntityFrameworkCore;
using AmazonAffiliateAPI.Data;

namespace AmazonAffiliateAPI.Services
{
    public class AmazonAffiliateService
    {
        private readonly AmazonApiHelper _apiHelper;
        private readonly ApplicationDbContext _context;

        public AmazonAffiliateService(ApplicationDbContext context)
        {
            _context = context;
        }

        public AmazonAffiliateService(AmazonApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<AmazonProduct>> FetchBestSellingToysAsync()
        {
            string jsonResponse = await _apiHelper.GetAmazonProductsAsync("Toys");
            return jsonResponse != null ? JsonConvert.DeserializeObject<List<AmazonProduct>>(jsonResponse) : new List<AmazonProduct>();
        }

        public async Task SaveProductsToDatabase(List<AmazonProduct> products)
        {
            foreach (var product in products)
            {
                var existingProduct = await _context.AmazonProducts
                    .FirstOrDefaultAsync(p => p.ASIN == product.ASIN);

                if (existingProduct != null)
                {
                    existingProduct.Title = product.Title;
                    existingProduct.Price = product.Price;
                    existingProduct.ImageUrl = product.ImageUrl;
                    existingProduct.LastUpdated = DateTime.UtcNow;
                }
                else
                {
                    _context.AmazonProducts.Add(product);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
