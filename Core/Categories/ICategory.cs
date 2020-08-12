using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Categories
{
    public interface ICategory
    {
        public string Name { get; }
        public double Increment { get; }
        public double Capacity { get; }
    }
}
