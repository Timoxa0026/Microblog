namespace Microblog.Domain
{
    #region << Using >>

    using FluentValidation;
    using Incoding.Block.IoC;
    using Incoding.CQRS;
    using Microblog.Domain.Operations.Query;

    #endregion

    public class SignUpCommand : CommandBase
    {
        #region Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        #endregion

        #region Nested Classes

        public class Validator : AbstractValidator<SignUpCommand>
        {
            #region Constructors

            public Validator()
            {
                RuleFor(r => r.Email).NotEmpty().EmailAddress().Must((command, s) => !IoCFactory.Instance.TryResolve<IDispatcher>().Query(new IsEmailExistQuery() { Email = command.Email }));
                RuleFor(r => r.Password).NotEmpty();
                RuleFor(r => r.FirstName).NotEmpty();
                RuleFor(r => r.LastName).NotEmpty();
            }

            #endregion
        }

        #endregion

        protected override void Execute()
        {
            Repository.Save(new User
                            {
                                    Email = Email,
                                    FirstName = FirstName,
                                    LastName = LastName,
                                    PasswordHash = Dispatcher.Query(new GetHashQuery { Value = Password })
                            });

            Dispatcher.Push(new SignInCommand
                            {
                                    Email = Email, 
                                    Password = Password
                            });
        }
    }
}