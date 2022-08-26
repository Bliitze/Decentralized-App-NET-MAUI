using BliitzeDaap.Maui.Helpers;

namespace BliitzeDaap.Maui.Views;

public partial class MapPage : ContentPage
{
    public BlockchainETH blockchainETH = new BlockchainETH();
    public MapPage()
	{
		InitializeComponent();
        
    }

    protected override void OnAppearing()
    {
        blockchainETH.GetAllLocations();
        Content = new MapView();
        base.OnAppearing();
    }
}