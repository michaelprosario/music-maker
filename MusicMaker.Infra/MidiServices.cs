using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using MusicMaker.Core.Interfaces;
using MusicMaker.Core.Requests;
using MusicMaker.Core.Services;
using Note = Melanchall.DryWetMidi.MusicTheory.Note;

namespace MusicMaker.Infra
{
    public class MidiServices : IMidiServices
    {
        public int GetNoteNumber(string note){
            return 42;
        }


        public MakeDrumTrackResponse MakeDrumTrack(MakeDrumTrackCommand command)
        {
            MakeDrumTrackResponse response = new();
            
            TempoMap tempoMap = TempoMap.Create(Tempo.FromBeatsPerMinute(command.BeatsPerMinute));            

            // https://melanchall.github.io/drywetmidi/articles/composing/Pattern.html
            var defaultNoteLength = MusicalTimeSpan.Sixteenth;
            var defaultVelocity = (SevenBitNumber)90;
            var tracks = new List<TrackChunk>();

            byte channel = 9;
            foreach (var track in command.Tracks)
            {
                var pattern = new PatternBuilder()
                    .SetNoteLength(defaultNoteLength)
                    .SetVelocity(defaultVelocity);

                var trackChunk = MakePattern(channel, track, pattern, tempoMap);
                tracks.Add(trackChunk);
            }
            
            var midiFile = new MidiFile();
            foreach (var trackChunk in tracks)
            {
                midiFile.Chunks.Add(trackChunk);    
            }
            
            midiFile.Write(command.FileName);            

            return response;
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        private TrackChunk MakePattern(byte channel,
            DrumTrackRow track,
            PatternBuilder pattern, TempoMap tempoMap)
        {
            foreach (var character in track.Pattern)
            {
                switch (character)
                {
                    case 'x':
                    {
                        var instrumentNote = track.InstrumentNumber;
                        pattern.Note(Note.Get((SevenBitNumber)instrumentNote));
                        break;
                    }
                    case '-':
                        pattern.StepForward(MusicalTimeSpan.Sixteenth);
                        break;
                }
            }
            
            return pattern.Build().ToTrackChunk(tempoMap, new FourBitNumber(channel));
        }
    }
}