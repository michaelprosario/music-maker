using System.Collections.Generic;
using MusicMaker.Core.Enums;
using MusicMaker.Core.Interfaces;
using MusicMaker.Core.Requests;
using MusicMaker.Core.Services;
using MusicMaker.Infra;
using NUnit.Framework;

namespace MusicMaker.Tests
{
    [TestFixture]
    public class DrumMakerTests
    {
        [Test]
        public void DrumMakerService__MakeDrumTrack__HandleValidData()
        {
            // arrange
            IMidiServices midiServices = new MidiServices();
            var service = new MakeDrumTrackService(midiServices);
            var command = new MakeDrumTrackCommand();
            command.BeatsPerMinute = 90;
            command.Tracks = new List<DrumTrackRow>
            {
                new()
                {
                    Pattern = "x---|x---|x---|x---|",
                    InstrumentNumber = DrumConstants.AcousticBassDrum
                },
                new()
                {
                    Pattern = "--x-|--x-|--x-|--x-|",
                    InstrumentNumber = DrumConstants.AcousticSnare
                }
            };
            command.UserId = "system";

            // act
            var response = service.MakeDrumTrack(command);

            // assert
            Assert.IsTrue(response != null);
        }
    }
}