using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MusicMaker.Core.Requests
{
    [DataContract]
    public class DrumTrackRow
    {
        [DataMember] public int InstrumentNumber { get; set; }
        [DataMember] public string Pattern { get; set; } = "";
    }

    [DataContract]
    public class MakeDrumTrackCommand : IRequest
    {
        [DataMember] public int BeatsPerMinute { get; set; }
        [DataMember] public List<DrumTrackRow> Tracks { get; set; }
        [DataMember] public string FileName { get; set; }
        [DataMember] public string UserId { get; set; }
    }
}