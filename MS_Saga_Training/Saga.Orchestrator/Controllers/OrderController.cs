using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Saga.Orchestrator.Models;
using System.Net;
using System.Text;

namespace Saga.Orchestrator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<OrderResponse> Post([FromBody] Order order)
        {
            var request = JsonConvert.SerializeObject(order);

            // create order
            var orderClient = _httpClientFactory.CreateClient("OrderService");
            var orderResponse = await orderClient.PostAsync("/api/order",
                new StringContent(request, Encoding.UTF8, "application/json"));
            var orderId = await orderResponse.Content.ReadAsStringAsync();

            var inventoryId = string.Empty;
            try
            {
                // update Inventory
                var inventoryClient = _httpClientFactory.CreateClient("InventoryService");
                var inventoryResponse = await inventoryClient.PostAsync("/api/inventory",
                    new StringContent(request, Encoding.UTF8, "application/json"));
                if (inventoryResponse.StatusCode != HttpStatusCode.OK)
                    throw new Exception(inventoryResponse.ReasonPhrase);

                inventoryId = await inventoryResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                await orderClient.DeleteAsync($"/api/order/{orderId}");
                return new OrderResponse { Success = false, Reason = $"{ex.Message}" };
            }


            //Send Notification
            var notificationClient = _httpClientFactory.CreateClient("NotificationService");
            var notificationResponse = await notificationClient.PostAsync("/api/notification",
                new StringContent(request, Encoding.UTF8, "application/json"));
            var notificationId = await notificationResponse.Content.ReadAsStringAsync();


            Console.WriteLine($"Order: {orderId}, Inventory: {inventoryId}, Notification: {notificationId}");

            return new OrderResponse { OrderId = orderId };
        }
    }
}
