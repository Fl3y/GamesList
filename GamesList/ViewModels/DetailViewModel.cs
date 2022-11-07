using CommunityToolkit.Mvvm.Collections;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GamesList.ViewModels
{
    [QueryProperty("Text", "Text")]
    public partial class DetailViewModel: ObservableObject
    {
        [ObservableProperty]
        string text;

    }
}
