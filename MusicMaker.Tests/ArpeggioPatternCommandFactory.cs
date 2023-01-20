﻿using System.Collections.Generic;
using MusicMaker.Core.Requests;
using MusicMaker.Core.ValueObjects;

namespace MusicMaker.Tests
{
    public class ArpeggioPatternCommandFactory
    {
        public static MakeArpeggioPatternCommand MakeArpeggioPatternCommand3()
        {
            var command = new MakeArpeggioPatternCommand
            {
                Pattern = new ArpeggioPattern
                {
                    Rows = new List<ArpeggioPatternRow>
                    {
                        new() { Type = ArpeggioPatternRowType.Fifth, Octave = 2, Pattern = "----|----|----|----|" },
                        new() { Type = ArpeggioPatternRowType.Third, Octave = 2, Pattern = "----|----|----|----|" },
                        new() { Type = ArpeggioPatternRowType.Root, Octave = 2, Pattern = "----|----|----|----|" },
                        new() { Type = ArpeggioPatternRowType.Fifth, Octave = 1, Pattern = "----|----|----|---s|" },
                        new() { Type = ArpeggioPatternRowType.Third, Octave = 1, Pattern = "----|----|----|----|" },
                        new() { Type = ArpeggioPatternRowType.Root, Octave = 1, Pattern = "s-s-|s-s-|s-s-|s-s-|" }
                    },
                    InstrumentNumber = Instruments.Banjo
                },
                UserId = "mrosario",
                BeatsPerMinute = 120,
                Channel = 0
            };
            return command;
        }


        public static MakeArpeggioPatternCommand MakeArpeggioPatternCommand4()
        {
            // pattern includes half and eighth note items
            var command = new MakeArpeggioPatternCommand
            {
                Pattern = new ArpeggioPattern
                {
                    Rows = new List<ArpeggioPatternRow>
                    {
                        new() { Type = ArpeggioPatternRowType.Fifth, Octave = 2, Pattern = "----|----|----|----|" },
                        new() { Type = ArpeggioPatternRowType.Third, Octave = 2, Pattern = "----|----|----|----|" },
                        new() { Type = ArpeggioPatternRowType.Root, Octave = 2, Pattern = "----|----|----|----|" },
                        new() { Type = ArpeggioPatternRowType.Fifth, Octave = 1, Pattern = "----|----|q~~~|e~--|" },
                        new() { Type = ArpeggioPatternRowType.Third, Octave = 1, Pattern = "----|----|----|--e~|" },
                        new() { Type = ArpeggioPatternRowType.Root, Octave = 1, Pattern = "h~~~|~~~~|----|----|" }
                    },
                    InstrumentNumber = Instruments.Banjo
                },
                UserId = "mrosario",
                BeatsPerMinute = 120,
                Channel = 0
            };
            return command;
        }


        public static MakeArpeggioPatternCommand MakeArpeggioPatternCommand2()
        {
            var command = new MakeArpeggioPatternCommand
            {
                Pattern = new ArpeggioPattern
                {
                    Rows = new List<ArpeggioPatternRow>
                    {
                        new() { Type = ArpeggioPatternRowType.Fifth, Octave = 2, Pattern = "----|----|----|----|" },
                        new() { Type = ArpeggioPatternRowType.Third, Octave = 2, Pattern = "----|----|----|----|" },
                        new() { Type = ArpeggioPatternRowType.Root, Octave = 2, Pattern = "----|----|----|----|" },
                        new() { Type = ArpeggioPatternRowType.Fifth, Octave = 1, Pattern = "--s-|--s-|--s-|s--s|" },
                        new() { Type = ArpeggioPatternRowType.Third, Octave = 1, Pattern = "--s-|--s-|--s-|s--s|" },
                        new() { Type = ArpeggioPatternRowType.Root, Octave = 1, Pattern = "--s-|--s-|--s-|s--s|" }
                    },
                    InstrumentNumber = Instruments.Banjo
                },
                UserId = "mrosario",
                BeatsPerMinute = 120,
                Channel = 0
            };
            return command;
        }

        public static MakeArpeggioPatternCommand MakeArpeggioPatternCommand1()
        {
            var command = new MakeArpeggioPatternCommand
            {
                Pattern = new ArpeggioPattern
                {
                    Rows = new List<ArpeggioPatternRow>
                    {
                        new() { Type = ArpeggioPatternRowType.Fifth, Octave = 2, Pattern = "----|----|----|---s|" },
                        new() { Type = ArpeggioPatternRowType.Third, Octave = 2, Pattern = "----|--s-|s---|s---|" },
                        new() { Type = ArpeggioPatternRowType.Root, Octave = 2, Pattern = "---s|-s-s|---s|-s--|" },
                        new() { Type = ArpeggioPatternRowType.Fifth, Octave = 1, Pattern = "--s-|s---|--s-|--s-|" },
                        new() { Type = ArpeggioPatternRowType.Third, Octave = 1, Pattern = "-s--|----|-s--|----|" },
                        new() { Type = ArpeggioPatternRowType.Root, Octave = 1, Pattern = "s---|----|s---|----|" }
                    },
                    InstrumentNumber = Instruments.Banjo
                },
                UserId = "mrosario",
                BeatsPerMinute = 120,
                Channel = 0
            };
            return command;
        }
    }
}