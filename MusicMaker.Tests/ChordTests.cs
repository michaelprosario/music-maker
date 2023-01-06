using System.Collections.Generic;
using System.IO;
using MusicMaker.Core.Enums;
using MusicMaker.Core.Interfaces;
using MusicMaker.Core.Requests;
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
            var response = service.MakeChordNoteNumbers("C4", ChordType.Major7);

            // assert
            Assert.True(response.Length == 4);
        }
    }
}