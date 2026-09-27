using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.Create;

public record Command(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<Result>;
public record Result(Guid Id);

internal class Handler : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        /* 
         * create entity from command object
         * save to database
         * return result
        */

        // create entity from command object
        var entity = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // save to database

        // return result
        return new Result(Guid.NewGuid());
    }
}
