using System.Collections.Generic;
using GymBooster.Api.Models;
using Microsoft.Extensions.Logging;

namespace CarvedRock.Api.Data
{
    public class CarvedRockDbContext
    {
        public CarvedRockDbContext(ILogger<CarvedRockDbContext> logger)
        {
            logger.Log(LogLevel.Critical, "ttttttttttttttttttttttttttttttttttttttttttttttttttttt");
            Products = new List<Product>();
            ProductReviews = new List<ProductReview>();
        }

        public List<Product> Products { get; set; }
        public List<ProductReview> ProductReviews { get; set; }
    }
}