using InventoryService.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        [HttpPost]
        public int Post([FromBody] Inventory inventory)
        {
            Console.WriteLine($"Update Inventory for: {inventory.ProductName}");
            return 2;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Console.WriteLine($"Deleted Inventory: {id}");
        }
    }
}
