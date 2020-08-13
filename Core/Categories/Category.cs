using System;

namespace Core.Categories
{
    public abstract class Category
    {
        public Category(string name, double increment, double capacity)
        {
            Name = name;
            Increment = increment;
            Capacity = capacity;
        }

        public string Name { get; }
        public double Increment { get; }
        public double Capacity { get; }
        
        public override bool Equals(object? obj)
        {
            return obj is Category category &&
                   Name == category.Name &&
                   Increment == category.Increment &&
                   Capacity == category.Capacity;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Increment, Capacity);
        }
    }
}
