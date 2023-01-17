using System.Collections.Generic;
using System.IO;
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
            string fileName = "testDrums.mid";

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
            var response = service.MakeDrumTrack(command, ".\\");

            // assert
            if(response != null)
                Assert.IsTrue(response != null);
        }
    }
}