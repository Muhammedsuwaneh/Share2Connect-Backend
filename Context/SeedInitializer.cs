using Share2Connect.Backend.Models;
using Share2Connect.Backend.Encryption;

namespace Share2Connect.Backend.Context
{
    public class SeedInitializer
    {
        public static void seed(IApplicationBuilder applicationBuilder)
        {
           using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                // init user context service 
                var _userContext = serviceScope.ServiceProvider.GetService<UserDbContext>();

                if(!_userContext.Users.Any())
                {
                    // create a default user
                    _userContext.Users.AddRange(new User { FullName = "enes", Email = "enes@enes.com", 
                        Gender = "Male", Password = Encrypt.GenerateMD5HashedPassword("1234@@@") });

                    // save default user
                    _userContext.SaveChanges();
                }
            } 
        }
    }
}
