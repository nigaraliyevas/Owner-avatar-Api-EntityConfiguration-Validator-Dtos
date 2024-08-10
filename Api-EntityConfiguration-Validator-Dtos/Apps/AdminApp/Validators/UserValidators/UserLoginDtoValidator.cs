using Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Dtos.UserDto;
using FluentValidation;

namespace Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Validators.UserValidators
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(s => s.Username)
            .NotEmpty().WithMessage("can't be empty")
            .MaximumLength(30)
            .WithMessage("maxlength 30");

            RuleFor(s => s.Password)
                .NotEmpty().WithMessage("can't be empty")
                .MaximumLength(15)
                .MinimumLength(6);
        }
    }
}
