using FinanceApp.Models;
using FinanceApp.ViewModels;

namespace FinanceApp.Views;

public partial class TransactionsPage : ContentPage
{
	public TransactionsPage(TransactionViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await ((TransactionViewModel)BindingContext).LoadTransactions();
    }
    
    private async void OnTransactionTapped(object sender, EventArgs e)
    {
         if (sender is Element element && element.BindingContext is Transaction selectedTransaction)
    {
        int transactionId = selectedTransaction.Id;

        // Navigate to DetailTransactionPage with the transaction ID in the route
        await Shell.Current.GoToAsync($"DetailTransactionPage?TransactionId={transactionId}");
    }
    }
}