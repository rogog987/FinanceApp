using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceApp.Models;
using FinanceApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.ViewModels
{
    public partial class DetailTransactionViewModel : ObservableObject, IQueryAttributable
    {
        private readonly LocalDbService _dbService;

        [ObservableProperty]
        private Transaction transaction;

        [ObservableProperty]
        private ObservableCollection<Category> compatibleCategories;

        public DetailTransactionViewModel(LocalDbService dbService)
        {
            _dbService = dbService;
        }

        [ObservableProperty]
        private string selectedCategory;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("TransactionId", out var transactionId))
            {
                // Check if the value is a string and parse it
                if (transactionId is string idString && int.TryParse(idString, out int id))
                {
                    LoadTransaction(id);
                }
                // If it's already an integer, you can proceed with it
                else if (transactionId is int intId)
                {
                    LoadTransaction(intId);
                }
            }
        }
              
        [RelayCommand]
        public async Task LoadTransaction(int transactionId)
        {
            Transaction = await _dbService.GetTransactionById(transactionId);

            var category = await _dbService.GetCategoryById(Transaction.CategoryId);

            // Filter categories by the same IsExpense type
            var allCategories = await _dbService.GetCategories();
            CompatibleCategories = new ObservableCollection<Category>(
                allCategories.Where(c => c.IsExpense == category.IsExpense));
    
            SelectedCategory = category.Name;
        }

        [RelayCommand]
        public async Task SaveTransaction()
        {
            if (Transaction.Amount <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Invalid Amount", "Please enter an amount greater than zero.", "OK");
                return;
            }
            if (Transaction.Category != null)
            {
                Transaction.CategoryId = Transaction.Category.Id; 
            }
            await _dbService.UpdateTransaction(Transaction);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task DeleteTransaction()
        {
            await _dbService.DeleteTransaction(Transaction);
            await Shell.Current.GoToAsync("..");
        }
    }
}
