using MusicMaker.Core.Enums;

namespace MusicMaker.Core.ValueObjects
{
    public class ChordNotes
    {
        public ChordNotes(int[] notes, string noteName, ChordType chordType)
        {
            if (string.IsNullOrEmpty(noteName))
            {
                throw new ArgumentException($"'{nameof(noteName)}' cannot be null or empty.", nameof(noteName));
            }

            Notes = notes ?? throw new ArgumentNullException(nameof(notes));
            NoteName = noteName;
            ChordType = chordType;
        }

        public string NoteName { get; set; }
        public int[] Notes { get; set; }
        public ChordType ChordType { get; set; }

    }

}