using GraphQL.Types;
using GymBooster.Api.Models;

namespace GymBooster.Api.GraphQL
{
    public class ProductTypesType : EnumerationGraphType<ProductTypes>
    {
        public ProductTypesType()
        {
            Name = "Type";
            Description = "The type of product";
        }
    }
}