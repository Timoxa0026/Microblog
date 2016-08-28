namespace Microblog.Domain.Operations.Command
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;
    using System;
    using FluentValidation;
    using Microblog.Domain.Operations.Query;

    #endregion
    public class AddPostCommand : CommandBase
    {
        #region Properties

        public virtual string Text { get; set; }

        public virtual User Owner { get; set; }

        public virtual DateTime CreateDate { get; set; }

        #endregion

        #region Nested Classes

        public class Validator : AbstractValidator<AddPostCommand>
        {
            #region Constructors

            public Validator()
            {
                RuleFor(r => r.Text).NotEmpty().Length(1, 180);
            }

            #endregion
        }

        #endregion

        protected override void Execute()
        {
            Repository.Save(new Post
            {
                Text = Text,
                Owner = Dispatcher.Query(new GetCurrentUserQuery()),
                CreateDate = DateTime.UtcNow
            });
        }
    }
}