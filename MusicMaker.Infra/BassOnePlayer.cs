using System;
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
        private readonly ChordPlayerTrack track;
        private readonly PatternBuilder patternBuilder;

        public BassOnePlayer(ChordPlayerTrack track)
        {
            this.track = track ?? throw new ArgumentNullException(nameof(track));
            patternBuilder = track.PatternBuilder;
        }

        public override void PlayOneBarPattern(IChordPlayerTrack track, int channel, ChordChange2 chordChange)
        {
            patternBuilder.Note((Note)Note.Get((SevenBitNumber)(chordChange.ChordRoot - 24)), MusicalTimeSpan.Quarter);
        }

        public override void PlayTwoBarPattern(IChordPlayerTrack track, int channel, ChordChange2 chordChange)
        {
            patternBuilder.Note(Note.Get((SevenBitNumber)(chordChange.ChordRoot - 24)), MusicalTimeSpan.Half);
        }

        public override void PlayThreeBarPattern(IChordPlayerTrack track, int channel, ChordChange2 chordChange)
        {
            patternBuilder.Note(Note.Get((SevenBitNumber)(chordChange.ChordRoot - 24)), MusicalTimeSpan.Quarter * 3);
        }

        public override void PlayFourBarPattern(IChordPlayerTrack track, int channel, ChordChange2 chordChange)
        {
            patternBuilder.Note(Note.Get((SevenBitNumber)(chordChange.ChordRoot - 24)), MusicalTimeSpan.Quarter * 3);
            patternBuilder.Note(Note.Get((SevenBitNumber)(chordChange.ChordRoot - 24)), MusicalTimeSpan.Quarter);
        }
    }
}