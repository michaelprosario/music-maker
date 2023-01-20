using FluentValidation;
using MusicMaker.Core.ValueObjects;

public class ArpeggioPatternRowValidator : AbstractValidator<ArpeggioPatternRow>
{
    public ArpeggioPatternRowValidator()
    {
        RuleFor(r => r.Pattern).NotNull().NotEmpty();
    }
}