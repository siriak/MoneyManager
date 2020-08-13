using System;

namespace Core.Categories
{
    public class Category
    {
        protected Category(string name, double increment, double capacity)
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
                   Increment - category.Increment < 0.01 &&
                   Capacity - category.Capacity < 0.01;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Increment, Capacity);
        }
    }
}
