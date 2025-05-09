namespace Demo.HybridCache;

using Microsoft.Extensions.Caching.Hybrid;

public static partial class Endpoints
{
    public static void MapEndpointsWithCache(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("books", async (HybridCache cache, Database database) =>
        {
            var books = await cache.GetOrCreateAsync(
                "books", 
                database,
                static async (db, ct) => await db.GetBooksAsync(),
                tags: ["books"]);

            return books;
        });

        endpoints.MapPost("books", async (HybridCache cache, Database database) =>
        {
            var book = new Book(Guid.CreateVersion7(), $"NewBook {Guid.NewGuid()}");
            database.AddBook(book);

            await cache.RemoveByTagAsync("books");
        });

        endpoints.MapPut("books", async (HybridCache cache) =>
        {
            await cache.RemoveByTagAsync("books");
        });
    }
}