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

        endpoints.MapPost("books", (Database database) =>
        {
            var book = new Book(Guid.CreateVersion7(), $"NewBook {Guid.NewGuid()}");
            database.AddBook(book);
        });
    }
}