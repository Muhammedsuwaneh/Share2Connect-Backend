using Share2Connect.Api.Models;
using Share2Connect.Api.Encryption;

namespace Share2Connect.Api.Context
{
    public class SeedInitializer
    {
        public static void seed(IApplicationBuilder applicationBuilder)
        {
           using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                // init user context service 
                var _userContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                if(!_userContext.Users.Any())
                {
                    // create a default user
                    _userContext.Users.AddRange(new User { userNameText = "enes", userMail = "enes@enes.com", 
                        userGender = "Erkek", userBio  = "Android App Developer", userPhoneNumber = "05340770833",
                        userImage = null,
                        userPassword = Encrypt.GenerateMD5HashedPassword("1234"),
                     userDepartment = "Bilgisayar Mühendisliği" });

                    // save default user
                    _userContext.SaveChanges();
                }
            } 
        }
    }
}
