namespace Demo.HybridCache;

using Microsoft.Extensions.Caching.Hybrid;

public static partial class Endpoints
{
    public static void MapEndpointsWithCache(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("books", async (HybridCache cache, Database database) =>
        {
            return await cache.GetOrCreateAsync(
                "books", 
                async _ => await database.GetBooksAsync(),
                tags: ["books"]);
        });

        endpoints.MapPost("books", async (HybridCache cache, Database database) =>
        {
            var book = new Book(Guid.CreateVersion7(), $"NewBook {Guid.NewGuid()}");
            database.AddBook(book);
            
            await cache.RemoveByTagAsync("books");
            await cache.SetAsync($"book-{book.Id}", book, tags: ["books"]);
        });

        endpoints.MapPut("books", async (HybridCache cache) =>
        {
            await cache.RemoveByTagAsync("books");
        });
    }
}