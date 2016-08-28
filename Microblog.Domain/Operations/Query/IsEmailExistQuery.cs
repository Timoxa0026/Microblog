namespace Microblog.Domain.Operations.Query
{
    #region << Using >>

    using Incoding.CQRS;
    using NHibernate.Util;

    #endregion

    public class IsEmailExistQuery : QueryBase<bool>
    {
        #region Properties

        public string Email { get; set; }

        #endregion

        protected override bool ExecuteResult()
        {
            return Repository.Query(whereSpecification: new User.Where.ByEmail(Email)).Any();
        }
    }
}