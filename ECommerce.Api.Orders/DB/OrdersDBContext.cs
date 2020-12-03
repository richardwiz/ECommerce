using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.DB
{
   public class OrdersDBContext : DbContext
   {
      public DbSet<Order> Orders { get; set; }

      public OrdersDBContext(DbContextOptions options) : base(options)
      {

      }
   }
}
