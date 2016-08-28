namespace Microblog.Domain
{
    #region << Using >>

    using System;
    using Incoding.Data;

    #endregion

    public class MicroblogEntityBase : IncEntityBase
    {
        #region Properties

        public virtual new string Id { get; set; }

        #endregion

        #region Constructors

        public MicroblogEntityBase()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        #endregion
    }
}