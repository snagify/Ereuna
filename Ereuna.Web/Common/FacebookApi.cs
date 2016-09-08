using System.IO;
using System.Net;
using Ereuna.Web.Models;
using Newtonsoft.Json;

namespace Ereuna.Web.Common
{
    public class FacebookApi : IFacebookApi
    {
        private static readonly string ApplicationId  = "923459464377126";
        private static readonly string ApplicationSecret = "341e38ad67b23a5f62f63a8dc8f4f47c";
        private string ApplicationToken => ApplicationId + "|" + ApplicationSecret;

        private static readonly string graphApi = "graph.facebook.com";

        public bool IsFacebookUserTokenValid(string userToken, string userId)
        {
            var api = "/me?access_token=" + userToken;
            var result = MakeApiRequest(api);
            var facebookToken = JsonConvert.DeserializeObject<FacebookUserToken>(result);
            if (facebookToken == null) return false;
            if (facebookToken.Id == userId && facebookToken.Verified)
            {
                return true;
            }
            return true;
        }

        private string MakeApiRequest(string request)
        {
            var url = "https://" + graphApi + request;
            var webRequest = WebRequest.Create(url);
            webRequest.Method = "GET";
            var response = webRequest.GetResponse();
            using (var stream = response.GetResponseStream())
            using (var sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }
    }
}