
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.GetProductByCategory
{

    //public record GetProductByCategoryRequest();
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}",
                async (string category, ISender sender) =>
                {
                    var result = await sender.Send(new GetProductByCategoryQuery(category));
                    var respons = result.Adapt<GetProductByCategoryResponse>();
                    return Results.Ok(respons); 
                })
                  .WithName("GetProductByCategory")
                .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .WithSummary("Get a product by Category")
                .WithDescription("Get a product by Category");
            
        }
    }
}
