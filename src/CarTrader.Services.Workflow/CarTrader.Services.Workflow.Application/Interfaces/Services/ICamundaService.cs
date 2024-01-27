using Camunda.Api.Client.ProcessInstance;

namespace CarTrader.Services.Workflow.Application.Interfaces.Services
{
    public interface ICamundaService
    {
        Task<ProcessInstanceWithVariables> StartProcessAsync(Guid carId, string bussinesKey, string userId);
    }
}
