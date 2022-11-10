using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GamesList.ViewModels
{
    [QueryProperty(nameof(Game), nameof(Game))]

    
    public partial class DetailViewModel: ObservableObject
    {

        [ObservableProperty]
        string game;

        [ObservableProperty]
        string pic;

        [ObservableProperty]
        string requis;




    }
}
