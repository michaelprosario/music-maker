using MusicMaker.Core.Requests;
using MusicMaker.Core.Services;

namespace MusicMaker.Core.Interfaces
{
    public interface IMidiServices
    {
        MakeDrumTrackResponse MakeDrumTrack(MakeDrumTrackCommand command);
        int GetNoteNumber(string note);
    }
}