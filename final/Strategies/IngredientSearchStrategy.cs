using System;
using System.Collections.Generic;
using System.Linq;
using final.Models;

namespace final.Strategies
{
    public class IngredientSearchStrategy : ISearchStrategy
    {
            public IEnumerable<Recipe> Filter(IEnumerable<Recipe> recipes, string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return recipes;

            var lowerQuery = query.ToLower().Trim();
            // Фільтрація рецептів за інгредієнтами
            return recipes.Where(r => 
                r.Ingredients != null && r.Ingredients.Any(i => 
                    i.Name != null && i.Name.ToLower().Contains(lowerQuery)
                )
            );
        }
    }
}
