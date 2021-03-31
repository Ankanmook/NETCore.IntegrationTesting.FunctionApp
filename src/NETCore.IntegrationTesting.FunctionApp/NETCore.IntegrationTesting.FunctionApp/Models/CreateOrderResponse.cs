using NETCore.IntegrationTesting.FunctionApp.Services;
namespace NETCore.IntegrationTesting.FunctionApp.Models
{
    public class CreateOrderResponse
    {
        public CreateOrderResponse(string orderId, OrderType orderType)
        {
            OrderId = orderId;
            OrderType = orderType;
        }

        public string OrderId { get; }

        public OrderType OrderType { get; }
    }
}