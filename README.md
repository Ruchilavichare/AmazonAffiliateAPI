**Amazon Affiliate API Integration & Automation**

**Overview**

This project is a .NET Core Web API that integrates with the Amazon Affiliate API to fetch new and best-selling toys daily and store them in a database. It uses Entity Framework Core (EF Core) for database management and Hangfire for job automation.

**Features**

Fetch new and best-selling toys from Amazon Affiliate API

Store product data in a SQL Server database

Automate daily API calls using Hangfire

Expose REST API endpoints for retrieving stored products

Swagger UI for API testing

**🛠️ Tech Stack**

.NET 6/7 (ASP.NET Core Web API)

C#

Entity Framework Core (EF Core)

SQL Server

Hangfire (Job Scheduling)

Swagger (API Documentation)

Newtonsoft.Json (JSON Parsing)

Redis (Optional, for caching)

**🚀 Getting Started**

1️⃣ Clone the Repository

git clone https://github.com/your-repo/amazon-affiliate-api.git
cd amazon-affiliate-api

2️⃣ Install Dependencies

dotnet restore

3️⃣ Set Up Database Connection

Open appsettings.json and update the connection string:

"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=AmazonAffiliateDB;Trusted_Connection=True;Encrypt=False"
}

Apply Migrations to create the database:

dotnet ef migrations add InitialCreate
dotnet ef database update

4️⃣ Run the Application

dotnet run

This will start the API on https://localhost:7162/ (or another port specified in launchSettings.json).

5️⃣ Access Swagger UI

Open in a browser:

https://localhost:7162/swagger

📌 API Endpoints

✅ Fetch Best-Selling Toys from Amazon API

GET /api/products/fetch

Description: Manually triggers fetching of best-selling toys from the Amazon API and stores them in the database.

✅ Get All Stored Products

GET /api/products

Description: Retrieves all stored Amazon products.

✅ Get Product by ASIN

GET /api/products/{asin}

Description: Retrieves a single product by its Amazon ASIN.

🔄 Automated Task with Hangfire

This API automatically fetches best-selling toys daily using Hangfire.

You can access the Hangfire dashboard at:

http://localhost:5000/hangfire

The job is scheduled using:

RecurringJob.AddOrUpdate<AmazonProductFetcher>(job => job.FetchDailyBestSellers(), Cron.Daily);

**🏗️ Project Structure**

📦 AmazonAffiliateAPI
 ┣ 📂 Controllers
 ┃ ┗ 📜 ProductsController.cs
 ┣ 📂 Data
 ┃ ┗ 📜 ApplicationDbContext.cs
 ┣ 📂 Models
 ┃ ┗ 📜 AmazonProduct.cs
 ┣ 📂 Services
 ┃ ┣ 📜 AmazonAffiliateService.cs
 ┃ ┗ 📜 AmazonProductFetcher.cs
 ┣ 📂 BackgroundJobs
 ┃ ┗ 📜 AmazonProductJob.cs
 ┣ 📜 appsettings.json
 ┣ 📜 Program.cs
 ┗ 📜 README.md

**📌 Deployment**

To deploy this API, use Docker, Azure, or AWS Lambda.

**🐳 Run with Docker**

Build Docker image:

docker build -t amazon-affiliate-api .

Run the container:

docker run -p 8080:80 amazon-affiliate-api

**Open Swagger UI:**

http://localhost:8080/swagger
