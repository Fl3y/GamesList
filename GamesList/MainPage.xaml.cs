
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace GamesList;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnRandomGameSelect(object sender, EventArgs e)
    {
        Show_picked_Game.Text = await ApiRawgGamesRequest();

    }

    public async Task<string> ApiRawgGamesRequest()
    {
        string JString;
        JObject json;
        List<KeyValuePair<string, string>> keyValueListlist = new List<KeyValuePair<string, string>>();
        List<string> games = new List<string>(); 
        List<JToken> list = new List<JToken>();
        var rand  = new Random();
        var standartURI = "https://rawg-video-games-database.p.rapidapi.com/games?";
        var PageSize = "page_size=350000&";
        var apiKey = "key=";
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{standartURI}{PageSize}{apiKey}"),
            Headers =
            {
                { "X-RapidAPI-Key", "" },
                { "X-RapidAPI-Host", "rawg-video-games-database.p.rapidapi.com" },
            },
        };

        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            JString = await response.Content.ReadAsStringAsync();
            json = JObject.Parse(JString);

            int i = rand.Next(0, 20);

            list = json.SelectToken($"results[{i}]").ToList();

            foreach (var item in list)
            {
                JProperty prop = (JProperty)item;
                string key = prop.Name;
                string value = prop.Value.ToString();
                keyValueListlist.Add(new KeyValuePair<string, string>(key, value));
            }
            
        }



        games = (keyValueListlist.Where(pair => pair.Key == "name").Select(pair => pair.Value).ToList());
        

        var gamesToString = string.Join("-", games);

        return gamesToString;
        
    }
}





