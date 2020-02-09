using System.Collections.Generic;

namespace Core
{
    public class Category
    {
		public Category(string name, IList<Rule> rules)
		{
			Name = name;
			Rules = rules;
		}

		public string Name { get; set; }
		public IList<Rule> Rules { get; }
	}
}
