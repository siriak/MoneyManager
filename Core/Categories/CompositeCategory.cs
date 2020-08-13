namespace Core.Categories
{
    public class CompositeCategory : Category
    {
        public CompositeCategory(string name, double increment, double capacity, string[] categories) 
            : base(name, increment, capacity)
        {
            Categories = categories;
        }

        public string[] Categories { get; }
    }
}
