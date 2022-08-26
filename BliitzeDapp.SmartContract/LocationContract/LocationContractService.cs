using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using BliitzeDapp.SmartContract.LocationContract.ContractDefinition;

namespace BliitzeDapp.SmartContract.LocationContract
{
    public partial class LocationContractService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, LocationContractDeployment locationContractDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<LocationContractDeployment>().SendRequestAndWaitForReceiptAsync(locationContractDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, LocationContractDeployment locationContractDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<LocationContractDeployment>().SendRequestAsync(locationContractDeployment);
        }

        public static async Task<LocationContractService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, LocationContractDeployment locationContractDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, locationContractDeployment, cancellationTokenSource);
            return new LocationContractService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public LocationContractService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AddLocationRequestAsync(AddLocationFunction addLocationFunction)
        {
             return ContractHandler.SendRequestAsync(addLocationFunction);
        }

        public Task<TransactionReceipt> AddLocationRequestAndWaitForReceiptAsync(AddLocationFunction addLocationFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addLocationFunction, cancellationToken);
        }

        public Task<string> AddLocationRequestAsync(string date, string longitude, string latitude, string label)
        {
            var addLocationFunction = new AddLocationFunction();
                addLocationFunction.Date = date;
                addLocationFunction.Longitude = longitude;
                addLocationFunction.Latitude = latitude;
                addLocationFunction.Label = label;
            
             return ContractHandler.SendRequestAsync(addLocationFunction);
        }

        public Task<TransactionReceipt> AddLocationRequestAndWaitForReceiptAsync(string date, string longitude, string latitude, string label, CancellationTokenSource cancellationToken = null)
        {
            var addLocationFunction = new AddLocationFunction();
                addLocationFunction.Date = date;
                addLocationFunction.Longitude = longitude;
                addLocationFunction.Latitude = latitude;
                addLocationFunction.Label = label;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addLocationFunction, cancellationToken);
        }

        public Task<GetAllLocationsOutputDTO> GetAllLocationsQueryAsync(GetAllLocationsFunction getAllLocationsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetAllLocationsFunction, GetAllLocationsOutputDTO>(getAllLocationsFunction, blockParameter);
        }

        public Task<GetAllLocationsOutputDTO> GetAllLocationsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetAllLocationsFunction, GetAllLocationsOutputDTO>(null, blockParameter);
        }

        public Task<GetLabelOutputDTO> GetLabelQueryAsync(GetLabelFunction getLabelFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetLabelFunction, GetLabelOutputDTO>(getLabelFunction, blockParameter);
        }

        public Task<GetLabelOutputDTO> GetLabelQueryAsync(string label, BlockParameter blockParameter = null)
        {
            var getLabelFunction = new GetLabelFunction();
                getLabelFunction.Label = label;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetLabelFunction, GetLabelOutputDTO>(getLabelFunction, blockParameter);
        }

        public Task<BigInteger> FindIndexQueryAsync(FindIndexFunction findIndexFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FindIndexFunction, BigInteger>(findIndexFunction, blockParameter);
        }

        
        public Task<BigInteger> FindIndexQueryAsync(string label, BlockParameter blockParameter = null)
        {
            var findIndexFunction = new FindIndexFunction();
                findIndexFunction.Label = label;
            
            return ContractHandler.QueryAsync<FindIndexFunction, BigInteger>(findIndexFunction, blockParameter);
        }

        public Task<LocationsOutputDTO> LocationsQueryAsync(LocationsFunction locationsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<LocationsFunction, LocationsOutputDTO>(locationsFunction, blockParameter);
        }

        public Task<LocationsOutputDTO> LocationsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var locationsFunction = new LocationsFunction();
                locationsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<LocationsFunction, LocationsOutputDTO>(locationsFunction, blockParameter);
        }
    }
}
