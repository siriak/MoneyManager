using System.Collections.Generic;

namespace Core
{
    public class Category
    {
		public Category(string name, ICollection<Rule> rules, double increment, int capacity)
		{
			Name = name;
			Rules = rules;
			Increment = increment;
			Capacity = capacity;
		}

		public string Name { get; set; }
		public ICollection<Rule> Rules { get; }
		public double Increment { get; }
		public double Capacity { get; }
	}
}
