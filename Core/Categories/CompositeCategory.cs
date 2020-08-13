using System.Collections.Generic;

namespace Core.Categories
{
    public class CompositeCategory : Category
    {
        public CompositeCategory(string name, int increment, int capacity, IReadOnlyCollection<string> categories) 
            : base(name, increment, capacity)
        {
            Categories = categories;
        }

        public IReadOnlyCollection<string> Categories { get; }
    }
}
