using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ona_Mihai_Lab2.Models;
using Ona_Mihai_Lab2.ViewModels;

namespace Ona_Mihai_Lab2.Pages.PublisherPages
{
    public class IndexModel : PageModel
    {
        private readonly Ona_Mihai_Lab2Context _context;

        public IndexModel(Ona_Mihai_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publisher { get; set; } = default!;
        public PublisherIndexData PublisherData { get; set; } = default!;
        public int PublisherID { get; set; }
        public int BookID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {
            PublisherData = new PublisherIndexData();
            PublisherData.Publishers = await _context.Publishers
                .Include(i => i.Books)
                .ThenInclude(c => c.Author)
                .OrderBy(i => i.PublisherName)
                .ToListAsync();

            if (id != null)
            {
                PublisherID = id.Value;
                Publisher publisher = PublisherData.Publishers
                    .Where(i => i.ID == id.Value).Single();
                PublisherData.Books = publisher.Books;
            }

            if (bookID != null)
            {
                BookID = bookID.Value;
                var selectedBook = PublisherData.Books
                    .Where(x => x.ID == bookID.Value).Single();
            }
        }
    }
}
