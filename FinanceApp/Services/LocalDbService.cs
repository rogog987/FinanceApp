using FinanceApp.Models;
using SQLite;

namespace FinanceApp.Services
{
    public class LocalDbService
    {
        private const string DB_NAME = "Finance.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
        }

        public async Task InitializeAsync()
        {
            await _connection.CreateTableAsync<Category>();
            await _connection.CreateTableAsync<Transaction>();
            await SeedCategoriesAsync();
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _connection.Table<Category>().ToListAsync();
        }
        public async Task<List<Transaction>> GetTransactions()
        {
            var transactions = await _connection.Table<Transaction>().ToListAsync();

            // Fetch all categories
            var categories = await _connection.Table<Category>().ToListAsync();

            // Map each transaction to its category
            foreach (var transaction in transactions)
            {
                transaction.Category = categories.FirstOrDefault(c => c.Id == transaction.CategoryId);
            }

            return transactions.OrderByDescending(t => t.Date).ThenByDescending(t => t.Id).ToList();
        }

        public async Task CreateTransaction(Transaction transaction)
        {
            await _connection.InsertAsync(transaction);
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            await _connection.UpdateAsync(transaction);
        }

        public async Task DeleteTransaction(Transaction transaction)
        {
            await _connection.DeleteAsync(transaction);
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
           
            // Query the Category table for a specific category by ID
            var category = await _connection.Table<Category>()
                                    .Where(c => c.Id == categoryId)
                                    .FirstOrDefaultAsync();

            return category;
        }

        public async Task<Transaction?> GetTransactionById(int id)
        {
            return await _connection.FindAsync<Transaction>(id);
        }

        public async Task<Dictionary<string, decimal>> GetMonthlyExpensesByCategoryAsync(int year, int month)
        {
            var transactions = await GetTransactions();
            var monthlyExpenses = transactions
                .Where(t => t.Date.Year == year && t.Date.Month == month && t.Category.IsExpense)
                .GroupBy(t => t.Category.Name)
                .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));

            return monthlyExpenses;
        }

        public async Task<decimal> GetTotalExpensesForMonthAsync(int year, int month)
        {
            var transactions = await GetTransactions();
            return transactions
                .Where(t => t.Date.Year == year && t.Date.Month == month && t.Category.IsExpense)
                .Sum(t => t.Amount);
        }


        private async Task SeedCategoriesAsync()
        {
            var existingCategories = await _connection.Table<Category>().ToListAsync();
            if (existingCategories.Count == 0)
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Food", IsExpense = true },
                    new Category { Name = "Utilities", IsExpense = true },
                    new Category { Name = "Insurance", IsExpense = true },
                    new Category { Name = "Rent", IsExpense = true },
                    new Category { Name = "Traveling", IsExpense = true },
                    new Category { Name = "Health", IsExpense = true },
                    new Category { Name = "Gifts", IsExpense = true },
                    new Category { Name = "Alcohol", IsExpense = true },
                    new Category { Name = "Pet care", IsExpense = true },
                    new Category { Name = "Electronics", IsExpense = true },
                    new Category { Name = "Furniture", IsExpense = true },
                    new Category { Name = "Donates", IsExpense = true },
                    new Category { Name = "Clothes", IsExpense = true },
                    new Category { Name = "Petrol", IsExpense = true },
                    new Category { Name = "Renovation", IsExpense = true },
                    new Category { Name = "Entertainment", IsExpense = true },
                    new Category { Name = "Cigarettes", IsExpense = true },
                    new Category { Name = "Beauty", IsExpense = true },

                    new Category { Name = "Salary", IsExpense = false },
                    new Category { Name = "Gift", IsExpense = false },
                    new Category { Name = "Investment ", IsExpense = false},
                    new Category { Name = "Part-time ", IsExpense = false},
                    new Category { Name = "Other", IsExpense = false }
                };
                await _connection.InsertAllAsync(categories);
            }
        }
    }
}