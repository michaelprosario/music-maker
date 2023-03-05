using System;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;

namespace MidiPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            var midiFile = MidiFile.Read("seek-ye-first.mid");

            Console.WriteLine("Chords:");

            foreach (var chord in midiFile.GetChords())
            {
                if(chord.Notes.Count() > 1){
                    Console.Write($@"
                    chord
                    channel = {chord.Channel}
                    time = {chord.Time}
                    length = {chord.Length}
                    notes:");

                    Console.WriteLine(chord.ToString());
                }
            }
        }
    }
}