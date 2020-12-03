using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
   [ApiController]
   [Route("api/customers")]
   public class CustomersController : ControllerBase
   {
      private readonly ICustomersProvider CustomersProvider;

      public CustomersController(ICustomersProvider CustomersProvider)
      {
         this.CustomersProvider = CustomersProvider;
      }

      [HttpGet]
      public async Task<IActionResult> GetCustomersAsync()
      {
         var all = await CustomersProvider.GetCustomersAsync();
         if (all.IsSuccess)
         {
            return Ok(all.Customers);
         }
         return NotFound(all.ErrorMessage);
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> GetCustomerAsync(int id)
      {
         var result = await CustomersProvider.GetCustomerAsync(id);
         if (result.IsSuccess)
         {
            return Ok(result.Customer);
         }
         return NotFound(result.ErrorMessage);
      }
   }
}