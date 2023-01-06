namespace MusicMaker.Core.ValueObjects
{
    public class Chord
    {
        public string ChordName;
        public string ChordType;

        private Chord(string root, string type)
        {
            ChordName = root;
            ChordType = type;
        }
    }
}