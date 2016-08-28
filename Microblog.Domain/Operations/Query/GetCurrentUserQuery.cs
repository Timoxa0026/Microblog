namespace Microblog.Domain.Operations.Query
{
    #region << Using >>

    using System.Web;
    using Incoding.CQRS;

    #endregion

    public class GetCurrentUserQuery : QueryBase<User>
    {
        protected override User ExecuteResult()
        {
            var identity = HttpContext.Current.User.Identity;

            return !identity.IsAuthenticated ? null : Repository.GetById<User>(identity.Name);
        }
    }
}