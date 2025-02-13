namespace Demo.HybridCache;

public class Database
{
    private readonly List<Book> _books = Enumerable
        .Range(1, 10)
        .Select(x => new Book(
            Guid.CreateVersion7(),
            $"Book {x}" ))
        .ToList();

    public async Task<IReadOnlyList<Book>> GetBooksAsync()
    {
        Console.WriteLine("Fetching data ...");
        await Task.Delay(5000);

        return _books;
    }

    public void AddBook(Book newBook)
    {
        _books.Add(newBook);
    }
}