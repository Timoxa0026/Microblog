namespace Microblog.Domain
{
    #region << Using >>

    using System.Linq;
    using FluentValidation;
    using Incoding;
    using Incoding.CQRS;
    using Incoding.Extensions;
    using Microblog.Domain.Operations.Query;

    #endregion

    public class SignInCommand : CommandBase
    {
        #region Properties

        public string Email { get; set; }

        public string Password { get; set; }

        #endregion

        #region Nested Classes

        public class Validator : AbstractValidator<SignInCommand>
        {
            #region Constructors

            public Validator()
            {
                RuleFor(r => r.Email).NotEmpty().EmailAddress();
                RuleFor(r => r.Password).NotEmpty();
            }

            #endregion
        }

        #endregion

        protected override void Execute()
        {
            var user = Repository.Query(whereSpecification: new User.Where.ByEmail(Email)
                                                .And(new User.Where.ByPasswordHash(Dispatcher.Query(new GetHashQuery { Value = Password })))).SingleOrDefault();

            if (user == null)
                throw IncWebException.ForServer("Invalid login attempt!");

            Dispatcher.Push(new AddAuthTicketCommand()
                            {
                                    IsPersistent = true,
                                    Ticket = user.Id
                            });
        }
    }
}