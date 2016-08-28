namespace Microblog.Domain.Operations.Query
{
    #region << Using >>

    using System.Collections.Generic;
    using Incoding.CQRS;

    #endregion

    public class GetCurrentUserPostsQuery : QueryBase<List<GetPostsQuery.Response>>
    {
        protected override List<GetPostsQuery.Response> ExecuteResult()
        {
            return Dispatcher.Query(new GetPostsQuery { OwnerId = Dispatcher.Query(new GetCurrentUserQuery()).Id });
        }
    }
}