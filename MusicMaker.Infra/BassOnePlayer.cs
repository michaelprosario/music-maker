using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Interaction;
using MusicMaker.Core.Services;
using MusicMaker.Core.ValueObjects;
using Note = Melanchall.DryWetMidi.MusicTheory.Note;

namespace MusicMaker.Infra
{
    public class BassOnePlayer : AbstractChordPlayer
    {
        private readonly PatternBuilder _patternBuilder;
        private readonly int TwoOctaves = 24;

        public BassOnePlayer(ChordPlayerTrack track) : base(track)
        {
            _patternBuilder = track.PatternBuilder;
        }

        public override void PlayOneBarPattern(ChordChange chordChange)
        {
            _patternBuilder.Note((Note)Note.Get((SevenBitNumber)(chordChange.ChordRoot - TwoOctaves)),
                MusicalTimeSpan.Quarter);
        }

        public override void PlayTwoBarPattern(ChordChange chordChange)
        {
            _patternBuilder.Note(Note.Get((SevenBitNumber)(chordChange.ChordRoot - TwoOctaves)), MusicalTimeSpan.Half);
        }

        public override void PlayThreeBarPattern(ChordChange chordChange)
        {
            _patternBuilder.Note(Note.Get((SevenBitNumber)(chordChange.ChordRoot - TwoOctaves)),
                MusicalTimeSpan.Quarter * 3);
        }

        public override void PlayFourBarPattern(ChordChange chordChange)
        {
            _patternBuilder.Note(Note.Get((SevenBitNumber)(chordChange.ChordRoot - TwoOctaves)),
                MusicalTimeSpan.Quarter * 3);
            _patternBuilder.Note(Note.Get((SevenBitNumber)(chordChange.ChordRoot - TwoOctaves)),
                MusicalTimeSpan.Quarter);
        }
    }
}