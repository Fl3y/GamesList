using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesList.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        string gameName;

        [ObservableProperty]
        string gamePic;

        [RelayCommand]
        public void test()
        {

        }
    }
}
