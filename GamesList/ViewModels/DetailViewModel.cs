using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WikiDotNet;
using System;

namespace GamesList.ViewModels
{
    [QueryProperty(nameof(Game), nameof(Game))]
    [QueryProperty(nameof(Pic), nameof(Pic))]
    [QueryProperty(nameof(Release), nameof(Release))]
    [QueryProperty(nameof(Critic), nameof(Critic))]
    
    public partial class DetailViewModel: ObservableObject
    {


        [ObservableProperty]
        string game;

        [ObservableProperty]
        string pic;

        [ObservableProperty]
        string release;

        [ObservableProperty]
        string critic;

        [ObservableProperty]
        string wikiEntry;


        private async Task<string> GetWikiEntry()
        {
            string searchString = Game;

            WikiSearcher searcher = new();
            WikiSearchSettings settings = new() { RequestId = "Request ID", ResultLimit = 5, ResultOffset = 2, Language = "en" };

            WikiSearchResponse response =  searcher.Search(searchString, settings);

            WikiSearchResult result = response.Query.SearchResults[0];

            return WikiEntry = result.Preview;
        }

        async partial void OnGameChanged(string value)
        {
            WikiEntry = await GetWikiEntry();
            
        }
    }
}
