using GraphQL.Types;
using GymBooster.Api.Models;

namespace GymBooster.Api.GraphQL
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType()
        {
            Field(t => t.Id);
            Field(t => t.Name);
            Field(t => t.Description);
        }
    }
}