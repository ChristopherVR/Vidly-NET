using static MovieSystem.API.Application.Commands.UserCommands;

namespace MovieSystem.API.Application.Validations;
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(ILogger<UpdateUserCommandValidator> logger)
    {
        logger.LogInformation("UserCommandValidator is executing...");
        RuleFor(command => command.Name).NotEmpty().WithMessage("Name cannot be empty");
        RuleFor(command => command.User).NotEmpty().EmailAddress().WithMessage("Not a valid Email Addres/s");
        RuleFor(command => command.ImageUrl)
            .Must(x => new System.ComponentModel.DataAnnotations.UrlAttribute().IsValid(x))
            .Unless(command => string.IsNullOrWhiteSpace(command.ImageUrl))
            .WithMessage("Url is not valid.");
    }
}
