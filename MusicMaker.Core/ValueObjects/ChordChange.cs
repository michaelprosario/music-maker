using MusicMaker.Core.Enums;

namespace MusicMaker.Core.ValueObjects
{
    public class ChordChange
    {
        public int BeatCount;
        public int ChordRoot;
        public ChordType ChordType;

        public ChordChange(int root, ChordType chordType, int beats)
        {
            ChordRoot = root;
            ChordType = chordType;
            BeatCount = beats;
        }
    }
}