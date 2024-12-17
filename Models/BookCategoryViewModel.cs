using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bogdan_Cristina_Lab2.Models
{
    public class BookCategoryViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public List<int> SelectedCategories { get; set; }
        public List<SelectListItem> AvailableCategories { get; set; }
    }

}
