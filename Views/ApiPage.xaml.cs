using MusicApp.Models;
using MusicApp.ViewModels;

namespace MusicApp.Views;

public partial class ApiPage : ContentPage
{
	private ApiPageViewModel apiPageViewModel;

	public ApiPage()
	{
		InitializeComponent();
		apiPageViewModel= new ApiPageViewModel();
        BindingContext = apiPageViewModel;

        Task.Run(async() => await ApiPageViewModel.GetTokenAsync());
    }

    void OnEntryCompleted(object sender, EventArgs e)
    {
        if (txtSearch.Text == string.Empty)
        {
            ListArtist.ItemsSource = null;
            return;
        }

        var result = ApiPageViewModel.SearchArtistOrSong(txtSearch.Text);

        if (result == null)
        {
            return;
        }

        var listArtist = new List<SpotifyArtist>();

        foreach (var item in result.artists.items)
        {
            listArtist.Add(new SpotifyArtist()
            {
                ID = item.id,
                Image = item.images.Any() ? item.images[0].url : "https://user-images.githubusercontent.com/24848110/33519396-7e56363c-d79d-11e7-969b-09782f5ccbab.png",
                Name = item.name,
                Popularity = $"{item.popularity}% popularidad",
                Followers = $"{item.followers.total.ToString("N")} seguidores"
            });
        }

        ListArtist.ItemsSource = listArtist;
    }
}