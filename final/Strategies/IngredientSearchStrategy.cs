using System;
using System.Collections.Generic;
using System.Linq;
using final.Models;

namespace final.Strategies
{
    public class IngredientSearchStrategy : ISearchStrategy
    {
        public IEnumerable<Recipe> Search(IEnumerable<Recipe> recipes, string query)
        {
            // Якщо пошуковий запит порожній — повертаємо всі рецепти без змін
            if (string.IsNullOrWhiteSpace(query))
                return recipes;

            var lowerQuery = query.ToLower().Trim();

            // Шукаємо рецепти, де хоча б один інгредієнт містить текст запиту
            return recipes.Where(r => 
                r.Ingredients != null && r.Ingredients.Any(i => 
                    i.Name != null && i.Name.ToLower().Contains(lowerQuery)
                )
            );
        }
    }
}
