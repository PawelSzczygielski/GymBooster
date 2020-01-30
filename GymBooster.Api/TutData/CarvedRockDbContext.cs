using System.Collections.Generic;
using GymBooster.Api.Models;

namespace CarvedRock.Api.Data
{
    public class CarvedRockDbContext
    {
        public CarvedRockDbContext()
        {
            Products = new List<Product>();
        }

        public List<Product> Products { get; set; }
    }
}