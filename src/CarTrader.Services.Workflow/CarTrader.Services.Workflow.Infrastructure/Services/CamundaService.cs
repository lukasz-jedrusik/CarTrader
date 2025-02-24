using System.Text;
using Camunda.Api.Client;
using Camunda.Api.Client.ExternalTask;
using Camunda.Api.Client.ProcessDefinition;
using Camunda.Api.Client.ProcessInstance;
using Camunda.Api.Client.UserTask;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Domain.Models;
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

        public Task<ProcessInstanceWithVariables> StartProcessAsync(Car car, string userId)
        {
            var processParams = new StartProcessInstance()
                .SetVariable("createdBy", VariableValue.FromObject(userId))
                .SetVariable("carId", VariableValue.FromObject(car.Id.ToString()))
                .SetVariable("manufacturer", VariableValue.FromObject(car.Manfacturer))
                .SetVariable("model", VariableValue.FromObject(car.Model))
                .SetVariable("year", VariableValue.FromObject(car.YearOfProduction))
                .SetVariable("mileage", VariableValue.FromObject(car.Mileage))
                .SetVariable("VIN", VariableValue.FromObject(car.VIN))
                ;

            processParams.BusinessKey = $"{car.Year}/{car.Number}";

            return _camunda.ProcessDefinitions.ByKey(_configuration["Camunda:ProcessName"]).StartProcessInstance(processParams);
        }

        public async Task<List<UserTaskInfo>> GetCurrentTasksAsync(string processId)
        {
            var groupTaskQuery = new TaskQuery
            {
                ProcessInstanceId = processId
            };

            var tasks = await _camunda.UserTasks.Query(groupTaskQuery).List();
            return tasks;
        }

        public async Task CompleteTaskAsync(string camundaTaskId)
        {
            var completeTask = new CompleteTask();
            await _camunda.UserTasks[camundaTaskId].Complete(completeTask);
        }

        public async Task<List<ExternalTaskInfo>> GetCurrentExternalTasksAsync(string processId)
        {
            var groupTaskQuery = new ExternalTaskQuery
            {
                ProcessInstanceId = processId
            };

            var tasks = await _camunda.ExternalTasks.Query(groupTaskQuery).List();
            return tasks;
        }

        public async Task CompleteExternalTaskAsync(string camundaTaskId)
        {
            var completeTask = new CompleteExternalTask();
            await _camunda.ExternalTasks[camundaTaskId].Complete(completeTask);
        }
    }
}