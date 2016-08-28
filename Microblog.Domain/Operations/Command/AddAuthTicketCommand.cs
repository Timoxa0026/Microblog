namespace Microblog.Domain
{
    #region << Using >>

    using System;
    using System.Web;
    using System.Web.Security;
    using Incoding.CQRS;

    #endregion

    public class AddAuthTicketCommand : CommandBase
    {
        #region Properties

        public string Ticket { get; set; }

        public bool IsPersistent { get; set; }

        #endregion

        protected override void Execute()
        {
            var encryptTick = FormsAuthentication.Encrypt(new FormsAuthenticationTicket(Ticket, true, int.MaxValue));
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTick) { Expires = DateTime.UtcNow.AddMonths(6) };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}