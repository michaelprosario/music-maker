using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MusicMaker.Core.ValueObjects;

namespace MusicMaker.Core.Requests
{
    public class MakeMidiFromArpeggioCommand : MakeArpeggioPatternCommand
    {
        [DataMember] public byte Instrument { get; set; } = 1;
        [DataMember] public Guid Id { get; set; }
        [DataMember] public List<ChordChange> ChordChanges { get; set; } = new();
    }
}