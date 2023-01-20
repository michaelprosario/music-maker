using System;
using MusicMaker.Core.Interfaces;
using MusicMaker.Core.Requests;
using MusicMaker.Core.Responses;

namespace MusicMaker.Core.Services
{
    public class MakeDrumTrackResponse : CommonResponse
    {
    }

    public class MakeDrumTrackService
    {
        private readonly IMidiServices _midiServices;

        public MakeDrumTrackService(IMidiServices midiServices)
        {
            _midiServices = midiServices ?? throw new ArgumentNullException(nameof(midiServices));
        }

        public MakeDrumTrackResponse MakeDrumTrack(MakeDrumTrackCommand command, string outputPath)
        {
            if (command == null) throw new ArgumentException("command is required");
            if (string.IsNullOrEmpty(outputPath)) throw new ArgumentException("outputPath is required");

            MakeDrumTrackResponse response = new();

            // make sure command is filled out
            if (command.Tracks.Count == 0)
            {
                response.Message = "command.tracks is not defined";
                response.Code = 400;
                return response;
            }

            // make sure user is defined
            if (string.IsNullOrEmpty(command.UserId))
            {
                response.Message = "command.userId is not defined";
                response.Code = 400;
                return response;
            }

            if (string.IsNullOrEmpty(command.FileName))
            {
                response.Message = "command.FileName is not defined";
                response.Code = 400;
                return response;
            }

            return _midiServices.MakeDrumTrack(command, outputPath);
        }
    }
}