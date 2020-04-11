using FluentValidation;

public class CreateClientInputValidator : AbstractValidator<CreateClientInput>
{
    public enum ErrorCodes
    {
        ClientIdEmpty,
        ClientIdTooLong,
        ClientNameEmpty,
        ClientNameTooLong,
        ClientSecretsEmpty,
    }
    public CreateClientInputValidator()
    {
        RuleFor(m => m.ClientId)
            .NotEmpty().WithErrorCode(ErrorCodes.ClientIdEmpty.ToString())
            .Length(1, 10).WithErrorCode(ErrorCodes.ClientIdTooLong.ToString());

        RuleFor(m => m.ClientName)
            .NotEmpty().WithErrorCode(ErrorCodes.ClientNameEmpty.ToString())
            .Length(1, 10).WithErrorCode(ErrorCodes.ClientNameTooLong.ToString());

        RuleFor(m => m.ClientSecrets)
            .NotEmpty().WithErrorCode(ErrorCodes.ClientSecretsEmpty.ToString());

        RuleForEach(m => m.ClientSecrets)
            .SetValidator(new CreateClientSecretInputValidator());

    }
}
