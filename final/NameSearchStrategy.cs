public class NameSearchStrategy : ISearchStrategy
{
    public IEnumerable<Recipe> Filter(IEnumerable<Recipe> source, string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return source;
        }

        return source.Where(recipe =>
            recipe.Title.Contains(query, StringComparison.OrdinalIgnoreCase));
    }
}
