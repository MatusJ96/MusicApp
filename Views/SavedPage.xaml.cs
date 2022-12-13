using MusicApp.ViewModels;

namespace MusicApp.Views;

public partial class SavedPage : ContentPage
{
    private SavedPageViewModel savedPageViewModel;

    public SavedPage()
	{
        InitializeComponent();
        savedPageViewModel = new SavedPageViewModel();
        BindingContext = savedPageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await savedPageViewModel.InitializeItems();
    }
}