using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core
{
    public static class CategoriesManager
    {
		public static Dictionary<string, Func<Transaction, bool>> BuildFilters(List<Category> categories)
		{
			var categoryFilters = new Dictionary<string, Func<Transaction, bool>>();
			
			foreach (var c in categories)
			{
				categoryFilters.Add(c.Name, t => c.Rules.Any(r => Regex.IsMatch(t.CardNumber, r.CardNumber)
											&& Regex.IsMatch(t.Description, r.Description)
											&& Regex.IsMatch(t.Amount.ToString().ToLower(), r.Amount)
											&& r.Amount[1..] is var ruleAmount
											&& r.Amount[0] switch
											{
												'>' => t.Amount.Amount > int.Parse(ruleAmount),
												'<' => t.Amount.Amount < int.Parse(ruleAmount),
												'=' => t.Amount.Amount == int.Parse(ruleAmount),
												'*' => true,
												_ => throw new NotImplementedException()
											}
											));
			}

			return categoryFilters;
		}
	}
}
