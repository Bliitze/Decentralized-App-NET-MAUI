using BliitzeDaap.Maui.Helpers;

namespace BliitzeDaap.Maui.Views;

public partial class AddLocationPage : ContentPage
{
    public BlockchainETH blockchainETH = new BlockchainETH();
    public AddLocationPage()
	{
		InitializeComponent();
	}

	private async void BtnAddlocation_Clicked(object sender, EventArgs e)
	{
		if(labelPosition.Text !="")
		{
			await blockchainETH.AddLocationContract(labelPosition.Text);

        }
		
	}
}