using Microsoft.AspNetCore.Mvc.RazorPages;
using Ona_Mihai_Lab2.Models;

namespace Ona_Mihai_Lab2.Pages.BookPages;

public class BookCategoriesPageModel : PageModel
{
    public IList<AssignedCategoryData> AssignedCategoryDataList { get; set; } = [];

    public void PopulateAssignedCategoryData(Ona_Mihai_Lab2Context context, Book book)
    {
        var bookCategories = new HashSet<int>(
            book.BookCategories?.Select(bc => bc.CategoryID) ?? Enumerable.Empty<int>());

        AssignedCategoryDataList = context.Category
            .OrderBy(category => category.CategoryName)
            .Select(category => new AssignedCategoryData
            {
                CategoryID = category.ID,
                Name = category.CategoryName,
                Assigned = bookCategories.Contains(category.ID)
            })
            .ToList();
    }

    public void UpdateBookCategories(
        Ona_Mihai_Lab2Context context,
        string[]? selectedCategories,
        Book bookToUpdate)
    {
        if (selectedCategories is null)
        {
            bookToUpdate.BookCategories = [];
            return;
        }

        var selectedCategoriesHashSet = new HashSet<string>(selectedCategories);
        var existingCategories = new HashSet<int>(
            bookToUpdate.BookCategories?.Select(bc => bc.CategoryID) ?? Enumerable.Empty<int>());

        foreach (var category in context.Category)
        {
            var isSelected = selectedCategoriesHashSet.Contains(category.ID.ToString());
            var isAssigned = existingCategories.Contains(category.ID);

            if (isSelected && !isAssigned)
            {
                bookToUpdate.BookCategories ??= [];
                bookToUpdate.BookCategories.Add(new BookCategory
                {
                    BookID = bookToUpdate.ID,
                    CategoryID = category.ID
                });
            }
            else if (!isSelected && isAssigned)
            {
                var categoryToRemove = bookToUpdate.BookCategories!
                    .Single(bc => bc.CategoryID == category.ID);
                context.Remove(categoryToRemove);
            }
        }
    }
}
