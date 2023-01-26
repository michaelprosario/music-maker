using System.Collections.Generic;
using MusicMaker.Core.Enums;
using MusicMaker.Core.Interfaces;
using MusicMaker.Core.Services;
using MusicMaker.Core.ValueObjects;
using MusicMaker.Infra;
using NUnit.Framework;

namespace MusicMaker.Tests
{
    [TestFixture]
    public class ParseChordProgressionTests
    {
        [Test]
        public void ChordServices__ParseChordProgression__ParseFourBars()
        {
            // arrange
            IMidiServices midiServices = new MidiServices();
            var service = new ChordServices(midiServices);
            string chordProgressionString = "C Am F G";

            // act
            List<ChordChange> chordChanges = service.ParseChordProgression(chordProgressionString);

            // assert
            Assert.True(chordChanges.Count == 4);
            foreach (var chordChange in chordChanges) Assert.True(chordChange.BeatCount == 4);
            Assert.True(chordChanges[0].ChordType == ChordType.Major);
            Assert.True(chordChanges[1].ChordType == ChordType.Minor);
            Assert.True(chordChanges[2].ChordType == ChordType.Major);
        }
    }
}