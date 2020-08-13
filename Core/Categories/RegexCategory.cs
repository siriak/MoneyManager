using Core.Categories;
using System.Collections.Generic;

namespace Core
{
    public class RegexCategory : Category
    {
        public RegexCategory(string name, IReadOnlyCollection<Rule> rules, int increment, int capacity)
            :base(name, increment, capacity)
        {
            Rules = rules;
        }

        public IReadOnlyCollection<Rule> Rules { get; }
    }

}
