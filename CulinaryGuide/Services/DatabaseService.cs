using CulinaryGuide.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryGuide.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<FavoriteRestaurant>().Wait();
        }

        public Task<List<FavoriteRestaurant>> GetFavoritesAsync()
        {
            return _database.Table<FavoriteRestaurant>().ToListAsync();
        }

        public Task<int> AddFavoriteAsync(FavoriteRestaurant favorite)
        {
            return _database.InsertAsync(favorite);
        }

        public Task<int> RemoveFavoriteAsync(int id)
        {
            return _database.DeleteAsync<FavoriteRestaurant>(id);
        }

        public Task<FavoriteRestaurant> GetFavoriteByNameAsync(string name)
        {
            return _database.Table<FavoriteRestaurant>().Where(f => f.Name == name).FirstOrDefaultAsync();
        }
    }
}