using Microsoft.AspNetCore.Mvc;
using CarTrader.Services.Workflow.Application.Interfaces.Services;
using CarTrader.Services.Workflow.Application.Requests;
using CarTrader.Services.Workflow.Application.Responses;

namespace CarTrader.Services.Workflow.Api.Controllers
{
    public class TestController : Controller
    {
        private readonly IMessagePublisher _messagePublisher;

        public TestController(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        [HttpPost]
        [Route("[controller]")]
        public async Task Post()
        {
            // Wywołanie metody SendRequestAsync
            var response = await _messagePublisher.SendRequestAsync<SetParkingPlaceRequest, SetParkingPlaceResponse>(
                queue: "CarTraderSetParkingPlaceQueueResponses",
                exchange: "CarTrader.Cars",
                routingKey: "SetParkingPlaceRequestResponse",
                request: new SetParkingPlaceRequest(Guid.NewGuid(), "BK-1"),
                handleResponse: HandleResponse
            );
        }

        // Metoda obsługująca otrzymaną odpowiedź
        private async Task HandleResponse(SetParkingPlaceResponse response)
        {
            Console.WriteLine("Received response: " + response);
            // Obsługa otrzymanej odpowiedzi
            await Task.CompletedTask;
        }
    }
}