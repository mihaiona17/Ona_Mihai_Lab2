using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.Borrowings;

public class EditModel : PageModel
{
    private readonly Ona_Mihai_Lab2Context _context;

    public EditModel(Ona_Mihai_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public SelectList Members { get; set; } = default!;
    public SelectList Books { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowings.FindAsync(id);
        if (borrowing is null)
        {
            return NotFound();
        }

        Borrowing = borrowing;
        LoadSelectLists();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            LoadSelectLists();
            return Page();
        }

        _context.Attach(Borrowing).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Borrowings.Any(b => b.ID == Borrowing.ID))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private void LoadSelectLists()
    {
        Members = new SelectList(_context.Members, "ID", "FullName", Borrowing.MemberID);
        var bookList = _context.Book
            .Include(b => b.Author)
            .Select(b => new
            {
                b.ID,
                BookFullName = b.Title + " - " + b.Author!.LastName + " " + b.Author.FirstName
            });

        Books = new SelectList(bookList, "ID", "BookFullName", Borrowing.BookID);
    }
}
