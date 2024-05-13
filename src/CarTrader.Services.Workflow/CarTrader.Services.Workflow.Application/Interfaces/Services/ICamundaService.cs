using Camunda.Api.Client.ExternalTask;
using Camunda.Api.Client.ProcessInstance;
using Camunda.Api.Client.UserTask;

namespace CarTrader.Services.Workflow.Application.Interfaces.Services
{
    public interface ICamundaService
    {
        Task<ProcessInstanceWithVariables> StartProcessAsync(Guid carId, string bussinesKey, string userId);
        Task<List<UserTaskInfo>> GetCurrentTasksAsync(string processId);
        Task CompleteTaskAsync(string camundaTaskId);
        Task<List<ExternalTaskInfo>> GetCurrentExternalTasksAsync(string processId);
        Task CompleteExternalTaskAsync(string camundaTaskId);
    }
}
