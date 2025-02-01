﻿

namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price); 
    
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products"
                , async (CreateProductRequest request,ISender sender) =>
            {
                //Business Logic to create a product
                //save to database 
                //return result
                var command = request.Adapt<CreateProductCommand>();
                var result=await  sender.Send(command);
                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/products/{response.Id}", response);

            })
             .WithName("CreateProduct")
             .Produces<CreateProductResponse>(StatusCodes.Status201Created)
             .Produces(StatusCodes.Status400BadRequest)
             .WithSummary("Create a new product")
             .WithDescription("Create a new product in the catalog"); 
        }
    }
}
