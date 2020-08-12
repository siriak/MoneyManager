using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Categories
{
    public class AutoCategory : ICategory
    {
        public AutoCategory(string name, double increment, double capacity, string category)
        {
            Name = name;
            Increment = increment;
            Capacity = capacity;
            Category = category;
        }

        public string Name { get; }
        public string Category { get; set; }
        public double Increment { get; }
        public double Capacity { get; }
    }
}