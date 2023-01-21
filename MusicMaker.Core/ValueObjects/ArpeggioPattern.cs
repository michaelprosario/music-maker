using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MusicMaker.Core.ValueObjects
{
    [DataContract]
    public enum ArpeggioPatternRowType
    {
        Root,
        Second,
        Third,
        Fifth,
        Sixth,
        Seventh,
        MajorSeventh
    }

    [DataContract]
    public class ArpeggioPatternRow
    {
        [DataMember] public int Octave { get; set; }
        [DataMember] public ArpeggioPatternRowType Type { get; set; }
        [DataMember] public string Pattern { get; set; } = "";
    }

    [DataContract]
    public class ArpeggioPattern
    {
        [DataMember] public List<ArpeggioPatternRow> Rows { get; set; } = new();
    }
}