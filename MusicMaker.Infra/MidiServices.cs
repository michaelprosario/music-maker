using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Interaction;

public class MidiServices : IMidiServices
{
    public MidiServices()
    {
    }

    public MakeDrumTrackResponse MakeDrumTrack(MakeDrumTrackCommand command)
    {
        MakeDrumTrackResponse response = new MakeDrumTrackResponse();

        // https://melanchall.github.io/drywetmidi/articles/composing/Pattern.html
        var defaultNoteLength = MusicalTimeSpan.Sixteenth;
        var defaultVelocity = (SevenBitNumber)90;
        var tracks = new List<Melanchall.DryWetMidi.Core.TrackChunk>();

        byte channel = 1;
        foreach(var track in command.Tracks)
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
        List<Melanchall.DryWetMidi.Core.TrackChunk> tracks
        )
    {
        TempoMap tempoMap = TempoMap.Create(Tempo.FromBeatsPerMinute(140));

        foreach (var character in track.Pattern)
        {
            if (character == 'x')
            {
                pattern.Note(track.InstrumentNumber);
            }
            else if (character == '-')
            {
                pattern.Note(Notes.A8);
            }

            Melanchall.DryWetMidi.Core.TrackChunk trackChunk = pattern.Build().ToTrackChunk(tempoMap, new FourBitNumber(channel));
            tracks.Add(trackChunk);
        }
    }
}
