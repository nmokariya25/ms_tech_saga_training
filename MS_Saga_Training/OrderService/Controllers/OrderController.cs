using Microsoft.AspNetCore.Mvc;
using OrderService.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public int Post([FromBody] Order order)
        {
            Console.WriteLine($"Created new order: {order.ProductName}");
            return 1;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Console.WriteLine($"Deleted order: {id}");
        }

    }
}
