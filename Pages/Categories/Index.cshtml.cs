using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Bogdan_Cristina_Lab2.Data;
using Bogdan_Cristina_Lab2.Models;

namespace Bogdan_Cristina_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Bogdan_Cristina_Lab2Context _context;

        public IndexModel(Bogdan_Cristina_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.ToListAsync();
        }
    }
}
