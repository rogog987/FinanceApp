using FinanceApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Services
{
    public class LocalDbService
    {
        private const string DB_NAME = "Finance.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Category>();
            _connection.CreateTableAsync<Transaction>();
        }

        public async Task<List<Transaction>> GetTransactions()
        {
            return await _connection.Table<Transaction>().ToListAsync();
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
    }
}
