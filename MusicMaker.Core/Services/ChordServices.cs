using System;
using System.Collections.Generic;
using MusicMaker.Core.Enums;
using MusicMaker.Core.Interfaces;
using MusicMaker.Core.ValueObjects;

namespace MusicMaker.Core.Services
{
    public interface IChordServices
    {
        ChordNotes MakeChordNotes(string root, ChordType type);
        List<ChordChange> ParseChordProgression(string chordProgressionString);
    }

    public class ChordServices : IChordServices
    {
        private readonly IMidiServices _midiServices;

        public ChordServices(IMidiServices midiServices)
        {
            _midiServices = midiServices ?? throw new ArgumentNullException(nameof(midiServices));
        }

        public List<ChordChange> ParseChordProgression(string chordProgressionString)
        {
            if (string.IsNullOrEmpty(chordProgressionString)) return new List<ChordChange>();

            var response = new List<ChordChange>();
            string[] symbols = chordProgressionString.Split(" ");

            foreach (string symbol in symbols)
            {
                Console.WriteLine(symbol);
                if(symbol.Contains(":"))
                {
                    string[] parts = symbol.Split(':');
                    Console.WriteLine(parts[0]);
                    Console.WriteLine(parts[1]);

                    string chordSymbol = parts[0];
                    int count = int.Parse(parts[1]);
                    ChordChange chordChange = _midiServices.ParseChordSymbol(chordSymbol);
                    chordChange.BeatCount = count;
                    response.Add(chordChange);
                }
                else
                {
                    ChordChange chordChange = _midiServices.ParseChordSymbol(symbol);
                    response.Add(chordChange);
                }
            }

            return response;
        }

        public ChordNotes MakeChordNotes(string root, ChordType type)
        {
            var intRoot = _midiServices.GetNoteNumber(root);
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

            return new ChordNotes(aChord.ToArray(), root, type);
        }
    }
}