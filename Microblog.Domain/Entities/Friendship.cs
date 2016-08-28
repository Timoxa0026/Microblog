namespace Microblog.Domain
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using FluentNHibernate.Mapping;
    using Incoding;

    #endregion
    public class Friendship : MicroblogEntityBase
    {
        #region Properties

        public virtual User User { get; set; }

        public virtual User Friend { get; set; }

        public virtual bool Status { get; set; }

        #endregion

        public abstract class Where
        {
            #region Nested Classes

            public class ByUserFriend : Specification<Friendship>
            {
                #region Properties

                public string userId;

                #endregion

                public override Expression<Func<Friendship, bool>> IsSatisfiedBy()
                {
                    return r => r.User.Id == this.userId;
                }
            }

            public class IsUserFriendly : Specification<Friendship>
            {
                #region Properties

                public string FriendId;

                public string UserId;

                #endregion

                public override Expression<Func<Friendship, bool>> IsSatisfiedBy()
                {
                    return r => r.User.Id == this.UserId && r.Friend.Id == this.FriendId;
                }
            }

            #endregion
        }

        #region Nested Classes
        public class Map : ClassMap<Friendship>
        {
            #region Constructors

            public Map()
            {
                Id(r => r.Id);
                References(u => u.User);
                References(u => u.Friend);
                Map(r => r.Status);
            }

            #endregion
        }

        #endregion
    }
}
