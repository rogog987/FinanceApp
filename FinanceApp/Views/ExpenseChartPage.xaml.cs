namespace FinanceApp.Views;

using System.Data;
using ViewModels;

public partial class ExpenseChartPage : ContentPage
{
	public ExpenseChartPage(ExpenseChartViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ExpenseChartViewModel viewModel)
        {
            viewModel.LoadExpenseChartData();
        }
    }
}