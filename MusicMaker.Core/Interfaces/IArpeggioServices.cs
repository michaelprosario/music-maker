using MusicMaker.Core.Requests;
using MusicMaker.Core.Responses;

namespace MusicMaker.Core.Services
{
    public interface IArpeggioServices
    {
        CommonResponse MakeMidiFromArpeggio(MakeMidiFromArpeggioCommand command, string outputFilePath);
    }
}