namespace BookStoreAPI.Models
{
    // Redundant, used for readability purposes
    public class InsertBook
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }
    }
}