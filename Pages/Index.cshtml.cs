using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Ona_Mihai_Lab2Context _context;

        public IList<Book> Book { get; set; }

        public IndexModel(Ona_Mihai_Lab2Context context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Book = await _context.Set<Book>()
                .Include(b => b.Publisher)
                .ToListAsync();
        }
    }
}
