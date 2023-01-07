using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using MusicMaker.Core.Services;

namespace MusicMaker.Infra
{
    public class ChordPlayerTrack : IChordPlayerTrack
    {
        private readonly int channel;
        private readonly int tempo;
        public PatternBuilder PatternBuilder;
        public ChordPlayerTrack(int instrument, int channel, int tempo)
        {
            PatternBuilder = new PatternBuilder();
            PatternBuilder.ProgramChange((SevenBitNumber)instrument);
            this.channel = channel;
            this.tempo = tempo;
        }

        public TrackChunk MakeTrackChunk()
        {
            TempoMap tempoMap = TempoMap.Create(Tempo.FromBeatsPerMinute(tempo));            
            return PatternBuilder.Build().ToTrackChunk(tempoMap, new FourBitNumber((byte)channel));
        }
    }
}