using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Standards;
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
            var instrument = (byte)Instruments.AcousticGrandPiano;
            var channel = 1;

            var track = new ChordPlayerTrack(instrument, channel, tempo);

            var player = new ArpeggioPlayer(track, MakeArpeggioPatternCommand1());
            var chordChanges = GetChords1();

            player.PlayFromChordChanges(chordChanges);

            var midiFile = new MidiFile();
            midiFile.Chunks.Add(track.MakeTrackChunk());
            midiFile.Write("arp1.mid", true);
        }

        [Test]
        public void ArpeggioPlayer__Arp3Test()
        {
            var tempo = 180;
            var instrument = (byte)Instruments.AcousticGrandPiano;
            var channel = 1;

            var track = new ChordPlayerTrack(instrument, channel, tempo);

            var player = new ArpeggioPlayer(track, MakeArpeggioPatternCommand4());
            var chordChanges = GetChords1();

            player.PlayFromChordChanges(chordChanges);

            var midiFile = new MidiFile();
            midiFile.Chunks.Add(track.MakeTrackChunk());
            midiFile.Write("arp3.mid", true);
        }

        private static List<ChordChange> GetChords1()
        {
            var chordChanges = new List<ChordChange>
            {
                new(Note.Parse("A4").NoteNumber, ChordType.Minor, 4),
                new(Note.Parse("G4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("F4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("E4").NoteNumber, ChordType.Major, 4)
            };
            return chordChanges;
        }

        private static List<ChordChange> GetChords2()
        {
            var chordChanges = new List<ChordChange>
            {
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("E4").NoteNumber, ChordType.Minor, 4),
                new(Note.Parse("F4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("G4").NoteNumber, ChordType.Major, 4)
            };
            return chordChanges;
        }

        [Test]
        public void ArpeggioPlayer__Arp2Test()
        {
            var tempo = 180;

            var track = new ChordPlayerTrack((byte)GeneralMidiProgram.Marimba, 1, tempo);
            var track2 = new ChordPlayerTrack((byte)GeneralMidiProgram.Vibraphone, 2, tempo);
            var track3 = new ChordPlayerTrack((byte)GeneralMidiProgram.ElectricBass1, 3, tempo);

            var player = new ArpeggioPlayer(track, MakeArpeggioPatternCommand2());
            var player2 = new ArpeggioPlayer(track2, MakeArpeggioPatternCommand1());
            var player3 = new ArpeggioPlayer(track3, MakeArpeggioPatternCommand3());

            var chordChanges = GetChords2();

            player.PlayFromChordChanges(chordChanges);
            player2.PlayFromChordChanges(chordChanges);
            player3.PlayFromChordChanges(chordChanges);

            var midiFile = new MidiFile();
            midiFile.Chunks.Add(track.MakeTrackChunk());
            midiFile.Chunks.Add(track2.MakeTrackChunk());
            midiFile.Chunks.Add(track3.MakeTrackChunk());
            midiFile.Write("charlie1.mid", true);
        }

        private static List<ChordChange>? YourGraceIsEnoughChords()
        {
            var chordChanges = new List<ChordChange>
            {
                new(Note.Parse("G4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("G4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),

                new(Note.Parse("G4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("G4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),

                new(Note.Parse("E4").NoteNumber, ChordType.Minor, 4),
                new(Note.Parse("D4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),

                new(Note.Parse("G4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("G4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),

                new(Note.Parse("E4").NoteNumber, ChordType.Minor, 4),
                new(Note.Parse("D4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),

                new(Note.Parse("A4").NoteNumber, ChordType.Minor, 4),
                new(Note.Parse("G4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("D4").NoteNumber, ChordType.Major, 4),

                new(Note.Parse("E4").NoteNumber, ChordType.Minor, 4),
                new(Note.Parse("D4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),


                new(Note.Parse("G4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("D4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("E4").NoteNumber, ChordType.Minor, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),

                new(Note.Parse("G4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("D4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4)
            };
            return chordChanges;
        }

        private MakeArpeggioPatternCommand MakeArpeggioPatternCommand1()
        {
            var command = new MakeArpeggioPatternCommand
            {
                Pattern = new ArpeggioPattern
                {
                    Rows = new List<ArpeggioPatternRow>
                    {
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Fifth, Octave = 2, Pattern = "----|----|----|---s|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Third, Octave = 2, Pattern = "----|--s-|s---|s---|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Root, Octave = 2, Pattern = "---s|-s-s|---s|-s--|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Fifth, Octave = 1, Pattern = "--s-|s---|--s-|--s-|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Third, Octave = 1, Pattern = "-s--|----|-s--|----|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Root, Octave = 1, Pattern = "s---|----|s---|----|" }
                    },
                    InstrumentNumber = Instruments.Banjo
                },
                UserId = "mrosario",
                BeatsPerMinute = 120,
                Channel = 0
            };
            return command;
        }

        private MakeArpeggioPatternCommand MakeArpeggioPatternCommand2()
        {
            var command = new MakeArpeggioPatternCommand
            {
                Pattern = new ArpeggioPattern
                {
                    Rows = new List<ArpeggioPatternRow>
                    {
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Fifth, Octave = 2, Pattern = "----|----|----|----|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Third, Octave = 2, Pattern = "----|----|----|----|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Root, Octave = 2, Pattern = "----|----|----|----|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Fifth, Octave = 1, Pattern = "--s-|--s-|--s-|s--s|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Third, Octave = 1, Pattern = "--s-|--s-|--s-|s--s|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Root, Octave = 1, Pattern = "--s-|--s-|--s-|s--s|" }
                    },
                    InstrumentNumber = Instruments.Banjo
                },
                UserId = "mrosario",
                BeatsPerMinute = 120,
                Channel = 0
            };
            return command;
        }

        private MakeArpeggioPatternCommand MakeArpeggioPatternCommand3()
        {
            var command = new MakeArpeggioPatternCommand
            {
                Pattern = new ArpeggioPattern
                {
                    Rows = new List<ArpeggioPatternRow>
                    {
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Fifth, Octave = 2, Pattern = "----|----|----|----|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Third, Octave = 2, Pattern = "----|----|----|----|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Root, Octave = 2, Pattern = "----|----|----|----|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Fifth, Octave = 1, Pattern = "----|----|----|---s|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Third, Octave = 1, Pattern = "----|----|----|----|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Root, Octave = 1, Pattern = "s-s-|s-s-|s-s-|s-s-|" }
                    },
                    InstrumentNumber = Instruments.Banjo
                },
                UserId = "mrosario",
                BeatsPerMinute = 120,
                Channel = 0
            };
            return command;
        }

        private MakeArpeggioPatternCommand MakeArpeggioPatternCommand4()
        {
            // pattern includes half and eighth note items
            var command = new MakeArpeggioPatternCommand
            {
                Pattern = new ArpeggioPattern
                {
                    Rows = new List<ArpeggioPatternRow>
                    {
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Fifth, Octave = 2, Pattern = "----|----|----|----|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Third, Octave = 2, Pattern = "----|----|----|----|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Root, Octave = 2, Pattern = "----|----|----|----|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Fifth, Octave = 1, Pattern = "----|----|q~~~|e~--|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Third, Octave = 1, Pattern = "----|----|----|--e~|" },
                        new ArpeggioPatternRow
                            { Type = ArpeggioPatternRowType.Root, Octave = 1, Pattern = "h~~~|~~~~|----|----|" }
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