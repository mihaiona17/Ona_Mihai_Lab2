using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DbContext _context;

        public IList<Book> Book { get; set; }

        public IndexModel(DbContext context)
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
