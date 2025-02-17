﻿
using Catalog.API.Products.GetProductByCategory;

namespace Catalog.API.Products.DeleteProduct
{

    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);
    internal class DeleteProductCommandHandler
        (IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductCommandHandler.Handle called with {@Command}", command);    



            var product = session.Load<Product>(command.Id);
            if (product is null)
            {
                throw new ProductNotFoundException();
            }
            session.Delete(product);

            await session.SaveChangesAsync(cancellationToken);  
            return new DeleteProductResult(true);  
        }
    }
}
