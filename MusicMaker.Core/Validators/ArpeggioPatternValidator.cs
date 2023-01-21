using FluentValidation;
using MusicMaker.Core.ValueObjects;

namespace MusicMaker.Core.Services
{
    public class ArpeggioPatternValidator : AbstractValidator<ArpeggioPattern>
    {
        public ArpeggioPatternValidator()
        {
            RuleForEach(x => x.Rows).SetValidator(new ArpeggioPatternRowValidator());
        }
    }
}