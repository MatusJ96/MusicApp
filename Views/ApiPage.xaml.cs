using CommunityToolkit.Mvvm.ComponentModel;
using MusicApp.Models;
using MusicApp.ViewModels;
using System.Collections.ObjectModel;

namespace MusicApp.Views;

public partial class ApiPage : ContentPage
{
	private ApiPageViewModel apiPageViewModel;

	public ApiPage()
	{
		InitializeComponent();
		apiPageViewModel= new ApiPageViewModel();
        BindingContext = apiPageViewModel;

        _ = apiPageViewModel.GetTokenAsync();
    }
}