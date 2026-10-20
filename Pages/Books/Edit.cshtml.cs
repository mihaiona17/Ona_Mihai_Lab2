using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.BookPages;

public class EditModel : BookCategoriesPageModel
{
    private readonly Ona_Mihai_Lab2Context _context;

    public EditModel(Ona_Mihai_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    [BindProperty]
    public string[] SelectedCategoryIds { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book
            .AsNoTracking()
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (book is null)
        {
            return NotFound();
        }
        Book = book;
        SelectedCategoryIds = book.BookCategories?.Select(bc => bc.CategoryID.ToString()).ToArray() ?? [];
        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID","PublisherName");
        ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "FullName");
        PopulateAssignedCategoryData(_context, Book);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id, string[]? selectedCategories)
    {
        if (id is null)
        {
            return NotFound();
        }

        var bookToUpdate = await _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
            .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(b => b.ID == id);
        if (bookToUpdate is null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            book => book.Title,
            book => book.AuthorID,
            book => book.Price,
            book => book.PublishingDate,
            book => book.PublisherID))
        {
            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        SelectedCategoryIds = selectedCategories ?? [];
        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName", bookToUpdate.PublisherID);
        ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "FullName", bookToUpdate.AuthorID);
        PopulateAssignedCategoryData(_context, bookToUpdate);
        foreach (var category in AssignedCategoryDataList)
        {
            category.Assigned = SelectedCategoryIds.Contains(category.CategoryID.ToString());
        }
        Book = bookToUpdate;
        return Page();
    }
}
