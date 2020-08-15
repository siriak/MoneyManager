namespace Core.Categories
{
    public class Category
    {
        protected Category(string name, int increment, int capacity)
        {
            Name = name;
            Increment = increment;
            Capacity = capacity;
        }

        public string Name { get; }
        public int Increment { get; }
        public int Capacity { get; }
        
        public override bool Equals(object? obj)
        {
            return obj is Category category &&
                   Name == category.Name;
        }
        
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
