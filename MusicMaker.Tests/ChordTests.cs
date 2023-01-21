using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;
using MusicMaker.Core.Enums;
using MusicMaker.Core.Interfaces;
using MusicMaker.Core.Services;
using MusicMaker.Core.ValueObjects;
using MusicMaker.Infra;
using NUnit.Framework;

namespace MusicMaker.Tests
{
    [TestFixture]
    public class ChordTests
    {
        [Test]
        public void ChordServices__MakeChord__HandleValidData()
        {
            // arrange
            IMidiServices midiServices = new MidiServices();
            var service = new ChordServices(midiServices);

            // act
            var response = service.MakeChordNotes("C4", ChordType.Major7);

            // assert
            Assert.True(response.Notes.Length == 4);
        }

        [Test]
        public void BaseOnePlayer__Test1()
        {
            var tempo = 180;
            var instrument = (byte)Instruments.ElectricPiano1;
            var channel = 1;

            var track = new ChordPlayerTrack(instrument, channel);

            var player = new BassOnePlayer(track);
            var chordChanges = new List<ChordChange>
            {
                new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("A4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("F4").NoteNumber, ChordType.Major, 4),
                new(Note.Parse("G4").NoteNumber, ChordType.Major, 4)
            };

            player.PlayFromChordChanges(chordChanges);

            var midiFile = new MidiFile();
            midiFile.Chunks.Add(track.MakeTrackChunk());
            midiFile.Write("bass3.mid", true);
        }
    }
}