using GraphQL.Types;
using GymBooster.Api.Models;

namespace GymBooster.Api.GraphQL
{
    public class ProductReviewType : ObjectGraphType<ProductReview>
    {
        public ProductReviewType()
        {
            Field(t => t.Id);
            Field(t => t.Title);
            Field(t => t.Review);
        }
    }
}