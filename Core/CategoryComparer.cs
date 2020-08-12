using Core.Categories;
using System.Collections.Generic;

namespace Core
{
    internal class CategoryComparer : IEqualityComparer<ICategory>
    {
        public bool Equals(ICategory x, ICategory y)
        {
            if (ReferenceEquals(x, y)) 
                return true;

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.Name == y.Name;
        }

        public int GetHashCode(ICategory category)
        {
            if (category is null) return 0;

            return category.Name.GetHashCode();
        }
    }
}