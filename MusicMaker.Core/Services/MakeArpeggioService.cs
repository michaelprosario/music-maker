using System;
using System.Collections.Generic;
using MusicMaker.Core.Requests;
using MusicMaker.Core.Responses;
using MusicMaker.Core.ValueObjects;

namespace MusicMaker.Core.Services
{
    public interface IMakeArpeggioService
    {
        CommonResponse MakeMidiFromArpeggio(MakeMidiFromArpeggioCommand command, string outputFilePath);
    }

    public class MakeArpeggioService : IMakeArpeggioService
    {
        private readonly IArpeggioServices _arpeggioServices;
        private readonly IChordServices _chordServices;

        public MakeArpeggioService(IArpeggioServices arpeggioServices, IChordServices chordServices)
        {
            _arpeggioServices = arpeggioServices ?? throw new ArgumentNullException(nameof(arpeggioServices));
            _chordServices = chordServices ?? throw new ArgumentNullException(nameof(chordServices));
        }

        public CommonResponse MakeMidiFromArpeggio(MakeMidiFromArpeggioCommand command, string outputFilePath)
        {
            var response = new CommonResponse();
            var validator = new MakeMidiFromArpeggioCommandValidator();

            // make chord changes if needed from string
            if (!string.IsNullOrEmpty(command.ChordChangesAsString))
            {
                List<ChordChange> chordChanges = _chordServices.ParseChordProgression(command.ChordChangesAsString);
                command.ChordChanges = chordChanges;
            }

            var validationResults = validator.Validate(command);
            if (validationResults.Errors.Count > 0)
            {
                response.Code = 400;
                response.Message = "Invalid data on command";
                response.Errors = validationResults.Errors;
                return response;
            }

            return _arpeggioServices.MakeMidiFromArpeggio(command, outputFilePath);
        }
    }
}