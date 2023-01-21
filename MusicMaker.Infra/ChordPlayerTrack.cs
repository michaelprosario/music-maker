using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Standards;
using MusicMaker.Core.Interfaces;

namespace MusicMaker.Infra
{
    public class ChordPlayerTrack : IChordPlayerTrack
    {
        private readonly int _channel;
        public PatternBuilder PatternBuilder;

        public ChordPlayerTrack(byte instrument, int channel)
        {
            if (channel < 0)
                throw new ArgumentException("Channel should not be negative");
            if (channel > 15)
                throw new ArgumentException("Channel should not be greater than 15");

            PatternBuilder = new PatternBuilder();
            var generalMidiProgram = (GeneralMidiProgram)instrument;
            PatternBuilder.ProgramChange(generalMidiProgram);
            _channel = channel;
        }

        public TrackChunk MakeTrackChunk()
        {
            return PatternBuilder.Build().ToTrackChunk(TempoMap.Default, new FourBitNumber((byte)_channel));
        }
    }
}