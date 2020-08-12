using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Categories
{
    public class CompositeCategory : ICategory
    {
        public CompositeCategory(string name, double increment, double capacity, string[] categories)
        {
            Name = name;
            Increment = increment;
            Capacity = capacity;
            Categories = categories;
        }

        public string Name { get; }
        public double Increment { get; }
        public double Capacity { get; }
        public string[] Categories { get; }
    }
}
