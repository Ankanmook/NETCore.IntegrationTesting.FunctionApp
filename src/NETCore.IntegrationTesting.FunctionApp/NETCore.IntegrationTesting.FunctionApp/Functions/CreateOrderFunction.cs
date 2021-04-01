using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NETCore.IntegrationTesting.FunctionApp.Models;
using NETCore.IntegrationTesting.FunctionApp.Services;
using Newtonsoft.Json;

namespace NETCore.IntegrationTesting.FunctionApp.Functions
{
    public class CreateOrderFunction
    {
        readonly IOrderProcessor _orderProcessor;

        public CreateOrderFunction(IOrderProcessor orderProcessor)
        {
            _orderProcessor = orderProcessor;
        }

        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code",
            In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string),
            Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string),
            Description = "The OK response")]
        [FunctionName("CreateOrder")]
        public async Task<IActionResult> PostCreateOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "CreateOrder")]
            HttpRequest req, ILogger log)
        {
            log?.LogInformation("Received a create order request");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(requestBody))
            {
                return new BadRequestObjectResult("Request is empty");
            }

            log?.LogInformation("Request body");
            log?.LogInformation(requestBody);


            var request = JsonConvert.DeserializeObject<CreateOrderRequest>(requestBody);
            if (request == null)
            {
                return new BadRequestObjectResult("Invalid request format");
            }

            var orderReceived = new OrderReceivedEvent(Guid.NewGuid(), request);
            var orderType = _orderProcessor.GetOrderNature(orderReceived);


            var response = new CreateOrderResponse(orderReceived.OrderTrackingId, orderType);

            return new OkObjectResult(response);
        }
    }
}

