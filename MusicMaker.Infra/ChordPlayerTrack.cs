using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Standards;
using MusicMaker.Core.Services;

namespace MusicMaker.Infra
{
    public class ChordPlayerTrack : IChordPlayerTrack
    {
        private readonly int _channel;
        private readonly int _tempo;
        public PatternBuilder PatternBuilder;

        public ChordPlayerTrack(int instrument, int channel, int tempo)
        {
            PatternBuilder = new PatternBuilder();
            GeneralMidi2Program generalMidiProgram = (GeneralMidi2Program)instrument;
            PatternBuilder.ProgramChange(generalMidiProgram);
            this._channel = channel;
            this._tempo = tempo;
        }

        public TrackChunk MakeTrackChunk()
        {
            TempoMap tempoMap = TempoMap.Create(Tempo.FromBeatsPerMinute(_tempo));
            return PatternBuilder.Build().ToTrackChunk(tempoMap, new FourBitNumber((byte)_channel));
        }
    }
}