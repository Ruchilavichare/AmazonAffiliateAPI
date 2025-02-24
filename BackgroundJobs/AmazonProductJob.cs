using System;
using System.Threading.Tasks;
using Hangfire;
using AmazonAffiliateAPI.Services;

namespace AmazonAffiliateAPI.BackgroundJobs
{
    public class AmazonProductJob
    {
        private readonly AmazonAffiliateService _amazonService;

        public AmazonProductJob(AmazonAffiliateService amazonService)
        {
            _amazonService = amazonService;
        }

        [AutomaticRetry(Attempts = 3)]
        public async Task FetchDailyBestSellers()
        {
            var products = await _amazonService.FetchBestSellingToysAsync();
            Console.WriteLine($"Fetched {products.Count} best-selling toys from Amazon API.");
        }
    }
}
