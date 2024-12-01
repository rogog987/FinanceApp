using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceApp.Models;
using FinanceApp.Services;
using FinanceApp.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace FinanceApp.ViewModels
{
    public partial class AddTransactionViewModel : ObservableObject
    {
        private readonly LocalDbService _dbService;

        [ObservableProperty]
        private bool isExpense;

        [ObservableProperty]
        private ObservableCollection<Category> categories;

        [ObservableProperty]
        private Category selectedCategory;

        [ObservableProperty]
        private decimal amount;

        [ObservableProperty]
        private DateTime selectedDate = DateTime.Today;

        [ObservableProperty]
        private string? description;


        public AddTransactionViewModel(LocalDbService dbService)
        {
            _dbService = dbService;
            LoadCategories();
        }

        public async void LoadCategories()
        {
            var allCategories = await _dbService.GetCategories();
            Categories = new ObservableCollection<Category>(allCategories.Where(c => c.IsExpense == IsExpense));
        }

        [RelayCommand]
        public async Task SaveTransactionAsync()
        {
            if (Amount <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Amount", "Please enter an amount greater than zero.", "OK");
                return;
            }
            if (SelectedCategory == null)
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Category", "Please select a category.", "OK");
                return;
            }
            var transaction = new Transaction
            {
                Amount = Amount,
                CategoryId = SelectedCategory.Id,
                Description = Description,
                Date = SelectedDate,
                IsIncome = !IsExpense
            };

            await _dbService.CreateTransaction(transaction);
            await Shell.Current.GoToAsync("//TransactionsPage"); 
        }
    }
}
