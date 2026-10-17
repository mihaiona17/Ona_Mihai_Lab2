using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.Authors;

public class EditModel : PageModel
{
    private readonly Ona_Mihai_Lab2Context _context;

    public EditModel(Ona_Mihai_Lab2Context context)
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

        var author = await _context.Authors.FindAsync(id);
        if (author is null)
        {
            return NotFound();
        }

        Author = author;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Author).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Authors.Any(a => a.ID == Author.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }
}
