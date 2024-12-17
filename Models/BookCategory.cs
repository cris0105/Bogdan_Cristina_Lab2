namespace Bogdan_Cristina_Lab2.Models
{
    public class BookCategory
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public required Book Book { get; set; }
        public int CategoryID { get; set; }
        public required Category Category { get; set; }
    }
}
