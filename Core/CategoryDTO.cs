using System.Collections.Generic;

namespace Core
{
    public class CategoryDto
    {
		public CategoryDto(string name, IEnumerable<Rule> rules)
		{
			Name = name;
			Rules = rules;
		}

		public string Name { get; set; }
		public IEnumerable<Rule> Rules { get; }
	}
}
