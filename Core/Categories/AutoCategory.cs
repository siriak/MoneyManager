namespace Core.Categories
{
    public class AutoCategory : Category
    {
        public AutoCategory(string name, int increment, int capacity, string category)
             : base(name, increment, capacity)
        {
            Category = category;
        }

        public string Category { get; set; }
    }
}
