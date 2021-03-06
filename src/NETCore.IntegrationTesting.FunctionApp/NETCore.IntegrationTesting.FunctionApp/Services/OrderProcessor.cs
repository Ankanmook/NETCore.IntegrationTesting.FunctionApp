using System;

namespace NETCore.IntegrationTesting.FunctionApp.Services
{
    public class OrderProcessor : IOrderProcessor
    {
        public OrderType GetOrderNature(OrderReceivedEvent data)
        {
            if (data?.OriginalOrder == null)
            {
                throw new ArgumentNullException("Invalid order created event.");
            }

            if (data.OriginalOrder.CustomerId % 2 == 0)
            {
                return OrderType.Vip;
            }

            return OrderType.Normal;
        }
    }
}