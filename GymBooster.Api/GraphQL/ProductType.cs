using CarvedRock.Api.Data;
using GraphQL.DataLoader;
using GraphQL.Types;
using GymBooster.Api.Models;

namespace GymBooster.Api.GraphQL
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(ProductReviewRepository reviewRepository, IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            Field(t => t.Id);
            Field(t => t.Name);
            Field(t => t.Description);
            Field(t => t.IntroducedAt).Description("When the product was first introduced in the catalog");
            Field(t => t.PhotoFileName).Description("The file name of the photo so the client can render it");
            Field(t => t.Price);
            Field(t => t.Rating).Description("The (max 5) star customer rating");
            Field(t => t.Stock);
            Field<ProductTypesType>();

            Field<ListGraphType<ProductReviewType>>("reviews",
                resolve: context =>
                {
                    var loader =
                        dataLoaderContextAccessor.Context.GetOrAddCollectionBatchLoader<int, ProductReview>(
                            "GetReviewsByProductId", reviewRepository.GetForProducts);
                    return loader.LoadAsync(context.Source.Id);

                });
        }
    }
}