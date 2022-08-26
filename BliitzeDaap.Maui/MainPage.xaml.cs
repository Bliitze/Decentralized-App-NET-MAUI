using BliitzeDapp.SmartContract.LocationContract;
using BliitzeDapp.SmartContract.LocationContract.ContractDefinition;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Microsoft.Maui.Storage;
using BliitzeDaap.Maui.Helpers;
using Nethereum.Contracts.Standards.ERC1155.ContractDefinition;
using Nethereum.Util;

namespace BliitzeDaap.Maui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public BlockchainETH blockchainETH = new BlockchainETH();
        public TestChain.NethereumTestChain NtestChainNetwork = new TestChain.NethereumTestChain();
        public TestChain.GoerliTestChain GtestChainNetwork = new TestChain.GoerliTestChain();
        public MainPage()
        {
            InitializeComponent();

            AccountaddressLb.Text = blockchainETH.GetAccountAddress();
            zxingbarcodeview.Value = blockchainETH.GetAccountAddress();
            
            GetBalance();
            
        }

        public async void GetBalance()
        {
            try
            {
                BalanceLb.Text = await blockchainETH.GetBalance();
                

            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "0k");
                
            }
        }

      
        private async void AddBtn_Clicked(object sender, EventArgs e)
        {
            //var somestring = await AddLocationContract();
        }

        private async void GetBtn_Clicked(object sender, EventArgs e)
        {
            //GetAllLocations();
            await Navigation.PushAsync(new Views.MapPage());
        }



        private async void BtnDeployContract_Clicked(object sender, EventArgs e)
        {
            blockchainETH.DeployContract();
            string contractaddress = Preferences.Get("contract_address", "Unknown");
            
        }
    }
}