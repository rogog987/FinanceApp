using FinanceApp.ViewModels;

namespace FinanceApp.Views;

public partial class AddTransactionPage : ContentPage
{
  
	public AddTransactionPage(AddTransactionViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;      
	}

    private void OnTransactionTypeChanged(object sender, EventArgs e)
    {
        if (BindingContext is AddTransactionViewModel viewModel)
        {
            viewModel.IsExpense = ((Picker)sender).SelectedIndex == 0;
            viewModel.LoadCategories();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is AddTransactionViewModel viewModel)
        {
            viewModel.IsExpense = true;
            viewModel.SelectedCategory = null;
            viewModel.Amount = 0;
            viewModel.Description = string.Empty;
            viewModel.SelectedDate = DateTime.Today;
        }
    }
}