namespace FinanceApp.Views;
using ViewModels;

public partial class ExpenseChartPage : ContentPage
{
	public ExpenseChartPage(ExpenseChartViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}