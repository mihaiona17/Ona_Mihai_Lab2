using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.Authors;

public class DeleteModel : PageModel
{
    private readonly Ona_Mihai_Lab2Context _context;

    public DeleteModel(Ona_Mihai_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Author Author { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var author = await _context.Authors.FirstOrDefaultAsync(a => a.ID == id);
        if (author is null)
        {
            return NotFound();
        }

        Author = author;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var author = await _context.Authors.FindAsync(id);
        if (author != null)
        {
            Author = author;
            _context.Authors.Remove(Author);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
