using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using EmsTU.Model.Models;

namespace EmsTU.Model.Infrastructure
{
    public class UserContextProviderImpl : IUserContextProvider
    {
        public const string UserContextKey = "__AIS__UserContextKey__";
        public const int CookieFullNameMaxLength = 40;
        private HttpContextBase httpContext;

        public UserContextProviderImpl(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }

        public UserContext GetCurrentUserContext()
        {
            if (this.httpContext.Items[UserContextKey] == null)
            {
                HttpCookie authCookie = this.httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    if (authTicket == null || authTicket.Expired)
                    {
                        throw new SecurityException("Invalid authentication ticket.");
                    }

                    string[] userData = authTicket.UserData.Split(',');
                    string fullName = this.httpContext.Server.UrlDecode(userData[0]);
                    string[] permissions = userData.Skip(2).Where(s => !string.IsNullOrEmpty(s)).ToArray();

                    this.httpContext.Items[UserContextKey] = new UserContext(int.Parse(authTicket.Name), fullName, permissions);
                }
            }

            return (UserContext)this.httpContext.Items[UserContextKey];
        }

        public void SetCurrentUserContext(UserContext userContext)
        {
            string fullname = userContext.FullName;
            if (fullname.Length > CookieFullNameMaxLength)
            {
                fullname = fullname.Substring(0, CookieFullNameMaxLength);
            }

            string userData =
                this.httpContext.Server.UrlEncode(fullname) + "," +
                "1," ;//+ (userContext.HasPassword ? "1" : "0") + "," +
                //string.Join(",", userContext.Permissions);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
              2,
              userContext.UserId.ToString(),
              DateTime.Now,
              DateTime.Now.Add(FormsAuthentication.Timeout),
              false, // isPersistent
              userData,
              FormsAuthentication.FormsCookiePath);

            HttpCookie authCookie = new HttpCookie(
                FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(authTicket));

            authCookie.HttpOnly = true;
            authCookie.Path = FormsAuthentication.FormsCookiePath;
            authCookie.Secure = FormsAuthentication.RequireSSL;
            if (FormsAuthentication.CookieDomain != null)
                authCookie.Domain = FormsAuthentication.CookieDomain;
            if (authTicket.IsPersistent)
                authCookie.Expires = authTicket.Expiration;

            this.httpContext.Response.Cookies.Add(authCookie);

            this.httpContext.Items[UserContextKey] = userContext;
        }

        public void ClearCurrentUserContext()
        {
            FormsAuthentication.SignOut();

            this.httpContext.Items[UserContextKey] = null;
        }
    }
}
