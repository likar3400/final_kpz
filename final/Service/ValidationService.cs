using System;
using System.Linq;
using RecipeFinder.Models;
using System.Collections.Generic;

namespace RecipeFinder.Services
{
    public interface IValidationRule
    {
        bool Validate(Recipe recipe, List<string> errors);
    }

    public class TitleValidationRule : IValidationRule
    {
        public bool Validate(Recipe recipe, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(recipe.Title))
            {
                errors.Add("Назва рецепта є обов'язковою.");
                return false;
            }
            if (recipe.Title.Length < 3)
            {
                errors.Add("Назва занадто коротка (мінімум 3 символи).");
                return false;
            }
            return true;
        }
    }

    public class IngredientsValidationRule : IValidationRule
    {
        public bool Validate(Recipe recipe, List<string> errors)
        {
            if (recipe.Ingredients == null || !recipe.Ingredients.Any())
            {
                errors.Add("Список інгредієнтів не може бути порожнім.");
                return false;
            }

            foreach (var ing in recipe.Ingredients)
            {
                if (ing.Amount <= 0)
                    errors.Add($"Кількість для '{ing.Name}' має бути більшою за 0.");
            }
            return true;
        }
    }

    public class InstructionsValidationRule : IValidationRule
    {
        public bool Validate(Recipe recipe, List<string> errors)
        {
            if (recipe.Instructions == null || !recipe.Instructions.Any())
            {
                errors.Add("Рецепт повинен мати хоча б один крок приготування.");
                return false;
            }
            return true;
        }
    }

    public class ValidationService
    {
        private readonly List<IValidationRule> _rules;
        public List<string> Errors { get; private set; } = new List<string>();

        public ValidationService()
        {
            _rules = new List<IValidationRule>
            {
                new TitleValidationRule(),
                new IngredientsValidationRule(),
                new InstructionsValidationRule()
            };
        }

        public bool ValidateRecipe(Recipe recipe)
        {
            Errors.Clear();

            if (recipe == null)
            {
                Errors.Add("Об'єкт рецепта не може бути порожнім.");
                return false;
            }

            foreach (var rule in _rules)
            {
                rule.Validate(recipe, Errors);
            }

            return !Errors.Any();
        }
    }
}