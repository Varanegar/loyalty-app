using LoyaltyAppLibrary.FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyAppLibrary.App
{
    public class Configuration
    {
        public static readonly string userInfoFile = "userInfo";
        public static readonly string customerInfoFile = "customerInfo";
        public static readonly string tokenInfoFile = "tk.info";
        public struct AppMobileAppInfo
        {
            public static readonly string UserName = "AnatoliMobileApp";
            public static readonly string Password = "Anatoli@App@Vn";
            public static readonly string Scope = "79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240,3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
        }
        public struct WebService
        {
            public static string PortalAddress = "";
            public static readonly string OAuthTokenUrl = "/oauth/token";
            public static readonly string BaseDatas = "api/gateway/basedata/basedatas/compress/";
        }

        public static Setting ReadSetting(string name)
        {
            return Client.GetInstance().DbClient.GetItem<Setting>(string.Format("SELECT * FROM Setting WHERE Name='{0}'", name));
        }
        public static List<Setting> ReadSetting()
        {
            return Client.GetInstance().DbClient.GetList<Setting>("SELECT * FROM Setting");
        }
        public static void SaveSetting(Setting setting)
        {
            if (ReadSetting(setting.Name) == null)
                Client.GetInstance().DbClient.UpdateItem(string.Format("INSERT INTO Setting (Name,Value) VALUES ('{0}','{1}')", setting.Name, setting.Value));
            else
                Client.GetInstance().DbClient.UpdateItem(string.Format("UPDATE Setting SET Value='{1}' WHERE Name='{0}'", setting.Name, setting.Value));
        }
        public static void SaveSetting(List<Setting> settings)
        {
            foreach (var setting in settings)
            {
                SaveSetting(setting);
            }
        }
        public class Setting : BaseModel
        {
            public string Name { get; set; }
            public string Value { get; set; }

            public static string Camera = "Camera";
        }
    }
}
