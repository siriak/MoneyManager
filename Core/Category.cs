using System.Collections.Generic;

namespace Core
{
    public class Category
    {
        public Category(string name, IReadOnlyCollection<Rule> rules, double increment, double capacity)
        {
            Name = name;
            Rules = rules;
            Increment = increment;
            Capacity = capacity;
        }

        public string Name { get; }
        public IReadOnlyCollection<Rule> Rules { get; }
        public double Increment { get; }
        public double Capacity { get; }
    }
}
