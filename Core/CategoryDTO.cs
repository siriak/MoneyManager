using System.Collections.Generic;

namespace Core
{
    public class CategoryDto
    {
		public CategoryDto(string name, List<Rule> rules)
		{
			Name = name;
			Rules = rules;
		}

		public string Name { get; set; }
		public List<Rule> Rules { get; set; }
	}
}
