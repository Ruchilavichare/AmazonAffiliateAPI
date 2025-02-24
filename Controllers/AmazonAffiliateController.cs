using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AmazonAffiliateAPI.Models;
using AmazonAffiliateAPI.Services;

namespace AmazonAffiliateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmazonAffiliateController : ControllerBase
    {
        private readonly AmazonAffiliateService _amazonService;

        public AmazonAffiliateController(AmazonAffiliateService amazonService)
        {
            _amazonService = amazonService;
        }

        [HttpGet("best-sellers")]
        public async Task<ActionResult<List<AmazonProduct>>> GetBestSellers()
        {
            var products = await _amazonService.FetchBestSellingToysAsync();
            return Ok(products);
        }
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("API is working!");
        }
    }
}
