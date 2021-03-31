using System;

namespace NETCore.IntegrationTesting.FunctionApp.Models
{
    public class CreateOrderRequest
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public int CustomerId { get; set; }
    }
}