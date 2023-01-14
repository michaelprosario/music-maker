using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;
using MusicMaker.Core.Enums;
using MusicMaker.Core.Requests;
using MusicMaker.Core.ValueObjects;
using MusicMaker.Infra;
using NUnit.Framework;

namespace MusicMaker.Tests
{
    [TestFixture]
    public class ArpeggioPlayerTests
    {
        [Test]
        public void ArpeggioPlayer__Arp1Test()
        {
            var tempo = 180;
            var instrument = Instruments.AcousticGrandPiano;
            var channel = 1;

            var track = new ChordPlayerTrack(instrument, channel, tempo);
            
            var command = MakeArpeggioPatternCommand1();

            var player = new ArpeggioPlayer(track,command);
            var chordChanges = new List<ChordChange>
            {
                new(Note.Parse("A4").NoteNumber, ChordType.Minor, 4),
                new(Note.Parse("G4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("F4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("E4").NoteNumber, ChordType.Major, 4),
            };

            player.PlayFromChordChanges(chordChanges);

            var midiFile = new MidiFile();
            midiFile.Chunks.Add(track.MakeTrackChunk());
            midiFile.Write("arp1.mid", true);
        }

        private MakeArpeggioPatternCommand MakeArpeggioPatternCommand1()
        {
            var command = new MakeArpeggioPatternCommand
            {
                Pattern = new ArpeggioPattern()
                {
                    Rows = new()
                    {
                        new() { Type = ArpeggioPatternRowType.Fifth, Octave = 2, Pattern = "----|----|--s-|----|" },
                        new() { Type = ArpeggioPatternRowType.Third, Octave = 2, Pattern = "----|----|s---|s---|" },
                        new() { Type = ArpeggioPatternRowType.Root, Octave = 2, Pattern =  "----|--s-|----|--s-|" },
                        new() { Type = ArpeggioPatternRowType.Fifth, Octave = 1, Pattern = "----|s---|----|----|" },
                        new() { Type = ArpeggioPatternRowType.Third, Octave = 1, Pattern = "--s-|----|----|----|" },
                        new() { Type = ArpeggioPatternRowType.Root, Octave = 1, Pattern =  "s---|----|----|----|" },
                    },
                    InstrumentNumber = Instruments.Banjo
                },
                UserId = "mrosario",
                BeatsPerMinute = 120,
                Channel = 0
            };
            return command;
        }
    }
}