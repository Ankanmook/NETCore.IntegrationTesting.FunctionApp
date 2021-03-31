namespace NETCore.IntegrationTesting.FunctionApp.Services
{
    public interface IOrderProcessor
    {
        OrderType GetOrderNature(OrderReceivedEvent data);
    }
}