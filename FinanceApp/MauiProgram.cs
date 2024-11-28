using FinanceApp.Services;
using FinanceApp.ViewModels;
using FinanceApp.Views;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace FinanceApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<LocalDbService>();

            builder.Services.AddSingleton<TransactionViewModel>(); 
            builder.Services.AddSingleton<AddTransactionViewModel>();
            builder.Services.AddTransient<DetailTransactionViewModel>();
            builder.Services.AddSingleton<ExpenseChartViewModel>();

            builder.Services.AddSingleton<TransactionsPage>();
            builder.Services.AddSingleton<AddTransactionPage>();
            builder.Services.AddTransient<DetailTransactionPage>();
            builder.Services.AddSingleton<ExpenseChartPage>();


            return builder.Build();
        }
    }
}
