namespace Microblog.Domain
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using FluentNHibernate.Mapping;
    using Incoding;

    #endregion

    public class User : MicroblogEntityBase
    {
        #region Properties

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string Email { get; set; }

        public virtual string PasswordHash { get; set; }

        #endregion

        #region Nested Classes

        public abstract class Where
        {
            #region Nested Classes

            public class ByEmail : Specification<User>
            {
                #region Properties

                readonly string email;

                #endregion

                #region Constructors

                public ByEmail(string email)
                {
                    this.email = email;
                }

                #endregion

                public override Expression<Func<User, bool>> IsSatisfiedBy()
                {
                    return r => r.Email.ToLower() == this.email.ToLower();
                }
            }

            public class ByPasswordHash : Specification<User>
            {
                #region Properties

                readonly string passwordHash;

                #endregion

                #region Constructors

                public ByPasswordHash(string passwordHash)
                {
                    this.passwordHash = passwordHash;
                }

                #endregion

                public override Expression<Func<User, bool>> IsSatisfiedBy()
                {
                    return r => r.PasswordHash == this.passwordHash;
                }
            }

            #endregion
        }

        public class Map : ClassMap<User>
        {
            #region Constructors

            public Map()
            {
                Id(r => r.Id);
                Map(r => r.FirstName);
                Map(r => r.LastName);
                Map(r => r.Email);
                Map(r => r.PasswordHash);
            }

            #endregion
        }

        #endregion
    }
}