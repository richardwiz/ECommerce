using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using System;

namespace ECommerce.Api.Search
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         // Services to Configure
         services.AddScoped<IProductsService, ProductsService>();
         services.AddScoped<IOrdersService, OrdersService>();
         services.AddScoped<ICustomersService, CustomersService>();
         services.AddScoped<ISearchService, SearchService>();

         // Orders Client
         services.AddHttpClient("OrdersService", config =>
         {
            config.BaseAddress = new Uri(Configuration["Services:Orders"]);
         }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));

         // Products Client
         services.AddHttpClient("ProductsService", config =>
         {
            config.BaseAddress = new Uri(Configuration["Services:Products"]);
         }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));


         // Customers Client
         services.AddHttpClient("CustomersService", config =>
         {
            config.BaseAddress = new Uri(Configuration["Services:Customers"]);
         }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));
         services.AddControllers();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.UseRouting();

         app.UseAuthorization();

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}