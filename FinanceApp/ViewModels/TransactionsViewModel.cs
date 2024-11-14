using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceApp.Models;
using FinanceApp.Services;
using FinanceApp.Views;
using System.Collections.ObjectModel;

namespace FinanceApp.ViewModels
{
    public partial class TransactionViewModel : ObservableObject
    {
        private readonly LocalDbService _dbService;

        [ObservableProperty]
        private ObservableCollection<Transaction> transactions;

        public TransactionViewModel(LocalDbService dbService)
        {
            _dbService = dbService;
            LoadTransactions();
        }

        public async Task LoadTransactions()
        {
            var allTransactions = await _dbService.GetTransactions();
            Transactions = new ObservableCollection<Transaction>(allTransactions);
        }

    }

}
