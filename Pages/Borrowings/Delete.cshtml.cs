using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.Borrowings;

public class DeleteModel : PageModel
{
    private readonly Ona_Mihai_Lab2Context _context;

    public DeleteModel(Ona_Mihai_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowings.FindAsync(id);
        if (borrowing != null)
        {
            _context.Borrowings.Remove(borrowing);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
