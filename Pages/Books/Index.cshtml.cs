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

    public IList<Book> Book { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Book = await _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .ToListAsync();
    }
}
