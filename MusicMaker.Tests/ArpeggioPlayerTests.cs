using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Standards;
using MusicMaker.Core.Enums;
using MusicMaker.Core.Services;
using MusicMaker.Core.ValueObjects;
using MusicMaker.Infra;
using NUnit.Framework;
using Note = Melanchall.DryWetMidi.MusicTheory.Note;

namespace MusicMaker.Tests
{
    [TestFixture]
    public class ArpeggioPlayerTests
    {
        [Test]
        public void ArpeggioPlayer__Arp1Test()
        {
            var tempo = 80;
            var instrument = (byte)Instruments.Marimba;
            var channel = 1;

            var track = new ChordPlayerTrack(instrument, channel);

            var player = new ArpeggioPlayer(track, ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand1());
            var chordChanges = GetChords2();

            player.PlayFromChordChanges(chordChanges);

            var midiFile = new MidiFile();
            TempoMap tempoMap = TempoMap.Create(Tempo.FromBeatsPerMinute(tempo));
            midiFile.ReplaceTempoMap(tempoMap);

            midiFile.Chunks.Add(track.MakeTrackChunk());
            midiFile.Write(@"arp1.mid", true);
        }

        [Test]
        public void ArpeggioPlayer__TestTwoBarChordChanges()
        {
            var tempo = 80;
            var instrument = (byte)Instruments.Marimba;
            var channel = 1;

            var track = new ChordPlayerTrack(instrument, channel);

            var player = new ArpeggioPlayer(track, ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand1());
            int k = 2;
            var chordChanges = new List<ChordChange>
            {
                new(Note.Parse("C4").NoteNumber, ChordType.Major, k),
                new(Note.Parse("E4").NoteNumber, ChordType.Minor, k),
                new(Note.Parse("F4").NoteNumber, ChordType.Major, k),
                new(Note.Parse("G4").NoteNumber, ChordType.Major, k)
            };

            player.PlayFromChordChanges(chordChanges);

            var midiFile = new MidiFile();
            TempoMap tempoMap = TempoMap.Create(Tempo.FromBeatsPerMinute(tempo));
            midiFile.ReplaceTempoMap(tempoMap);

            midiFile.Chunks.Add(track.MakeTrackChunk());
            midiFile.Write(@"arp1.mid", true);
        }        

        [Test]
        public void ArpeggioPlayer__Arp3Test()
        {
            var tempo = 180;
            var instrument = (byte)Instruments.AcousticGrandPiano;
            var channel = 1;

            var track = new ChordPlayerTrack(instrument, channel);

            var player = new ArpeggioPlayer(track, ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand4());
            var chordChanges = GetChords1();

            player.PlayFromChordChanges(chordChanges);

            var midiFile = new MidiFile();
            midiFile.Chunks.Add(track.MakeTrackChunk());
            midiFile.Write("arp3.mid", true);
        }

        [Test]
        public void ArpeggioPlayer__Arp4Test()
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
            midiFile.Write("arp4.mid", true);
        }
        
        [Test]
        public void ArpeggioPattern__ClonePattern()
        {
            var command = ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand1();
            ArpeggioPattern clone = command.Pattern.Clone();
            Assert.NotNull(clone);
        }        
        
        [Test]
        public void ArpeggioPattern__MakeTwoBarPattern()
        {
            var command = ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand1();
            ArpeggioPattern clone = command.Pattern.CopyFirstMeasures(2);
            Assert.NotNull(clone);
            foreach(var row in clone.Rows)
            {
                Assert.True(row.Pattern.Length == 8);
            }
        }        
        
        [Test]
        public void ArpeggioPattern__MakeThreeBarPattern()
        {
            var command = ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand1();
            ArpeggioPattern clone = command.Pattern.CopyFirstMeasures(3);
            Assert.NotNull(clone);
            foreach(var row in clone.Rows)
            {
                Assert.True(row.Pattern.Length == 12);
            }
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

            var track = new ChordPlayerTrack((byte)GeneralMidiProgram.Marimba, 1);
            var track2 = new ChordPlayerTrack((byte)GeneralMidiProgram.Vibraphone, 2);
            var track3 = new ChordPlayerTrack((byte)GeneralMidiProgram.ElectricBass1, 3);

            var player = new ArpeggioPlayer(track, ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand2());
            var player2 = new ArpeggioPlayer(track2, ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand1());
            var player3 = new ArpeggioPlayer(track3, ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand3());

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
    }
}