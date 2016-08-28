namespace Microblog.Domain
{
    #region << Using >>

    using System;
    using System.Web;
    using System.Web.Security;
    using Incoding.CQRS;

    #endregion

    public class SignOutCommand : CommandBase
    {
        protected override void Execute()
        {
            var encryptTick = FormsAuthentication.Encrypt(new FormsAuthenticationTicket(HttpContext.Current.User.Identity.Name, true, int.MaxValue));
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTick) { Expires = DateTime.UtcNow.AddMonths(-6) };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}