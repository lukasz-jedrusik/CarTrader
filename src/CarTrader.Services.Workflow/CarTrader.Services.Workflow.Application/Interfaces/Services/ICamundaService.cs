using Camunda.Api.Client;
using Camunda.Api.Client.ExternalTask;
using Camunda.Api.Client.ProcessInstance;
using Camunda.Api.Client.UserTask;
using CarTrader.Services.Workflow.Domain.Models;

namespace CarTrader.Services.Workflow.Application.Interfaces.Services
{
    public interface ICamundaService
    {
        Task<ProcessInstanceWithVariables> StartProcessAsync(Car car, string userId);
        Task<List<UserTaskInfo>> GetCurrentTasksAsync(string processId);
        Task CompleteTaskAsync(string camundaTaskId);
        Task<List<ExternalTaskInfo>> GetCurrentExternalTasksAsync(string processId);
        Task CompleteExternalTaskAsync(
            string camundaTaskId,
            string workerId,
            Dictionary<string, VariableValue> variables = null);
    }
}
