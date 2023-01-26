using System;
using System.Collections.Generic;
using System.IO;
using Melanchall.DryWetMidi.MusicTheory;
using MusicMaker.Core.Enums;
using MusicMaker.Core.Requests;
using MusicMaker.Core.Services;
using MusicMaker.Core.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace MusicMaker.Tests
{
    [TestFixture]
    public class ArpeggioPatternValidatorTests
    {
        [TestCase]
        public void ArpeggioPatternRowValidator__HandleValidData()
        {
            var command = ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand1();
            var validator = new ArpeggioPatternRowValidator();
            var result = validator.Validate(command.Pattern.Rows[0]);
            Assert.True(result.IsValid);
        }

        [TestCase]
        public void MakeMidiFromArpeggioCommandValidator__PassingCase()
        {
            var command = GetMakeMidiFromArpeggioCommand();
            var validator = new MakeMidiFromArpeggioCommandValidator();
            var validationResult = validator.Validate(command);


            DefaultContractResolver contractResolver = new()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
            string jsonString = JsonConvert.SerializeObject(command, jsonSettings);

            File.WriteAllText(@"makeMidiFromArp.json", jsonString);

            Assert.IsTrue(validationResult is { IsValid: true });
        }

        [TestCase]
        public void MakeMidiFromArpeggioCommandValidator__FailingCase()
        {
            var command = GetMakeMidiFromArpeggioCommand();
            command.Pattern.Rows[2].Pattern = ""; // clear a pattern
            var validator = new MakeMidiFromArpeggioCommandValidator();
            var validationResult = validator.Validate(command);
            Assert.IsTrue(validationResult is { IsValid: false });
        }

        private static MakeMidiFromArpeggioCommand GetMakeMidiFromArpeggioCommand()
        {
            var command = new MakeMidiFromArpeggioCommand
            {
                Id = Guid.NewGuid(),
                Instrument = 1,
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
                    }
                },
                UserId = "mrosario",
                BeatsPerMinute = 80,
                Channel = 1,
                ChordChanges = new List<ChordChange>
                {
                    new(Note.Parse("A4").NoteNumber, ChordType.Minor, 4),
                    new(Note.Parse("G4").NoteNumber, ChordType.Major, 4),
                    new(Note.Parse("F4").NoteNumber, ChordType.Major, 4),
                    new(Note.Parse("E4").NoteNumber, ChordType.Major, 4)
                }
            };
            return command;
        }
    }
}