using System.Collections.Generic;
using System.Runtime.Serialization;
using FluentValidation.Results;

namespace MusicMaker.Core.Responses
{
    [DataContract]
    public class CommonResponse
    {
        [DataMember] public int Code { get; set; } = 200;
        [DataMember] public List<ValidationFailure> Errors { get; set; } = new();
        [DataMember] public string Message { get; set; } = "ok";
    }
}