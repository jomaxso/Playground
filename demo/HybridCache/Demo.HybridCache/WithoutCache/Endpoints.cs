namespace Demo.HybridCache;

public static partial class Endpoints
{
    public static void MapEndpointsWithoutCache(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("books", async (Database database) =>
        {
            var books = await database.GetBooksAsync();

            return books;
        });

        endpoints.MapGet("books/{number:int}", (Database database, int number) =>
        {
            database.AddBook(new Book(Guid.CreateVersion7(), $"NewBook {number}"));
        });
    }
}