
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using GamesList.ViewModels;
using Newtonsoft.Json.Linq;

namespace GamesList;

public partial class MainPage : ContentPage
{

    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }


}





