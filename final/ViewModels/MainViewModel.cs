using System.Collections.ObjectModel;
using System.Windows.Input;
using RecipeFinder.Models;
using RecipeFinder.Services;

namespace RecipeFinder.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IRecipeRepository _repository;
        private readonly ISearchStrategy _searchStrategy;
        private ObservableCollection<Recipe> _recipes;
        private string _searchText;

        public MainViewModel(IRecipeRepository repository)
        {
            _repository = repository;
            _searchStrategy = new NameSearchStrategy();
            _recipes = new ObservableCollection<Recipe>(_repository.GetAll());

            SearchCommand = new RelayCommand(obj => ExecuteSearch());
            AddCommand = new RelayCommand(obj => OpenEditor());
        }

        public ObservableCollection<Recipe> Recipes
        {
            get => _recipes;
            set => SetProperty(ref _recipes, value);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    ExecuteSearch();
                }
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }

        private void ExecuteSearch()
        {
            var filtered = _searchStrategy.Filter(_repository.GetAll(), SearchText);
            Recipes = new ObservableCollection<Recipe>(filtered);
        }

        private void OpenEditor() { /* Логіка відкриття вікна створення */ }
    }
}
