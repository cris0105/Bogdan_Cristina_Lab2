using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Bogdan_Cristina_Lab2.Data;
using Bogdan_Cristina_Lab2.Models;

namespace Bogdan_Cristina_Lab2.Pages.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly Bogdan_Cristina_Lab2Context _context;

        public DetailsModel(Bogdan_Cristina_Lab2Context context)
        {
            _context = context;
        }

        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }
    }
    public class Bogdan_Cristina_Lab2Context : DbContext
    {
        public Bogdan_Cristina_Lab2Context(DbContextOptions<Bogdan_Cristina_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList { get; set; } = new List<AssignedCategoryData>();

        public void PopulateAssignedCategoryData(Bogdan_Cristina_Lab2Context context, Book book)
        {
            // Implementation for populating assigned category data
        }

        public void UpdateBookCategories(Bogdan_Cristina_Lab2Context context, string[] selectedCategories, Book bookToUpdate)
        {
            if (selectedCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }

            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var bookCategories = new HashSet<int>(bookToUpdate.BookCategories?.Select(c => c.CategoryID) ?? Enumerable.Empty<int>());

            if (bookToUpdate.BookCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
            }

            foreach (var category in context.Categories)
            {
                if (selectedCategoriesHS.Contains(category.ID.ToString()))
                {
                    if (!bookCategories.Contains(category.ID))
                    {
                        bookToUpdate.BookCategories.Add(new BookCategory
                        {
                            CategoryID = category.ID,
                            Book = bookToUpdate, // Set the Book property
                            Category = category // Set the Category property
                        });
                    }
                }
                else
                {
                    if (bookCategories.Contains(category.ID))
                    {
                        var categoryToRemove = bookToUpdate.BookCategories.FirstOrDefault(i => i.CategoryID == category.ID);
                        if (categoryToRemove != null)
                        {
                            bookToUpdate.BookCategories.Remove(categoryToRemove);
                        }
                    }
                }
            }
        }
    }
}
