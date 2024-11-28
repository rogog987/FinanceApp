using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FinanceApp.Models;
using FinanceApp.Services;
using Microcharts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkiaSharp;

namespace FinanceApp.ViewModels
{
    public partial class ExpenseChartViewModel : ObservableObject
    {
        private readonly LocalDbService _dbService;

        public ExpenseChartViewModel(LocalDbService dbService)
        {
            _dbService = dbService;                      
            LoadExpenseChartDataCommand = new RelayCommand(LoadExpenseChartData);

            SelectedDate = DateTime.Today;
            LoadExpenseChartData(); 
        }
             
        [ObservableProperty]
        private DateTime selectedDate;
            
        [ObservableProperty]
        private string totalExpenseDisplay;

        [ObservableProperty]
        private DonutChart expenseChart;

        public ICommand LoadExpenseChartDataCommand { get; }
               
        partial void OnSelectedDateChanged(DateTime oldValue, DateTime newValue)
        {
            ExpenseChart = null;            
            LoadExpenseChartDataCommand?.Execute(null);
        }

        private async void LoadExpenseChartData()
        {
            if (_dbService == null) return;

            int year = SelectedDate.Year;
            int month = SelectedDate.Month;

            // Get total expenses and category expenses for the selected month and year
            decimal totalExpense = await _dbService.GetTotalExpensesForMonthAsync(year, month);
            TotalExpenseDisplay = $"Total Expenses: {totalExpense:C}";

            var categoryExpenses = await _dbService.GetMonthlyExpensesByCategoryAsync(year, month);

            // Generate chart entries for the donut chart
            var chartEntries = categoryExpenses.Select(catExp => new ChartEntry((float)catExp.Value)
            {
                Label = catExp.Key,
                ValueLabel = $"{catExp.Value:C}",
                Color = SKColor.Parse(GetColorForCategory(catExp.Key))
                
            }).ToList();

            // Set the chart data
            ExpenseChart = new DonutChart
            {
                Entries = chartEntries,
                HoleRadius = 0,               
                LabelTextSize = 35,
                BackgroundColor = SKColors.Transparent
            };
        }

        // Method to get color based on category
        private string GetColorForCategory(string category)
        {
            return category switch
            {
                "Food" => "#FF6347",
                "Utilities" => "#FFA07A",
                "Insurance" => "#8A2BE2",
                "Rent" => "#FF4500",
                "Traveling" => "#1E90FF",
                "Health" => "#32CD32",
                "Gifts" => "#FFD700",
                "Alcohol" => "#CD5C5C",
                "Pet care" => "#8B4513",
                "Electronics" => "#4B0082",
                "Furniture" => "#DAA520",
                "Donates" => "#C71585",
                "Clothes" => "#20B2AA",
                "Petrol" => "#FF69B4",
                "Renovation" => "#4682B4",
                "Entertainment" => "#6A5ACD",
                "Cigarettes" => "#B22222",
                "Beauty" => "#DB7093",
                _ => "#808080" 
            };

        }
    }
}
