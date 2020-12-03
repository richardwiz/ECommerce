
namespace ECommerce.Api.Customers.Profiles
{
   public class CustomerProfile : AutoMapper.Profile
   {
      public CustomerProfile()
      {
         CreateMap<DB.Customer, Models.Customer>();
      }
   }
}
