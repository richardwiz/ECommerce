﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Models
{
   public class OrderItem 
   {
      public int Id { get; set; }
      public int ProductId { get; set; }
      public string ProductName { get; set; }
      public int Quantity { get; set; }
      public Decimal UnitPrice { get; set; }

      public OrderItem() : this (0, 0, 0, 0)
      {

      }
      public OrderItem( int id, int productId, int quantity, Decimal price)
      {
         this.Id = id;
         this.ProductId = productId;
         this.Quantity = quantity;
         this.UnitPrice = price;
      }
   }
}
