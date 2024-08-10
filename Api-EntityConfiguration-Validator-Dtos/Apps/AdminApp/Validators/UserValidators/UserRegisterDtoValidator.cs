using Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Dtos.UserDto;
using FluentValidation;

namespace Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Validators.ProductValidator
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(p => p.Email).NotEmpty();
            RuleFor(p => p.Fullname)
                .NotEmpty().WithMessage("can’t be empty")
                .MaximumLength(30)
                .WithMessage("maxlength 30");

            RuleFor(s => s.Username)
                .NotEmpty().WithMessage("can't be empty")
                .MaximumLength(30)
                .WithMessage("maxlength 30");

            RuleFor(s => s.Password)
                .NotEmpty().WithMessage("can't be empty")
                .MaximumLength(15)
                .MinimumLength(6);
            RuleFor(s => s.RePassword)
                .NotEmpty().WithMessage("can't be empty")
                .MaximumLength(15)
                .MinimumLength(6);

            RuleFor(p => p)
                .Custom((p, message) =>
                {
                    if (p.Password != p.RePassword)
                    {
                        message.AddFailure("Password", "doesn't match..");
                    }
                });

        }
    }
}
