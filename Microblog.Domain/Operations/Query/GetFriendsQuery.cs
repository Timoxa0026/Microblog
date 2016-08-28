namespace Microblog.Domain.Operations.Query
{
    #region << Using >>

    using System.Collections.Generic;
    using Incoding.CQRS;
    using System.Linq;

    #endregion
    public class GetFriendsQuery : QueryBase<List<GetFriendsQuery.Response>>
    {
        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FriendId { get; set; }

        #endregion

        #region Nested Classes
        public class Response
        {
            #region Properties

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string FriendId { get; set; }

            #endregion
        }

        #endregion

        protected override List<Response> ExecuteResult()
        {
            return Repository.Query(whereSpecification: new Friendship.Where.ByUserFriend() { userId = Dispatcher.Query(new GetCurrentUserQuery()).Id})
                             .Select(r => new Response
                             {
                                 FirstName = r.Friend.FirstName,
                                 LastName = r.Friend.LastName,
                                 FriendId = r.Friend.Id,
                             }).ToList();
        }
    }
}
