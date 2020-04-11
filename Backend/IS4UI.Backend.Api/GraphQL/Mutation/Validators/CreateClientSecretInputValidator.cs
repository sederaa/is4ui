using FluentValidation;

public class CreateClientSecretInputValidator : AbstractValidator<CreateClientSecretInput>
{
    public enum ErrorCodes
    {
        DescriptionEmpty,
        DescriptionTooLong,
        ExpirationEmpty,
        TypeEmpty,
        TypeTooLong,
        ValueEmpty,
        ValueTooLong
    }
    public CreateClientSecretInputValidator()
    {
        RuleFor(m => m.Description)
            .NotEmpty().WithErrorCode(ErrorCodes.DescriptionEmpty.ToString())
            .Length(1, 1000).WithErrorCode(ErrorCodes.DescriptionTooLong.ToString());

        RuleFor(m => m.Expiration)
            .NotEmpty().WithErrorCode(ErrorCodes.ExpirationEmpty.ToString());

        RuleFor(m => m.Type)
            .NotEmpty().WithErrorCode(ErrorCodes.TypeEmpty.ToString())
            .Length(1, 250).WithErrorCode(ErrorCodes.TypeTooLong.ToString());

        RuleFor(m => m.Value)
            .NotEmpty().WithErrorCode(ErrorCodes.ValueEmpty.ToString())
            .Length(1, 4000).WithErrorCode(ErrorCodes.ValueTooLong.ToString());

    }
}
