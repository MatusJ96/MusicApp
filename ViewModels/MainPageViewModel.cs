using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Data;
using MusicApp.Models;

namespace MusicApp.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string name;

        ItemRepository database;
        public MainPageViewModel()
        {
            database = new ItemRepository();
        }

        [RelayCommand]
        async void Add()
        {
            var myItem = new Item();
            myItem.Name = name;
            if (string.IsNullOrWhiteSpace(myItem.Name))
            {
                return;
            }

            await database.SaveItemAsync(myItem);
        }

    }
}