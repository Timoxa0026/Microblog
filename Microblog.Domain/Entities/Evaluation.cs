namespace Microblog.Domain
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using FluentNHibernate.Mapping;
    using Incoding;

    #endregion
    public class Evaluation : MicroblogEntityBase
    {
        #region Properties

        public virtual User Owner { get; set; }

        public virtual Post Post { get; set; }

        public virtual bool isLike { get; set; }

        public virtual bool isDisLike { get; set; }

        #endregion

        #region Nested Classes
        public abstract class Where
        {
            #region Nested Classes
            public class PostEvaluationByOwnerId : Specification<Evaluation>
            {
                #region Properties

                public string OwnerId;

                public string PostId;

                #endregion

                #region Constructors

                public override Expression<Func<Evaluation, bool>> IsSatisfiedBy()
                {
                    return r => r.Owner.Id == this.OwnerId && r.Post.Id == this.PostId;
                }

                #endregion
            }

            public class PostEvaluationLikes : Specification<Evaluation>
            {
                #region Properties

                public string PostId;

                #endregion

                #region Constructors

                public override Expression<Func<Evaluation, bool>> IsSatisfiedBy()
                {
                    return r => r.Post.Id == this.PostId && r.isLike == true;
                }

                #endregion
            }

            public class PostEvaluationDisLikes : Specification<Evaluation>
            {
                #region Properties

                public string PostId;

                #endregion

                #region Constructors

                public override Expression<Func<Evaluation, bool>> IsSatisfiedBy()
                {
                    return r => r.Post.Id == this.PostId && r.isDisLike == true;
                }

                #endregion
            }

            #endregion
        }

        public class Map : ClassMap<Evaluation>
        {
            #region Constructors

            public Map()
            {
                Id(r => r.Id);
                References(u => u.Owner);
                References(p => p.Post);
                Map(r => r.isLike);
                Map(r => r.isDisLike);
            }

            #endregion
        }

        #endregion
    }
}
