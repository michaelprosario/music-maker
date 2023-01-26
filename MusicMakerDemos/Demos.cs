
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Standards;
using MusicMaker.Core.Enums;
using MusicMaker.Core.Interfaces;
using MusicMaker.Core.Requests;
using MusicMaker.Core.Services;
using MusicMaker.Core.Services;
using MusicMaker.Core.ValueObjects;
using MusicMaker.Infra;
using Note = Melanchall.DryWetMidi.MusicTheory.Note;



namespace MusicMaker.Demos;

public class Demos
{
    public void TestDrums()
    {
        string fileName = "out.mid";

        if (File.Exists(fileName))
            File.Delete(fileName);

        IMidiServices midiServices = new MidiServices();
        var service = new MakeDrumTrackService(midiServices);
        var command = new MakeDrumTrackCommand
        {
            BeatsPerMinute = 50,
            FileName = fileName,
            Tracks = new List<DrumTrackRow>
                {
                    new()
                    {
                        Pattern = "x-x-|x-x-|x-x-|x-x-|x-x-|x-x-|x-x-|x-x-|",
                        InstrumentNumber = DrumConstants.HiHat
                    },
                    new()
                    {
                        Pattern = "x---|----|x---|----|x---|----|x---|----|",
                        InstrumentNumber = DrumConstants.AcousticBassDrum
                    },
                    new()
                    {
                        Pattern = "----|x---|----|x--x|----|x---|----|x--x|",
                        InstrumentNumber = DrumConstants.AcousticSnare
                    },
                    new()
                    {
                        Pattern = "-x-x|x-x-|-x-x|x--x|-xx-|xx--|-xx-|x--x|",
                        InstrumentNumber = DrumConstants.HiBongo
                    }
                },
            UserId = "system"
        };

        // act
        var response = service.MakeDrumTrack(command, ".");
    }

    public void PlayArp1()
    {
        var chordServices = new ChordServices(new MidiServices());
        
        var tempo = 90;
        var instrument = (byte)Instruments.AcousticGrandPiano;
        var channel = 1;

        var track = new ChordPlayerTrack(instrument, channel);

        var player = new ArpeggioPlayer(track, ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand1());
        var chordChanges = chordServices.ParseChordProgression("G G C D G G C D Am G C D Em C D D");

        player.PlayFromChordChanges(chordChanges);

        var midiFile = new MidiFile();
        midiFile.Chunks.Add(track.MakeTrackChunk());
        midiFile.Write("out.mid", true);
    }
}
