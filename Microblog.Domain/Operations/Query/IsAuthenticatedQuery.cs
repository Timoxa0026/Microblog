namespace Microblog.Domain.Operations.Query
{
    #region << Using >>

    using System.Web;
    using Incoding.CQRS;

    #endregion

    public class IsAuthenticatedQuery : QueryBase<bool>
    {
        protected override bool ExecuteResult()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }
}