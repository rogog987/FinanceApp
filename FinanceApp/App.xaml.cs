using FinanceApp.Services;

namespace FinanceApp
{
    public partial class App : Application
    {
        public static LocalDbService LocalDbService { get; set; }
        public App()
        {
            InitializeComponent();
            LocalDbService = new LocalDbService();

            MainPage = new AppShell();

            Task.Run(async() => LocalDbService.InitializeAsync()).Wait();
        }
    }
}
