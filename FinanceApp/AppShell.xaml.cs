using FinanceApp.Views;

namespace FinanceApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("TransactionsPage", typeof(TransactionsPage));
            Routing.RegisterRoute("AddTransactionPage", typeof(AddTransactionPage));
            Routing.RegisterRoute("ExpenseChartPage", typeof(ExpenseChartPage));
            Routing.RegisterRoute("DetailTransactionPage", typeof(DetailTransactionPage));
        }
    }
}
