namespace Ereuna.Web.Common
{
    public interface IFacebookApi
    {
        bool IsFacebookUserTokenValid(string userToken, string userId);
    }
}