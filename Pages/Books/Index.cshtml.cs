using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Ona_Mihai_Lab2Context _context;

    public IndexModel(Ona_Mihai_Lab2Context context)
    {
        _context = context;
    }

    public BookData BookData { get; set; } = new();
    public IList<Book> Book { get; set; } = [];
    public int BookID { get; set; }
    public int CategoryID { get; set; }

    public string TitleSort { get; set; } = "";
    public string AuthorSort { get; set; } = "";
    public string CurrentFilter { get; set; } = "";

    public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
    {
        TitleSort = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "author" ? "author_desc" : "author";
        CurrentFilter = searchString ?? "";

        IQueryable<Book> booksQuery = _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories!)
            .ThenInclude(bc => bc.Category);

        if (!string.IsNullOrEmpty(searchString))
        {
            booksQuery = booksQuery.Where(b => b.Title.Contains(searchString)
                || (b.Author != null && (b.Author.FirstName.Contains(searchString)
                    || b.Author.LastName.Contains(searchString))));
        }

        booksQuery = sortOrder switch
        {
            "title_desc" => booksQuery.OrderByDescending(b => b.Title),
            "author" => booksQuery.OrderBy(b => b.Author!.FullName),
            "author_desc" => booksQuery.OrderByDescending(b => b.Author!.FullName),
            _ => booksQuery.OrderBy(b => b.Title)
        };

        var books = await booksQuery.AsNoTracking().ToListAsync();

        Book = books;
        if (id.HasValue)
        {
            BookID = id.Value;
        }
        if (categoryID.HasValue)
        {
            CategoryID = categoryID.Value;
        }

        BookData = new BookData
        {
            Books = books,
            Categories = await _context.Category.ToListAsync(),
            BookCategories = books.SelectMany(book => book.BookCategories ?? []).ToList()
        };
    }
}
