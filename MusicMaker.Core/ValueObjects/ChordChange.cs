using System.Collections.Generic;

namespace MusicMaker.Core.ValueObjects
{
    public class ChordChange
    {
        public List<int> Chord;
        public int Length;

        private ChordChange(List<int> chord, int length)
        {
            Chord = chord;
            Length = length;
        }
    }
}