using BliitzeDaap.Maui.Helpers;

namespace BliitzeDaap.Maui
{
    public partial class AppShell : Shell
    {
        public BlockchainETH blockchainETH = new BlockchainETH();
        public AppShell()
        {
            InitializeComponent();
            string contractaddress = Preferences.Get("contract_address", "Unknown");
            ContractaddressLbETH.Text = "Contract: " + contractaddress;
        }

        private async void openBrowserbtn_Clicked(object sender, EventArgs e)
        {
            string accountaddress = blockchainETH.GetAccountAddress();
            string url = "https://goerli.etherscan.io/address/" + accountaddress;
            Uri uri = new Uri(url);
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}