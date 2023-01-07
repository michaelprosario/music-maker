using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using MusicMaker.Core.Enums;
using MusicMaker.Core.Interfaces;
using MusicMaker.Core.Requests;
using MusicMaker.Core.Services;
using MusicMaker.Core.ValueObjects;
using Note = Melanchall.DryWetMidi.MusicTheory.Note;
using System.Collections.Generic;

namespace MusicMaker.Infra
{

    public class BassOnePlayer : AbstractChordPlayer
    {
        PatternBuilder patternBuilder;
        private readonly ChordPlayerTrack track;

        public BassOnePlayer(ChordPlayerTrack track)
        {
            this.track = track ?? throw new ArgumentNullException(nameof(track));
            this.patternBuilder = track.PatternBuilder;
        }

        public override void PlayOneBarPattern(IChordPlayerTrack track, int channel, ChordChange2 chordChange){
            patternBuilder.Note(Note.Get((SevenBitNumber)(chordChange.ChordRoot-24)), MusicalTimeSpan.Quarter);
        }

        public override void PlayTwoBarPattern(IChordPlayerTrack track, int channel, ChordChange2 chordChange){
            patternBuilder.Note(Note.Get((SevenBitNumber)(chordChange.ChordRoot-24)), MusicalTimeSpan.Half);
        }

        public override void PlayThreeBarPattern(IChordPlayerTrack track, int channel, ChordChange2 chordChange){
            patternBuilder.Note(Note.Get((SevenBitNumber)(chordChange.ChordRoot-24)), MusicalTimeSpan.Quarter * 3);
        }

        public override void PlayFourBarPattern(IChordPlayerTrack track, int channel, ChordChange2 chordChange)
        {
            patternBuilder.Note(Note.Get((SevenBitNumber)(chordChange.ChordRoot-24)), MusicalTimeSpan.Quarter * 3);
            patternBuilder.Note(Note.Get((SevenBitNumber)(chordChange.ChordRoot-24)), MusicalTimeSpan.Quarter);
        }
    }
}