using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.Authors;

public class CreateModel : PageModel
{
    private readonly Ona_Mihai_Lab2Context _context;

    public CreateModel(Ona_Mihai_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Author Author { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Authors.Add(Author);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
