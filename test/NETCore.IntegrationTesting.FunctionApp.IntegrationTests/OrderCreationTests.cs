using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NETCore.IntegrationTesting.FunctionApp.Functions;
using NETCore.IntegrationTesting.FunctionApp.Models;
using NETCore.IntegrationTesting.FunctionApp.Services;
using Newtonsoft.Json;
using Xunit;

namespace NETCore.IntegrationTesting.FunctionApp.IntegrationTests
{
    [Collection(TestsCollection.Name)]
    public class OrderCreationSubcutaneousTests
    {
        private readonly CreateOrderFunction _sut;

        public OrderCreationSubcutaneousTests(TestHost testHost)
        {
            _sut = new CreateOrderFunction(testHost.ServiceProvider.GetRequiredService<IOrderProcessor>());
        }

        [Fact]
        public async Task Test()
        {
            // arrange
            var createOrderRequest = new CreateOrderRequest()
            {
                Price = 1.0m,
                Quantity = 10,
                CustomerId = 1,
                OrderDate = DateTime.UtcNow,
            };

            var json = JsonConvert.SerializeObject(createOrderRequest);
            var httpContext = new DefaultHttpContext();

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            httpContext.Request.Body = stream;
            httpContext.Request.ContentLength = stream.Length;

            var req = new DefaultHttpRequest(httpContext);

            // act
            var response = await _sut.PostCreateOrder(req, null);

            // assert
            Assert.NotNull(response);

            var createOrderResponse = (response as OkObjectResult)?.Value as CreateOrderResponse;

            //some more tests if needed on create order response
        }
    }
}