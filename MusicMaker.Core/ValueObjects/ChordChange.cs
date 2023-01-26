using System.Runtime.Serialization;
using MusicMaker.Core.Enums;

namespace MusicMaker.Core.ValueObjects
{
    [DataContract]
    public class ChordChange
    {
        public ChordChange(int root, ChordType chordType, int beats)
        {
            ChordRoot = root;
            ChordType = chordType;
            BeatCount = beats;
        }

        public ChordChange()
        {
        }

        [DataMember] public int BeatCount { get; set; }

        [DataMember] public int ChordRoot { get; set; }

        [DataMember] public ChordType ChordType { get; set; }
    }
}