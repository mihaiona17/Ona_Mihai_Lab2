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

    public async Task OnGetAsync(int? id, int? categoryID)
    {
        var books = await _context.Book
            .AsNoTracking()
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories!)
                .ThenInclude(bc => bc.Category)
            .ToListAsync();

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
