using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoyaltyAppLibrary.FrameWork;
using LoyaltyAppLibrary.Model;
using LoyaltyAppLibrary.App;

namespace LoyaltyAppLibrary.Manager
{
    public class LoyaltyUserManager : BaseManager<UserModel>
    {
        public override UserModel GetItem(Guid uniqueId)
        {
            throw new NotImplementedException();
        }

        public override int SaveItem(UserModel model)
        {
            throw new NotImplementedException();
        }
        public static async Task<UserModel> LoginAsync(string userName, string passWord)
        {
            try
            {
                await Client.GetInstance().WebClient.RefreshTokenAsync(new TokenRefreshParameters(userName, passWord, Configuration.AppMobileAppInfo.Scope));
                var result = await Client.GetInstance().WebClient.SendGetRequestAsync<UserModel>(TokenType.AppToken, Configuration.WebService.User.View,false);
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
