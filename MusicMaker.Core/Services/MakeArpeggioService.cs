using System;
using MusicMaker.Core.Requests;
using MusicMaker.Core.Responses;

namespace MusicMaker.Core.Services
{
    public class MakeArpeggioService
    {
        private readonly IArpeggioServices _arpeggioServices;

        public MakeArpeggioService(IArpeggioServices arpeggioServices)
        {
            _arpeggioServices = arpeggioServices ?? throw new ArgumentNullException(nameof(arpeggioServices));
        }

        private CommonResponse MakeMidiFromArpeggio(MakeMidiFromArpeggioCommand command, string outputFilePath)
        {
            var response = new CommonResponse();
            var validator = new MakeMidiFromArpeggioCommandValidator();
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