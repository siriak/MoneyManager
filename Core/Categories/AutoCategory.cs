namespace Core.Categories
{
    public class AutoCategory : Category
    {
        public AutoCategory(string name, double increment, double capacity, string category)
             : base(name, increment, capacity)
        {
            Category = category;
        }

        public string Category { get; set; }
    }
}
