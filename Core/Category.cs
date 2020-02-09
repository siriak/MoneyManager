using System.Collections.Generic;

namespace Core
{
    public class Category
    {
		public Category(string name, ICollection<Rule> rules)
		{
			Name = name;
			Rules = rules;
		}

		public string Name { get; set; }
		public ICollection<Rule> Rules { get; }
	}
}
