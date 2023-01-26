using MusicMaker.Core.Requests;
using MusicMaker.Core.Services;
using MusicMaker.Core.ValueObjects;

namespace MusicMaker.Core.Interfaces
{
    public interface IMidiServices
    {
        MakeDrumTrackResponse MakeDrumTrack(MakeDrumTrackCommand command, string outputPath);
        int GetNoteNumber(string note);
        ChordChange ParseChordSymbol(string symbol);
    }
}