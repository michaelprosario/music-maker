using System.Collections.Generic;
using System.IO;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using MusicMaker.Core.Enums;
using MusicMaker.Core.Interfaces;
using MusicMaker.Core.Requests;
using MusicMaker.Core.Services;
using MusicMaker.Core.ValueObjects;
using MusicMaker.Infra;

namespace MusicMaker.Demos
{
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
            var instrument = (byte)Instruments.Marimba;
            var channel = 1;

            var track = new ChordPlayerTrack(instrument, channel);

            var player = new ArpeggioPlayer(track, ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand1());
            var chordChanges = chordServices.ParseChordProgression("Em C D Em Em C D E");

            player.PlayFromChordChanges(chordChanges);

            var midiFile = new MidiFile();
            TempoMap tempoMap = TempoMap.Create(Tempo.FromBeatsPerMinute(tempo));
            midiFile.ReplaceTempoMap(tempoMap);

            midiFile.Chunks.Add(track.MakeTrackChunk());
            midiFile.Write("out.mid", true);
        }
    }
}