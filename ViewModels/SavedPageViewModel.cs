using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Data;
using MusicApp.Models;
using System.Collections.ObjectModel;

namespace MusicApp.ViewModels
{
    public partial class SavedPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string name;

        [ObservableProperty]
        ObservableCollection<Item> items = new ObservableCollection<Item>();

        ItemRepository database;

        public SavedPageViewModel()
        {
            database = new ItemRepository();
        }

        [RelayCommand]
        async void Delete(Item item)
        {
            Items.Remove(item);
            await database.DeleteItemAsync(item);
        }

        public async Task InitializeItems()
        {
            Items.Clear();
            foreach(var item in await database.GetItemsAsync())
            {
                Items.Add(item);
            }
        }
    }
}
