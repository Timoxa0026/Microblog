namespace Microblog.Domain
{
    #region << Using >>

    using System;
    using System.Linq.Expressions;
    using FluentNHibernate.Mapping;
    using Incoding;

    #endregion

    public class Post : MicroblogEntityBase
    {
        #region Properties

        public virtual string Text { get; set; }

        public virtual User Owner { get; set; }

        public virtual DateTime CreateDate { get; set; }

        #endregion

        #region Nested Classes

        public abstract class Where
        {
            #region Nested Classes

            public class ByOwnerId : Specification<Post>
            {
                #region Properties

                public string OwnerId;

                #endregion

                public override Expression<Func<Post, bool>> IsSatisfiedBy()
                {
                    return r => r.Owner.Id == this.OwnerId;
                }
            }

            public class All : Specification<Post>
            {
                public override Expression<Func<Post, bool>> IsSatisfiedBy()
                {
                    return r => true;
                }
            }

            #endregion
        }

        public class Map : ClassMap<Post>
        {
            #region Constructors

            public Map()
            {
                Id(r => r.Id);
                Map(r => r.Text);
                Map(r => r.CreateDate);
                References(r => r.Owner);
            }

            #endregion
        }

        #endregion
    }
}