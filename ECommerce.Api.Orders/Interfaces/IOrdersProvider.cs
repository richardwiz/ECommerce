using ECommerce.Api.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Interfaces
{
   public interface IOrdersProvider
   {
      Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync();
      Task<(bool IsSuccess, Order Order, string ErrorMessage)> GetOrderAsync(int customerId);
   }
}
