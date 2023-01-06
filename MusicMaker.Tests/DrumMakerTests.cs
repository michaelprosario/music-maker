namespace MusicMaker.Tests;
using NUnit.Framework;

[TestFixture]
public class DrumMakerTests
{
    [Test]
    public void DrumMakerService__MakeDrumTrack__HandleValidData(){
        // arrange
        IMidiServices midiServices = new MidiServices();
        var service = new MakeDrumTrackService(midiServices);
        var command = new MakeDrumTrackCommand();
        command.BeatsPerMinute = 90;
        command.Tracks = new List<DrumTrackRow>{
            new DrumTrackRow()
            {
                Pattern = "x---|x---|x---|x---|",
                InstrumentNumber = DrumConstants.AcousticBassDrum
            },
            new DrumTrackRow()
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
