using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.BookPages;

public class CreateModel : BookCategoriesPageModel
{
    private readonly Ona_Mihai_Lab2Context _context;

    public CreateModel(Ona_Mihai_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID","PublisherName");
        ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "FullName");
        var book = new Book
        {
            BookCategories = []
        };
        PopulateAssignedCategoryData(_context, book);
        return Page();
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync(string[]? selectedCategories)
    {
        if (!ModelState.IsValid)
        {
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName", Book.PublisherID);
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "ID", "FullName", Book.AuthorID);
            PopulateAssignedCategoryData(_context, Book);
            foreach (var category in AssignedCategoryDataList)
            {
                category.Assigned = selectedCategories?.Contains(category.CategoryID.ToString()) == true;
            }
            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        UpdateBookCategories(_context, selectedCategories, Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
