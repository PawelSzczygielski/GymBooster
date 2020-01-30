﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GymBooster.Api.Models;

namespace CarvedRock.Api.Data
{
    public class ProductRepository
    {
        private readonly CarvedRockDbContext _dbContext;

        public ProductRepository(CarvedRockDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Product>> GetAll()
        {
            return new Task<List<Product>>(() => _dbContext.Products);
        }
    }
}