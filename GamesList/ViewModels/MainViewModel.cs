using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using GamesList;

namespace GamesList.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        [ObservableProperty]
        string rawgGames;


        [ObservableProperty]
        string rawgPics;

        [ObservableProperty]
        string rawgRequis;


        [RelayCommand]
        private async void OnRandomGameSelect()
        {
            await APIConfig.SetList();
            rawgGames = APIConfig.ApiRawgGamesRequest();
            rawgPics = APIConfig.ApiRawgPictureRequest();
            rawgRequis = APIConfig.ApiRawgRequirementsRequst();
        }

        [RelayCommand]
        async Task LearnMore()
        {
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}");
        }


    }
}

