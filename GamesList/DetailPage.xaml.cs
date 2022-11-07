using GamesList.ViewModels;

namespace GamesList;

public partial class DetailPage : ContentPage
{



	public DetailPage(DetailViewModel vm)
	{
		InitializeComponent();
		BindingContext= vm;
	}


}