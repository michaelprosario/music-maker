using System.Runtime.Serialization;
using MusicMaker.Core.ValueObjects;

namespace MusicMaker.Core.Requests
{
    public class MakeArpeggioPatternCommand : IRequest
    {
        [DataMember] public ArpeggioPattern Pattern { get; set; } = new();
        [DataMember] public int BeatsPerMinute { get; set; } = 120;
        [DataMember] public byte Channel { get; set; }
        [DataMember] public string UserId { get; set; } = "";
    }
}