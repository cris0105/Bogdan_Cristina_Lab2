using Microsoft.AspNetCore.Mvc.RazorPages;
using Bogdan_Cristina_Lab2.Data;

namespace Bogdan_Cristina_Lab2.Models
{
    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList { get; set; } = new List<AssignedCategoryData>();

        public void PopulateAssignedCategoryData(Bogdan_Cristina_Lab2Context context, Book book)
        {
            var allCategories = context.Categories.ToList();
            var bookCategories = new HashSet<int>(
                book.BookCategories?.Select(c => c.CategoryID) ?? Enumerable.Empty<int>());

            AssignedCategoryDataList = new List<AssignedCategoryData>();

            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = bookCategories.Contains(cat.ID)
                });
            }
        }

        public void UpdateBookCategories(Bogdan_Cristina_Lab2Context context,
            string[] selectedCategories, Book bookToUpdate)
        {
            if (selectedCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }

            if (bookToUpdate.BookCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var bookCategories = new HashSet<int>(
                bookToUpdate.BookCategories.Select(c => c.CategoryID));

            foreach (var cat in context.Categories.ToList())
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!bookCategories.Contains(cat.ID))
                    {
                        bookToUpdate.BookCategories.Add(
                            new BookCategory
                            {
                                BookID = bookToUpdate.ID,
                                CategoryID = cat.ID
                            });
                    }
                }
                else
                {
                    var categoryToRemove = bookToUpdate.BookCategories
                        .SingleOrDefault(i => i.CategoryID == cat.ID);
                    if (categoryToRemove != null)
                    {
                        context.Remove(categoryToRemove);
                    }
                }
            }
        }
    }
}
