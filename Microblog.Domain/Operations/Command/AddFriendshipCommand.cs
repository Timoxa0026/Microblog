namespace Microblog.Domain.Operations.Command
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;
    using FluentValidation;
    using Microblog.Domain.Operations.Query;

    #endregion
    public class AddFriendshipCommand : CommandBase
    {
        #region Properties

        public string FriendId { get; set; }

        #endregion

        #region Nested Classes

        public class Validator : AbstractValidator<AddFriendshipCommand>
        {
            #region Constructors

            public Validator()
            {
                RuleFor(r => r.FriendId).NotEmpty();
            }

            #endregion
        }

        #endregion
        protected override void Execute()
        {
            var user = Dispatcher.Query(new GetCurrentUserQuery());
            var friend = Repository.GetById<User>(FriendId);

            if (user.Id != friend.Id && !Dispatcher.Query(new IsFriendshipQuery() { UserId = user.Id, FriendId = friend.Id }))
            {
                Repository.Save(new Friendship
                {
                    User = user,
                    Friend = friend,
                    Status = false
                });
            }
        }
    }
}
