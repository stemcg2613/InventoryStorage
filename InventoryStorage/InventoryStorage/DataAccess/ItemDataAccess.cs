using SQLite;
using InventoryStorage.Models;

namespace InventoryStorage.DataAccess
{
    public class ItemDataAccess
    {
        private SQLiteAsyncConnection? database;

        private async Task Init()
        {
            if (database is not null)
                return;

            string databasePath = Path.Combine(
                FileSystem.AppDataDirectory,
                "Inventory.db3");

            database = new SQLiteAsyncConnection(databasePath);

            await database.CreateTableAsync<Item>();
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            await Init();
            return await database!.Table<Item>().ToListAsync();
        }

        public async Task<int> SaveItemAsync(Item item)
        {
            await Init();
            return await database!.InsertAsync(item);
        }
    }
}