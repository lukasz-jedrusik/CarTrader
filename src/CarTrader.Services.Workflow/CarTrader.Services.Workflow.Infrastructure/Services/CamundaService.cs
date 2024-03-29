using System.Text;
using Camunda.Api.Client;
using Camunda.Api.Client.ProcessDefinition;
using Camunda.Api.Client.ProcessInstance;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace CarTrader.Services.Workflow.Infrastructure.Services
{
    public class CamundaService : ICamundaService
    {
        private readonly IConfiguration _configuration;
        private readonly CamundaClient _camunda;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public CamundaService(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory
            )
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;

            _httpClient = _httpClientFactory.CreateClient("camundaHttpClient");

            string credentials = _configuration["Camunda:Username"] + ":" + _configuration["Camunda:Password"];
            var byteArray = Encoding.ASCII.GetBytes(credentials);
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(byteArray));

            _camunda = CamundaClient.Create(_httpClient);
        }

        public Task<ProcessInstanceWithVariables> StartProcessAsync(Guid carId, string bussinesKey, string userId)
        {
            var processParams = new StartProcessInstance()
                .SetVariable("createdBy", VariableValue.FromObject(userId))
                .SetVariable("carId", VariableValue.FromObject(carId));

            processParams.BusinessKey = bussinesKey;

            return _camunda.ProcessDefinitions.ByKey(_configuration["Camunda:ProcessName"]).StartProcessInstance(processParams);
        }
    }
}