using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BliitzeDaap.Maui.Helpers
{
    public class TestChain
    {
        

        public class NethereumTestChain 
        {
            public string PrivateKey = "0x7580e7fb49df1c861f0050fae31c2224c6aba908e116b8da44ee8cd927b990b0";
            public Int64 ChainId = 444444444500;
            public string RpcURL = "http://testchain.nethereum.com:8545";
        }
        public class GoerliTestChain
        {
            public string PrivateKey = "YOUR_PRIVATE_KEY";
            public Int64 ChainId = 5;
            public string RpcURL = "https://goerli.prylabs.net";
        }
    }
}
