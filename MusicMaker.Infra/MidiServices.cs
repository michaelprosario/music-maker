using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using MusicMaker.Core.Interfaces;
using MusicMaker.Core.Requests;
using MusicMaker.Core.Services;
using Note = Melanchall.DryWetMidi.MusicTheory.Note;

namespace MusicMaker.Infra
{
    public class MidiServices : IMidiServices
    {
        public MakeDrumTrackResponse MakeDrumTrack(MakeDrumTrackCommand command)
        {
            MakeDrumTrackResponse response = new();

            // https://melanchall.github.io/drywetmidi/articles/composing/Pattern.html
            var defaultNoteLength = MusicalTimeSpan.Sixteenth;
            var defaultVelocity = (SevenBitNumber)90;
            var tracks = new List<TrackChunk>();

            byte channel = 1;
            foreach (var track in command.Tracks)
            {
                var pattern = new PatternBuilder()
                    .SetNoteLength(defaultNoteLength)
                    .SetVelocity(defaultVelocity);

                MakePattern(channel, track, pattern, tracks);
                channel++;
            }

            return response;
        }

        private static void MakePattern(
            byte channel,
            DrumTrackRow track,
            PatternBuilder pattern,
            List<TrackChunk> tracks
        )
        {
            TempoMap tempoMap = TempoMap.Create(Tempo.FromBeatsPerMinute(140));

            foreach (var character in track.Pattern)
            {
                if (character == 'x')
                {
                    var instrumentNote = track.InstrumentNumber;
                    pattern.Note(Note.Get((SevenBitNumber)instrumentNote));
                }
                else if (character == '-')
                {
                    pattern.Note(Notes.A8);
                }

                TrackChunk trackChunk = pattern.Build().ToTrackChunk(tempoMap, new FourBitNumber(channel));
                tracks.Add(trackChunk);
            }
        }
    }
}