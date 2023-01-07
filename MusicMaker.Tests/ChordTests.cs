using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using MusicMaker.Core.Enums;
using MusicMaker.Core.Interfaces;
using MusicMaker.Core.Requests;
using MusicMaker.Core.Services;
using MusicMaker.Core.ValueObjects;
using MusicMaker.Infra;
using Note = Melanchall.DryWetMidi.MusicTheory.Note;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

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
            int tempo = 120;
            int instrument = Instruments.Celesta;
            int channel = 1;
            
            ChordPlayerTrack track = new ChordPlayerTrack(tempo, channel, tempo);
            

            var player = new BassOnePlayer(track);
            var chordChanges = new List<ChordChange2>()
            {
                new ChordChange2(60, ChordType.Major, 4),
                new ChordChange2(65, ChordType.Major, 4),
                new ChordChange2(67, ChordType.Major, 4),
                new ChordChange2(65, ChordType.Major, 4),
            };

            player.PlayFromChordChanges(track, chordChanges, channel);
            
            var midiFile = new MidiFile();
            midiFile.Chunks.Add(track.MakeTrackChunk());
            midiFile.Write("bass3.mid", true);
        }
    }
}