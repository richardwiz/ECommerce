using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using ECommerce.Api.Customers.DB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
   public class CustomersProvider : ICustomersProvider
   {
      private readonly CustomersDBContext dbContext;
      private readonly ILogger<CustomersProvider> logger;
      private readonly IMapper mapper;

      public CustomersProvider(CustomersDBContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
      {
         this.dbContext = dbContext;
         this.logger = logger;
         this.mapper = mapper;

         SeedData();
      }

      private void SeedData()
      {
         if (!dbContext.Customers.Any())
         {
            dbContext.Customers.Add(new DB.Customer() { Id = 1, Name = "Richard Wiz", Address = "48 Geek St" });
            dbContext.Customers.Add(new DB.Customer() { Id = 2, Name = "Sophia Wiz", Address = "10 Groovy Pl" });
            dbContext.Customers.Add(new DB.Customer() { Id = 3, Name = "Amara Wiz", Address = "8 Awesome Terrace" });
            dbContext.Customers.Add(new DB.Customer() { Id = 4, Name = "Caroline Martin", Address = "48 Kind Lane" });
            dbContext.SaveChanges();
         }
      }

      public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
      {
         try
         {
            logger?.LogInformation($"Querying Customers with id: {id}");
            var Customer = await dbContext.Customers.FirstOrDefaultAsync(p => p.Id == id);
            if (Customer != null)
            {
               logger?.LogInformation("Customer found");
               var result = mapper.Map<Models.Customer>(Customer);
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

      public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
      {
         try
         {
            logger?.LogInformation("Querying Customers");
            var Customers = await dbContext.Customers.ToListAsync();
            if (Customers != null && Customers.Any())
            {
               logger?.LogInformation($"{Customers.Count} Customer(s) found");
               var result = mapper.Map<IEnumerable<Models.Customer>>(Customers);
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

