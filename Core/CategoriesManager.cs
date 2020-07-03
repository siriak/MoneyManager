using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core
{
    public static class CategoriesManager
    {
		public static Dictionary<string, Func<Transaction, bool>> Load()
		{
			//TODO: load from user provided file
			if (true)
			{
				return new Dictionary<string, Func<Transaction, bool>>();
			}

			// var categories = JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText("categories.json"));
			//
			// var categoryFilters = new Dictionary<string, Func<Transaction, bool>>();
			//
			// foreach (var c in categories)
			// {
			// 	categoryFilters.Add(c.Name, t => c.Rules.Any(r => Regex.IsMatch(t.CardNumber, r.CardNumber)
			// 								&& Regex.IsMatch(t.Description, r.Description)
			// 								&& Regex.IsMatch(t.IsExpence.ToString().ToLower(), r.IsExpence)
			// 								&& Regex.IsMatch(t.IsIncome.ToString().ToLower(), r.IsIncome)
			// 								&& Regex.IsMatch(t.Terminal, r.Terminal)));
			// }
			//
			// return categoryFilters;
		}
	}
}
