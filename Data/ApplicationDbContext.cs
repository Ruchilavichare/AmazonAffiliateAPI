using Microsoft.EntityFrameworkCore;
using AmazonAffiliateAPI.Models;
using System.Collections.Generic;

namespace AmazonAffiliateAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AmazonProduct> AmazonProducts { get; set; }
    }
}
