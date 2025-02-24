using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AmazonAffiliateAPI.Data;
using AmazonAffiliateAPI.Utils;
using AmazonAffiliateAPI.Services;
using Hangfire;
using Hangfire.SqlServer;
using AmazonAffiliateAPI.BackgroundJobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Hangfire;
using Hangfire.SqlServer;
using AmazonAffiliateAPI.Services;

namespace AmazonAffiliateAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            // Add Hangfire
            builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddHangfireServer();

            builder.Services.AddHttpClient();
            builder.Services.AddScoped<AmazonApiHelper>();
            builder.Services.AddScoped<AmazonAffiliateService>();
            builder.Services.AddControllers();
            builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddHangfireServer();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Amazon Affiliate API",
                    Version = "v1",
                    Description = "API for fetching best-selling Amazon toys using automation.",
                });
            });

            // Register services
            builder.Services.AddScoped<AmazonAffiliateAPI.Services.AmazonProductFetcher>();
            builder.Services.AddScoped<AmazonAffiliateService>();

            var app = builder.Build();
            app.UseRouting();
            // Enable Swagger UI
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Amazon Affiliate API v1");
                });
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseHangfireDashboard("/hangfire");
            // Schedule daily job
            RecurringJob.AddOrUpdate<AmazonAffiliateAPI.BackgroundJobs.AmazonProductJob>(
    job => job.FetchDailyBestSellers(),
    Cron.Daily
);
            app.MapControllers();

            app.Run();
        }
    }
}
