using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Core
{
    public static class CategoriesManager
    {
		public static Dictionary<string, Func<Transaction, bool>> Load()
		{
			if (!File.Exists("categories.json"))
			{
				return new Dictionary<string, Func<Transaction, bool>>();
			}

			var categories = JsonConvert.DeserializeObject<List<CategoryDto>>(File.ReadAllText("categories.json"));

			var categoryFilters = new Dictionary<string, Func<Transaction, bool>>();

			foreach (var c in categories)
			{
				categoryFilters.Add(c.Name, t => Regex.IsMatch(t.ApplicationCode, c.ApplicationCode)
											&& Regex.IsMatch(t.CardNumber, c.CardNumber)
											&& Regex.IsMatch(t.Description, c.Description)
											&& Regex.IsMatch(t.IsExpence.ToString(), c.IsExpence)
											&& Regex.IsMatch(t.IsIncome.ToString(), c.IsIncome)
											&& Regex.IsMatch(t.Terminal, c.Terminal));
			}

			return categoryFilters;
		}
	}
}
