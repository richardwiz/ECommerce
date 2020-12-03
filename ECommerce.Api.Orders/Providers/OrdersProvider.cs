using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using ECommerce.Api.Orders.DB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Providers
{
   public class OrdersProvider : IOrdersProvider
   {
      private readonly OrdersDBContext dbContext;
      private readonly ILogger<OrdersProvider> logger;
      private readonly IMapper mapper;

      public OrdersProvider(OrdersDBContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
      {
         this.dbContext = dbContext;
         this.logger = logger;
         this.mapper = mapper;

         SeedData();
      }

      private void SeedData()
      {
         if (!dbContext.Orders.Any())
         {
            var items = new List<DB.OrderItem>();
            items.Add(new DB.OrderItem(55, 45, 6, 23.50M));
            items.Add(new DB.OrderItem(56, 324, 67, 101.00M));
            items.Add(new DB.OrderItem(57, 1, 32, 1.75M));
            items.Add(new DB.OrderItem(58, 567, 12, 43.00M));

            dbContext.Orders.Add(new DB.Order() { Id = 1, CustomerId = 99, OrderDate = DateTime.Now, Total = 0 , Items = items });
            dbContext.Orders.Add(new DB.Order() { Id = 2, CustomerId = 98, OrderDate = DateTime.Now, Total = 0 , Items = items });
            dbContext.Orders.Add(new DB.Order() { Id = 3, CustomerId = 97, OrderDate = DateTime.Now, Total = 0 , Items = items });
            dbContext.Orders.Add(new DB.Order() { Id = 4, CustomerId = 96, OrderDate = DateTime.Now, Total = 0 , Items = items });
            dbContext.SaveChanges();
         }
      }

      public async Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> GetOrderAsync(int id)
      {
         try
         {
            logger?.LogInformation($"Querying Orders with id: {id}");
            var Order = await dbContext.Orders.FirstOrDefaultAsync(p => p.Id == id);
            if (Order != null)
            {
               logger?.LogInformation("Order found");
               var result = mapper.Map<Models.Order>(Order);
               return (true, result, null);
            }
            return (false, null, "Not found");
         }
         catch (Exception ex)
         {
            logger?.LogError(ex.ToString());
            return (false, null, ex.Message);
         }
      }

      public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync()
      {
         try
         {
            logger?.LogInformation("Querying Orders");
            var Orders = await dbContext.Orders.ToListAsync();
            if (Orders != null && Orders.Any())
            {
               logger?.LogInformation($"{Orders.Count} Order(s) found");
               var result = mapper.Map<IEnumerable<Models.Order>>(Orders);
               return (true, result, null);
            }
            return (false, null, "Not found");
         }
         catch (Exception ex)
         {
            logger?.LogError(ex.ToString());
            return (false, null, ex.Message);
         }
      }
   }
}

