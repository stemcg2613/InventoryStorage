using InventoryStorage.DataAccess;
using InventoryStorage.Models;

namespace InventoryStorage
{
    public partial class MainPage : ContentPage
    {
        private readonly ItemDataAccess itemDataAccess;

        public MainPage()
        {
            InitializeComponent();

            itemDataAccess = new ItemDataAccess();

            LoadItems();
        }

        private async void LoadItems()
        {
            ItemsCollectionView.ItemsSource =
                await itemDataAccess.GetItemsAsync();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Make sure Item ID is entered
            if (string.IsNullOrWhiteSpace(ItemIDEntry.Text))
            {
                await DisplayAlert(
                    "Validation Error",
                    "Please enter an Item ID.",
                    "OK");

                return;
            }

            // Make sure Item Name is entered
            if (string.IsNullOrWhiteSpace(ItemNameEntry.Text))
            {
                await DisplayAlert(
                    "Validation Error",
                    "Please enter an Item Name.",
                    "OK");

                return;
            }

            // Make sure Item Description is entered
            if (string.IsNullOrWhiteSpace(ItemDescriptionEntry.Text))
            {
                await DisplayAlert(
                    "Validation Error",
                    "Please enter an Item Description.",
                    "OK");

                return;
            }

            Item item = new Item
            {
                ItemID = ItemIDEntry.Text.Trim(),
                ItemName = ItemNameEntry.Text.Trim(),
                ItemDescription = ItemDescriptionEntry.Text.Trim()
            };

            try
            {
                await itemDataAccess.SaveItemAsync(item);

                // Clear the entry fields
                ItemIDEntry.Text = string.Empty;
                ItemNameEntry.Text = string.Empty;
                ItemDescriptionEntry.Text = string.Empty;

                // Refresh the displayed items
                LoadItems();

                await DisplayAlert(
                    "Success",
                    "Item saved successfully.",
                    "OK");
            }
            catch (SQLite.SQLiteException)
            {
                await DisplayAlert(
                    "Error",
                    "That Item ID already exists. Please use a different Item ID.",
                    "OK");
            }
        }
    }
}