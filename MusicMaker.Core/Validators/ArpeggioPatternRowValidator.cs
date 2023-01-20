using FluentValidation;
using MusicMaker.Core.ValueObjects;

namespace MusicMaker.Core.Services
{
    public class ArpeggioPatternRowValidator : AbstractValidator<ArpeggioPatternRow>
    {
        public ArpeggioPatternRowValidator()
        {
            RuleFor(x => x.Pattern).NotNull().NotEmpty();
        }
    }
}