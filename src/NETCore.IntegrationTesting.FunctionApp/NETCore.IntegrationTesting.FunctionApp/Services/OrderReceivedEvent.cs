using System;
using NETCore.IntegrationTesting.FunctionApp.Models;

namespace NETCore.IntegrationTesting.FunctionApp.Services
{
    public class OrderReceivedEvent
    {
        public OrderReceivedEvent(Guid orderId, CreateOrderRequest originalOrder)
        {
            OrderId = orderId;
            OriginalOrder = originalOrder ?? throw new ArgumentNullException(nameof(originalOrder));

            OrderTrackingId = $"{originalOrder.CustomerId}_{orderId}";
        }

        public Guid OrderId { get; }
        public CreateOrderRequest OriginalOrder { get; }
        public string OrderTrackingId { get; }
    }
}