﻿using System.IO;
using Melanchall.DryWetMidi.Core;
using MusicMaker.Core.Requests;
using MusicMaker.Core.Responses;
using MusicMaker.Core.Services;

namespace MusicMaker.Infra
{
    public class ArpeggioServices : IArpeggioServices
    {
        public CommonResponse MakeMidiFromArpeggio(MakeMidiFromArpeggioCommand command, string outputFilePath)
        {
            CommonResponse response = new();
            var tempo = command.BeatsPerMinute;
            var instrument = command.Instrument;
            var channel = command.Channel;

            var track = new ChordPlayerTrack(instrument, channel, tempo);

            var player = new ArpeggioPlayer(track, command);
            var chordChanges = command.ChordChanges;

            player.PlayFromChordChanges(chordChanges);

            var midiFile = new MidiFile();
            midiFile.Chunks.Add(track.MakeTrackChunk());
            midiFile.Write(outputFilePath + Path.DirectorySeparatorChar + command.Id + ".mid", true);

            return response;
        }
    }
}