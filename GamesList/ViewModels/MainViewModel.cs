﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;

namespace GamesList.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        public int i;
        public List<KeyValuePair<string, string>> listReturnedByApi = new List<KeyValuePair<string, string>>();

        public JObject json;



        [ObservableProperty]
        string rawgGames;


        [ObservableProperty]
        string rawgPics;

        [ObservableProperty]
        string rawgReleaseDate;

        [ObservableProperty]
        string rawgMetacritic;


        [RelayCommand]
        private async void OnRandomGameSelect()
        {
            listReturnedByApi = await ConnectToDbAndReturnJson();

            RawgGames = ApiRawgGamesRequest(listReturnedByApi);

            RawgPics = ApiRawgPictureRequest(listReturnedByApi);

            RawgReleaseDate = ApiRawgReleaseDateRequest(listReturnedByApi);

            RawgMetacritic = ApiRawgMetaCriticRequst(listReturnedByApi);
        }

        [RelayCommand]
        Task Navigate() => Shell.Current.GoToAsync($"{nameof(DetailPage)}?Game={rawgGames}&Pic={rawgPics}&Release={rawgReleaseDate}&Critic={rawgMetacritic}");
        



        public string ApiRawgGamesRequest(List<KeyValuePair<string, string>> l)
        {
            List<string> games = new List<string>();

            

            games = (l.Where(pair => pair.Key == "name").Select(pair => pair.Value).ToList());

            var gamesToString = string.Join("-", games);

            return gamesToString;

        }

        public string ApiRawgPictureRequest(List<KeyValuePair<string, string>> l)
        {
            List<string> pictures = new List<string>();

            pictures = (l.Where(pair => pair.Key == "background_image").Select(pair => pair.Value).ToList());

            var pictureUrlToString = string.Join("-", pictures);

            return pictureUrlToString;
        }

        public string ApiRawgReleaseDateRequest(List<KeyValuePair<string, string>> l)
        {
            List<string> release = new List<string>();

            release = (l.Where(pair => pair.Key == "released").Select(pair => pair.Value).ToList());

            var releaseString = string.Join("-", release);

            return releaseString;
        }

        public string ApiRawgMetaCriticRequst(List<KeyValuePair<string, string>> l)
        {
            List<string> critcs = new List<string>();

            critcs = (l.Where(pair => pair.Key == "metacritic").Select(pair => pair.Value).ToList());

            var criticsString = string.Join("-", critcs);

            return criticsString;
        }

        public async Task<List<KeyValuePair<string, string>>> ConnectToDbAndReturnJson()
        {
            string JString;
         
            List<JToken> list = new List<JToken>();
            List<KeyValuePair<string, string>> keyValueList = new List<KeyValuePair<string, string>>();
            var standartURI = "https://rawg-video-games-database.p.rapidapi.com/games?";
            var PageSize = "page_size=10000000&";
            var apiKey = "key=3af37b336430414aa6703049eb9ff1aa";
            var rand = new Random();

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{standartURI}{PageSize}{apiKey}"),
                Headers =
            {
                { "X-RapidAPI-Key", "308bccc392mshb024c99fad461f1p1d305djsn7bc6f1457146" },
                { "X-RapidAPI-Host", "rawg-video-games-database.p.rapidapi.com" },
            },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                i = rand.Next(0, 1000);

                JString = await response.Content.ReadAsStringAsync();
                json = JObject.Parse(JString);
                try
                {
                    list = json.SelectToken($"results[{i}]").ToList();
                }
                catch (Exception ex)
                {
                    i = rand.Next(0,20);
                    list = json.SelectToken($"results[{i}]").ToList();
                }
            }

            foreach (var item in list)
            {
                JProperty prop = (JProperty)item;
                string key = prop.Name;
                string value = prop.Value.ToString();
                keyValueList.Add(new KeyValuePair<string, string>(key, value));

            }
            return keyValueList;

        }
    }
}
