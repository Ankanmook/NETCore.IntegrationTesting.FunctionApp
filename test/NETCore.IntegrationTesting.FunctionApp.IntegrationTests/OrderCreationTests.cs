using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NETCore.IntegrationTesting.FunctionApp.Functions;
using NETCore.IntegrationTesting.FunctionApp.Models;
using NETCore.IntegrationTesting.FunctionApp.Services;
using Newtonsoft.Json;
using Xunit;
using Xunit.Sdk;

namespace NETCore.IntegrationTesting.FunctionApp.IntegrationTests
{

    [Collection(nameof(TestCollection))]
    public class OrderCreationTests
    {
        private readonly CreateOrderFunction _sut;
        public OrderCreationTests(TestHost testHost)
        {
            _sut = new CreateOrderFunction(testHost.ServiceProvider.GetRequiredService<IOrderProcessor>());
        }


       
    }
}
