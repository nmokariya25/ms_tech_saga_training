using Microsoft.AspNetCore.Mvc;
using NotificationService.Models;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        [HttpPost]
        public int Post([FromBody] Notification notification)
        {
            Console.WriteLine($"Sent notification for: {notification.ProductName}");
            return 3;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Console.WriteLine($"Sent rollback transaction notification: {id}");
        }
    }
}
