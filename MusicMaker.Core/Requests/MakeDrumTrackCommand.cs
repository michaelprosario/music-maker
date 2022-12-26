
public class DrumTrackRow{
    public int InstrumentNumber = 0;
    public string Pattern = "";
}

public class MakeDrumTrackCommand : IRequest {
    public int BeatsPerMinute { get; set; }
    public List<DrumTrackRow> Tracks { get; set; } = new List<DrumTrackRow>();
    public string UserId { get; set; }
    
    public MakeDrumTrackCommand()
    {
        BeatsPerMinute = 120;
        Tracks = new List<DrumTrackRow>();
        UserId = "";
    }

}