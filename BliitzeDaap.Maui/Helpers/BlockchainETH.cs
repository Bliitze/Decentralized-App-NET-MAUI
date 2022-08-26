using BliitzeDapp.SmartContract.LocationContract.ContractDefinition;
using BliitzeDapp.SmartContract.LocationContract;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Web3.Accounts;
using Android.Gms.Maps.Model;
using Nethereum.Hex.HexTypes;

namespace BliitzeDaap.Maui.Helpers
{
    public class BlockchainETH
    {
        public TestChain.NethereumTestChain NtestChainNetwork;
        public TestChain.GoerliTestChain GtestChainNetwork;
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        public BlockchainETH()
        {
            NtestChainNetwork = new TestChain.NethereumTestChain();
            GtestChainNetwork = new TestChain.GoerliTestChain();
        }
        public async void DeployContract()
        {
            try
            {
                bool answer = await App.Current.MainPage.DisplayAlert("Deploy New Contract", "Are you sure to deploy the Smart Contract?", "Yes", "No");
                if(answer == true)
                {
                    Application.Current.Dispatcher.Dispatch(async () =>
                    {
                        var privateKey = NtestChainNetwork.PrivateKey;
                        var chainId = NtestChainNetwork.ChainId;
                        var account = new Account(privateKey, chainId);
                        var web3 = new Web3(account,NtestChainNetwork.RpcURL);
                        web3.TransactionManager.UseLegacyAsDefault = true;

                        var deployment = new LocationContractDeployment();
                        var service = new LocationContractService(web3, account.Address);
                        var deploymentHandler = await LocationContractService.DeployContractAndWaitForReceiptAsync(web3, deployment);
                        var receipt = deploymentHandler;


                        Preferences.Clear();
                        // Set a string value:
                        Preferences.Default.Set("contract_address", receipt.ContractAddress);

                        await App.Current.MainPage.DisplayAlert("Contract", "Contract Address: " + receipt.ContractAddress, "0k");
                       
                    });
                   
                   
                }
               
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "0k");
            }
        }
        public  string GetAccountAddress()
        {
            var privateKey = NtestChainNetwork.PrivateKey;
            var chainId = NtestChainNetwork.ChainId;
            var account = new Account(privateKey, chainId);
           
            return account.Address;
        }
        public async Task<string> GetBalance()
        {
            try
            {
                var privateKey = NtestChainNetwork.PrivateKey;
                var chainId = NtestChainNetwork.ChainId;
                var account = new Account(privateKey, chainId);
                var web3 = new Web3(NtestChainNetwork.RpcURL);
                web3.TransactionManager.UseLegacyAsDefault = true;
                
                var balance = await web3.Eth.GetBalance.SendRequestAsync(account.Address);

                return "Balance: " + Web3.Convert.FromWei(balance.Value) + " ETH";
            }           
            catch (Exception ex)
            {
               
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "0k");
                return "";
            }
        }
        public async Task<string> AddLocationContract(string label)
        {
            string strhash = "";
            try
            {
                Application.Current.Dispatcher.Dispatch(async () =>
                {
                    _isCheckingLocation = true;
                    GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                    _cancelTokenSource = new CancellationTokenSource();

                    Microsoft.Maui.Devices.Sensors.Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                    var privateKey = NtestChainNetwork.PrivateKey;
                    var chainId = NtestChainNetwork.ChainId;
                    var account = new Account(privateKey, chainId);
                    var web3 = new Web3(account, NtestChainNetwork.RpcURL);
                    web3.TransactionManager.UseLegacyAsDefault = true;

                    string contractaddress = Preferences.Get("contract_address", "Unknown");

                    var locationservice = new LocationContractService(web3, contractaddress);

                    DateTime now = DateTime.Now;
                    string Time = now.ToString("h:mm tt");
                    string datetime = now.ToString("MM/dd/yyyy h:mm tt");

                    var transactionInput = new AddLocationFunction
                    {
                        FromAddress = account.Address,
                        Gas  = new HexBigInteger(900000),
                        Date = datetime,
                        Longitude = location.Longitude.ToString(),
                        Latitude = location.Latitude.ToString(),
                        Label = label,

                    };

                    var receiptForSetFunctionCall = await locationservice.AddLocationRequestAndWaitForReceiptAsync(transactionInput,null);

                    if (receiptForSetFunctionCall != null)
                    {
                        strhash = receiptForSetFunctionCall.TransactionHash;
                        await Clipboard.Default.SetTextAsync(strhash);
                        var hash = await Clipboard.Default.GetTextAsync();
                        await App.Current.MainPage.DisplayAlert("Copied to Clipboard", "TxtHash: " + hash, "0k");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Null TxtHash", "TxtHash: Null", "0k");
                        strhash = "N/A";
                    }
                });

                return strhash;
            }
            catch (Exception ex)
            {
                strhash = "N/A";
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "0k");
                return strhash;
            }
            

        }
        public async void GetAllLocations()
        {
            try
            {
                Application.Current.Dispatcher.Dispatch(async () =>
                {
                    var privateKey = NtestChainNetwork.PrivateKey;
                var chainId = NtestChainNetwork.ChainId;
                var account = new Account(privateKey, chainId);
                var web3 = new Web3(account, NtestChainNetwork.RpcURL);
                web3.TransactionManager.UseLegacyAsDefault = true;
                string contract = Preferences.Get("contract_address", "Unknown");
                string address = contract;

                var service = new LocationContractService(web3, address);


                var receiptForSetFunctionCall = await service.GetAllLocationsQueryAsync();
                if (receiptForSetFunctionCall != null)
                {
                    var list = receiptForSetFunctionCall.ReturnValue1;
                    App.LocationList = list;
                                       
                }

                });

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "0k");
            }
        }
    }
}
