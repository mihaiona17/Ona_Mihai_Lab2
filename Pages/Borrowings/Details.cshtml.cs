using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.Borrowings;

public class DetailsModel : PageModel
{
    private readonly Ona_Mihai_Lab2Context _context;

    public DetailsModel(Ona_Mihai_Lab2Context context)
    {
        _context = context;
    }

    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowings
            .Include(b => b.Member)
            .Include(b => b.Book)
                .ThenInclude(b => b.Author)
            .FirstOrDefaultAsync(b => b.ID == id);
        if (borrowing is null)
        {
            return NotFound();
        }

        Borrowing = borrowing;
        return Page();
    }
}
