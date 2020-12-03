using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers
{
   [ApiController]
   [Route("api/Orders")]
   public class OrdersController : ControllerBase
   {
      private readonly IOrdersProvider OrdersProvider;

      public OrdersController(IOrdersProvider OrdersProvider)
      {
         this.OrdersProvider = OrdersProvider;
      }

      [HttpGet]
      public async Task<IActionResult> GetOrdersAsync()
      {
         var all = await OrdersProvider.GetOrdersAsync();
         if (all.IsSuccess)
         {
            return Ok(all.Orders);
         }
         return NotFound(all.ErrorMessage);
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> GetOrderAsync(int id)
      {
         var result = await OrdersProvider.GetOrderAsync(id);
         if (result.IsSuccess)
         {
            return Ok(result.Order);
         }
         return NotFound(result.ErrorMessage);
      }
   }
}