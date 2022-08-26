using BliitzeDapp.SmartContract.LocationContract.ContractDefinition;

namespace BliitzeDaap.Maui
{
    public partial class App : Application
    {
        public static List<BliitzeDapp.SmartContract.LocationContract.ContractDefinition.Location> LocationList = new List<BliitzeDapp.SmartContract.LocationContract.ContractDefinition.Location>();
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}