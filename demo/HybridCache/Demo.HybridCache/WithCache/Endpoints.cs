namespace Demo.HybridCache;

using Microsoft.Extensions.Caching.Hybrid;

public static partial class Endpoints
{
    public static void MapEndpointsWithCache(this IEndpointRouteBuilder endpoints)
    {
        var cacheGroup = endpoints.MapGroup("cached");
        
        cacheGroup.MapGet("books", async (HybridCache cache, Database database) =>
        {
            var books = await cache
                .GetOrCreateAsync(
                    "books", 
                    database,
                    static async (db, ct) => await db.GetBooksAsync(),
                    tags: ["books"]);

            return books;
        });

        cacheGroup.MapGet("books/{number:int}", async (HybridCache cache, Database database, int number) =>
        {
            database.AddBook(new Book(Guid.CreateVersion7(), $"NewBook {number}"));
            await cache.RemoveByTagAsync("books");
        });

        cacheGroup.MapGet("books/reset", async (HybridCache cache) =>
        {
            await cache.RemoveByTagAsync("books");
        });
    }
}