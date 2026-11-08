using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.Borrowings;

public class IndexModel : PageModel
{
    private readonly Ona_Mihai_Lab2Context _context;

    public IndexModel(Ona_Mihai_Lab2Context context)
    {
        _context = context;
    }

    public IList<Borrowing> Borrowing { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Borrowing = await _context.Borrowings
            .Include(b => b.Member)
            .Include(b => b.Book)
                .ThenInclude(b => b.Author)
            .ToListAsync();
    }
}
