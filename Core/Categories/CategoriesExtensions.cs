using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Categories
{
    public static class CategoriesExtensions
    {
        public static string CategoriesOrederer(Category category)
        {
            return category switch
            {
                CompositeCategory _ => "1" + category.Name,
                RegexCategory _ => "2" + category.Name,
                AutoCategory _ => "3" + category.Name,
                _ => throw new NotSupportedException(),
            };
        }
    }
}
