using FluentValidation;
using MusicMaker.Core.Requests;

namespace MusicMaker.Core.Services
{
    public class MakeMidiFromArpeggioCommandValidator : AbstractValidator<MakeMidiFromArpeggioCommand>
    {
        public MakeMidiFromArpeggioCommandValidator()
        {
            RuleFor(x => x.ChordChanges).NotNull().NotEmpty();
            RuleFor(x => x.Pattern).NotNull().SetValidator(new ArpeggioPatternValidator());
            RuleFor(x => x.BeatsPerMinute).GreaterThan(50);
            RuleFor(x => x.UserId).NotNull().NotEmpty();
        }
    }
}