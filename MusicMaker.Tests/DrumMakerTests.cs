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

        // act
        var response = service.MakeDrumTrack(command);

        // assert
        Assert.IsTrue(response != null);
    }

}
