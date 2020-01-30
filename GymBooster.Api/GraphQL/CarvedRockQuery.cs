using CarvedRock.Api.Data;
using GraphQL.Types;

namespace GymBooster.Api.GraphQL
{
    public class CarvedRockQuery : ObjectGraphType
    {
        public CarvedRockQuery(ProductRepository repository)
        {
            Field<ListGraphType<ProductType>>(
                "products",
                resolve: context => repository.GetAll());
        }
    }
}