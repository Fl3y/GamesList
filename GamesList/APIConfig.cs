using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesList
{
    public static class APIConfig
    {
        static int i;


        public static List<KeyValuePair<string, string>> listReturnedByApi = new List<KeyValuePair<string, string>>();

        public static async Task SetList()
        {
            listReturnedByApi = await ConnectToDbAndReturnJson();
        }


        public static string ApiRawgGamesRequest()
        {

            List<string> games =  new List<string>();

            games = (listReturnedByApi.Where(pair => pair.Key == "name").Select(pair => pair.Value).ToList());

            var gamesToString = string.Join("-", games);

            return gamesToString;

        }

        public static string ApiRawgPictureRequest()
        {

            List<string> pictures = new List<string>();

            pictures =  (listReturnedByApi.Where(pair => pair.Key == "background_image").Select(pair => pair.Value).ToList());

            var pictureUrlToString =  string.Join("-", pictures);

            return pictureUrlToString;
        }

        public static string ApiRawgRequirementsRequst()
        {
            List<string> requs = new List<string>();

            requs =  (listReturnedByApi.Where(pair => pair.Key == "metacritic").Select(pair => pair.Value).ToList());

            var requsString =  string.Join("-", requs);

            return requsString;
        }

        public static async Task<List<KeyValuePair<string, string>>> ConnectToDbAndReturnJson()
        {
            string JString;
            JObject json;
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
                    i = rand.Next(0, 20);
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
