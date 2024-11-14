using FinanceApp.ViewModels;

namespace FinanceApp.Views;

public partial class DetailTransactionPage : ContentPage
{
	public DetailTransactionPage(DetailTransactionViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}