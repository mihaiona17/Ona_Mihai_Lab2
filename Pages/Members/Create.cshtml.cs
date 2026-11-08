using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.Members;

public class CreateModel : PageModel
{
    private readonly Ona_Mihai_Lab2Context _context;

    public CreateModel(Ona_Mihai_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Member Member { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Members.Add(Member);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
