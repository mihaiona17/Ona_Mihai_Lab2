using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.Authors;

public class DetailsModel : PageModel
{
    private readonly Ona_Mihai_Lab2Context _context;

    public DetailsModel(Ona_Mihai_Lab2Context context)
    {
        _context = context;
    }

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
}
