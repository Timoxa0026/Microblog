namespace Microblog.Domain.Operations.Query
{
    #region << Using >>

    using Incoding.CQRS;
    using NHibernate.Util;

    #endregion

    public class IsFriendshipQuery : QueryBase<bool>
    {
        #region Properties

        public string UserId { get; set; }
        public string FriendId { get; set; }

        #endregion

        protected override bool ExecuteResult()
        {
            return Repository.Query(whereSpecification: new Friendship.Where.IsUserFriendly() { UserId = this.UserId, FriendId = this.FriendId }).Any();
        }
    }
}