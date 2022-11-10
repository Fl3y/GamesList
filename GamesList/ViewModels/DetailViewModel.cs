using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;
using GamesList.ViewModels;

namespace GamesList.ViewModels
{
    public partial class DetailViewModel: ObservableObject
    {
        [ObservableProperty]
        string game;

        DetailViewModel()
        {
            
        }


    }


}
