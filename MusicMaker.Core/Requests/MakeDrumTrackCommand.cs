using System.Collections.Generic;

namespace MusicMaker.Core.Requests
{
    public class DrumTrackRow
    {
        public int InstrumentNumber = 0;
        public string Pattern = "";
    }

    public class MakeDrumTrackCommand : IRequest
    {
        public MakeDrumTrackCommand()
        {
            BeatsPerMinute = 120;
            Tracks = new List<DrumTrackRow>();
            UserId = "";
        }

        public int BeatsPerMinute { get; set; }
        public List<DrumTrackRow> Tracks { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
    }
}