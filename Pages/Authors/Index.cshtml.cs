using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.Authors;

public class IndexModel : PageModel
{
    private readonly Ona_Mihai_Lab2Context _context;

    public IndexModel(Ona_Mihai_Lab2Context context)
    {
        _context = context;
    }

    public IList<Author> Author { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Author = await _context.Authors.ToListAsync();
    }
}
