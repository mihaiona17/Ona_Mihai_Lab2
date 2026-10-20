using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.Publishers;

public class CreatePublisherModel : PageModel
{
    private readonly Ona_Mihai_Lab2Context _context;

    public CreatePublisherModel(Ona_Mihai_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Publisher Publisher { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Publishers.Add(Publisher);
        await _context.SaveChangesAsync();
        return RedirectToPage("/Books/Index");
    }
}
