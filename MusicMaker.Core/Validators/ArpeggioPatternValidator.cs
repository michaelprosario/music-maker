using FluentValidation;
using MusicMaker.Core.ValueObjects;

namespace MusicMaker.Core.Services
{
    public class ArpeggioPatternValidator : AbstractValidator<ArpeggioPattern>
    {
        public ArpeggioPatternValidator()
        {
            RuleFor(x => x.InstrumentNumber).GreaterThan(0).LessThan(255); // does this break DRY???
            RuleForEach(x => x.Rows).SetValidator(new ArpeggioPatternRowValidator());
        }
    }
}