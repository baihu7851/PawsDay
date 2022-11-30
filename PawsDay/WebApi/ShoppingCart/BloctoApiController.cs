using Flow.FCL.Models;
using Flow.FCL;
using Flow.Net.Sdk.Core.Cadence;
using Flow.Net.Sdk.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using PawsDay.Models.ShoppingCart.WebApi.BaseModel;
using Blocto.Sdk.Core.Utility;
using CloudinaryDotNet.Actions;
using Flow.Net.Sdk.Core;
using PawsDay.Models.ShoppingCart.WebApi.Enums;
using BaseResult = PawsDay.Models.ShoppingCart.WebApi.BaseModel.BaseResult;

namespace PawsDay.WebApi.ShoppingCart
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BloctoApiController : ControllerBase
    {

        private ILogger<BloctoApiController> _logger;

        private FlowClientLibrary _fcl;

        public BloctoApiController(FlowClientLibrary fcl, ILogger<BloctoApiController> logger)
        {
            _fcl = fcl;
            _logger = logger;
        }

        [HttpGet]
        [Route("login")]
        public async Task<BaseResult> Login()
        {
            _logger.LogInformation("Test serilog.");
            
            var accountProofData = new AccountProofData
            {
                AppId = "com.blocto.flow.unitydemo",
                Nonce = KeyGenerator.GetUniqueKey(32).StringToHex()
            };
            var data = await _fcl.AuthenticateAsync(accountProofData);
            var result = new Dictionary<string, string>
                     {
                         { "Url", data.Url },
                         { "AuthenticationId", data.AuthenticationId }
                     };
            var response = new BaseResult { Body = result };

            return response;
        }

        [HttpGet]
        [Route("login/Result")]
        public async Task<BaseResult> Login(string authenticationId)
        {
            _logger.LogInformation($"AuthenticationId: {authenticationId}");
            var address = await _fcl.AuthenticateResultAsync(authenticationId);



            var apiResult = default(BaseResult);
            if (address == "")
            {
                apiResult = new BaseResult { Response=ApiStatus.StillPending, Body=new object()};
            }
            else
            {
                apiResult = new BaseResult { Response=ApiStatus.Success,Body= new Dictionary<string, string>{{ "Address", $"0x{address}"}}} ;
            }

            return apiResult;
        }

        [HttpPost]
        [Route("transaction/{address}/to/{receiveAddress}/{value}")]
        public async Task<BaseResult> Mutate(string address, string receiveAddress, decimal value)
        {
            _logger.LogInformation($"Address: {address}, ReceivceAddress: {receiveAddress}, Value: {value}");
            var transaction = new FlowTransaction
            {
                Arguments = new List<ICadence>
                                          {
                                              new CadenceNumber(CadenceNumberType.UFix64, $"{value:N8}"),
                                              new CadenceAddress($"{receiveAddress}")
                                          }
            };
            
            var data = await _fcl.MutateAsync(address.Replace("0x",""), transaction);

            return new BaseResult { Response=ApiStatus.Success,Body = new Dictionary<string, string>
                                                     {
                                                         { "Url", data.Url },
                                                         { "AuthorizationId", data.AuthorizationId },
                                                         { "SessionId", data.SessionId }
                                                     }
            } ;
        }

        [HttpPost]
        [Route("transaction/{authorizationId}/{sessionId}")]
        public async Task<BaseResult> Mutate(string authorizationId, string sessionId)
        {
            _logger.LogInformation($"AuthorizationId: {authorizationId}, SessionId: {sessionId}");
            var data = await _fcl.MutateResultAsync(authorizationId, sessionId);


            return new BaseResult
            {
                Response = ApiStatus.Success,
                Body = new Dictionary<string, string>
                                                     {
                                                         { "txId", data },
                                                         { "FlowScanUrl", $"https://testnet.flowscan.org/transaction/{data}" }
                                                     }
            };
        }


        [HttpGet]
        [Route("transaction/result/{txId}")]
        public async Task<BaseResult> GetTxResult(string txId)
        {
            _logger.LogInformation($"TxId: {txId}");
            var result = await _fcl.MetateExecuteResultAsync(txId);
            return new BaseResult { Response=ApiStatus.Success,Body=result};
        }
    }
}
