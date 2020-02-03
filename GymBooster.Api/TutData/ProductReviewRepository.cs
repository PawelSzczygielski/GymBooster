using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymBooster.Api.Models;

namespace CarvedRock.Api.Data
{
    public class ProductReviewRepository
    {
        private readonly CarvedRockDbContext _dbContext;

        public ProductReviewRepository(CarvedRockDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ProductReview> GetForProduct(int productId)
        {
            return _dbContext.ProductReviews.Where(pr => pr.ProductId == productId).ToList();
        }

        public Task<ILookup<int, ProductReview>> GetForProducts(IEnumerable<int> productIds)
        {
            IEnumerable<ProductReview> reviewsForProducts = _dbContext.ProductReviews.Where(pr => productIds.Contains(pr.ProductId));
            ILookup<int, ProductReview> reviewsByProdId = reviewsForProducts.ToLookup(pr => pr.ProductId);
            var tcs = new TaskCompletionSource<ILookup<int, ProductReview>>();
            tcs.SetResult(reviewsByProdId);
            return tcs.Task;
        }
    }
}