using MusicMaker.Core.Enums;
using MusicMaker.Core.Interfaces;

namespace MusicMaker.Core.Services
{
    public class ChordServices
    {
        private readonly IMidiServices _midiServices;

        public ChordServices(IMidiServices midiServices)
        {
            _midiServices = midiServices ?? throw new ArgumentNullException(nameof(midiServices));
        }

        public int[] MakeChordNoteNumbers(string root, ChordType type)
        {
            int intRoot = _midiServices.GetNoteNumber(root);
            var aChord = new List<int>();

            if (type == ChordType.Major)
            {
                aChord.Add(intRoot);
                aChord.Add(intRoot + 4);
                aChord.Add(intRoot + 7);
            }
            else if (type == ChordType.Minor)
            {
                aChord.Add(intRoot);
                aChord.Add(intRoot + 3);
                aChord.Add(intRoot + 7);

            }
            else if (type == ChordType.Major7)
            {
                aChord.Add(intRoot);
                aChord.Add(intRoot + 4);
                aChord.Add(intRoot + 7);
                aChord.Add(intRoot + 11);
            }
            else if (type == ChordType.M7)
            {
                aChord.Add(intRoot);
                aChord.Add(intRoot + 4);
                aChord.Add(intRoot + 7);
                aChord.Add(intRoot + 10);
            }
            else if (type == ChordType.Minor7)
            {
                aChord.Add(intRoot);
                aChord.Add(intRoot + 3);
                aChord.Add(intRoot + 7);
                aChord.Add(intRoot + 10);
            }
            else
            {
                throw new Exception("Help...Handle new chord type");
            }

            return aChord.ToArray();
        }

    }
}