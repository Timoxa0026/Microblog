namespace Microblog.Domain.Operations.Command
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;
    using FluentValidation;
    using Microblog.Domain.Operations.Query;

    #endregion
    public class AddEvaluationCommand : CommandBase
    {
        #region Properties

        public bool Like { get; set; }

        public bool DisLike { get; set; }

        public string PostId { get; set; }

        #endregion

        #region Nested Classes

        public class Validator : AbstractValidator<AddEvaluationCommand>
        {
            #region Constructors

            public Validator()
            {
                RuleFor(r => r.Like != r.DisLike);
            }

            #endregion
        }

        #endregion
        protected override void Execute()
        {
            var post = Repository.GetById<Post>(PostId);
            var owner = Dispatcher.Query(new GetCurrentUserQuery());
            //TODO: используй SingleOrDefault() всегда, если в бизнес-логике подразумевается единственная запись
            var evaluation = Repository.Query(whereSpecification: new Evaluation.Where.PostEvaluationByOwnerId() { OwnerId = owner.Id, PostId = this.PostId }).FirstOrDefault();

            if (evaluation == null)
            {
                Repository.Save(new Evaluation
                {
                    Owner = owner,
                    Post = post,
                    isLike = this.Like,
                    isDisLike = this.DisLike
                });
            }
            else
            {
                evaluation.isLike = this.Like;
                evaluation.isDisLike = this.DisLike;
                Repository.SaveOrUpdate(evaluation);
            }
        }
    }
}
