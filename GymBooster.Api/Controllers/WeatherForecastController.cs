using System;
using System.Collections.Generic;
using System.Linq;
using CarvedRock.Api.Data;
using GymBooster.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GymBooster.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;
        private readonly CarvedRockDbContext _dbContext;

        public ProductsController(ILogger<ProductsController> logger, CarvedRockDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _dbContext.Products;
        }
    }
}
