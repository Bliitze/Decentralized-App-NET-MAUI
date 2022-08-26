using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BliitzeDapp.SmartContract.LocationContract.ContractDefinition
{
    public partial class Location : LocationBase { }

    public class LocationBase 
    {
        [Parameter("string", "date", 1)]
        public virtual string Date { get; set; }
        [Parameter("string", "longitude", 2)]
        public virtual string Longitude { get; set; }
        [Parameter("string", "latitude", 3)]
        public virtual string Latitude { get; set; }
        [Parameter("string", "label", 4)]
        public virtual string Label { get; set; }
    }
}
