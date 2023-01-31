using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MusicApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.ObjectModel;
using System.Text;
using static MusicApp.Models.SpotifySearch;

namespace MusicApp.ViewModels;

public partial class ApiPageViewModel : ObservableObject
{
    public Token Token { get; set; }

    public async Task GetTokenAsync()
    {
        #region SecretVault
        string clientID = "63b876f3f9de4102bef1ebf00b328df0";

        string clientSecret = "826b360ef21b4cbc8de3f3942329c8f8";
        #endregion

        string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(clientID + ":" + clientSecret));

        List<KeyValuePair<string, string>> args = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };

        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Basic {auth}");
        HttpContent content = new FormUrlEncodedContent(args);

        HttpResponseMessage resp = await client.PostAsync("https://accounts.spotify.com/api/token", content);
        string msg = await resp.Content.ReadAsStringAsync();

        Token = JsonConvert.DeserializeObject<Token>(msg);
    }

    public SpotifyResult SearchArtistOrSong(string searchWord)
    {
        var client = new RestClient("https://api.spotify.com/v1/search");
        client.AddDefaultHeader("Authorization", $"Bearer {Token.Access_token}");
        var request = new RestRequest($"?q={searchWord}&type=artist", Method.Get);
        var response = client.Execute(request);

        if (response.IsSuccessful)
        {
            var result = JsonConvert.DeserializeObject<SpotifyResult>(response.Content);
            return result;
        }
        else
        {
            return null;
        }

    }

    [ObservableProperty]
    string artist;

    [ObservableProperty]
    ObservableCollection<SpotifyArtist> listArtist = new ObservableCollection<SpotifyArtist>();

    [RelayCommand]
    void Search()
    {

        if (artist == string.Empty)
        {
            //ListArtist.ItemsSource = null;
            ListArtist.Clear();
            return;
        }

        var result = SearchArtistOrSong(artist);

        if (result == null)
        {
            return;
        }

        //var listArtist = new List<SpotifyArtist>();

        foreach (var item in result.artists.items)
        {
            ListArtist.Add(new SpotifyArtist()
            {
                ID = item.id,
                Image = item.images.Any() ? item.images[0].url : "https://user-images.githubusercontent.com/24848110/33519396-7e56363c-d79d-11e7-969b-09782f5ccbab.png",
                Name = item.name,
                Popularity = $"{item.popularity}% popularity",
                Followers = $"{item.followers.total.ToString("N")} followers"
            });
        }
    }
}