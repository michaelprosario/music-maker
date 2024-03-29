﻿using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Interaction;
using MusicMaker.Core.Enums;
using MusicMaker.Core.Requests;
using MusicMaker.Core.Services;
using MusicMaker.Core.ValueObjects;
using Note = Melanchall.DryWetMidi.MusicTheory.Note;

namespace MusicMaker.Infra
{
  public class ArpeggioPlayer : AbstractChordPlayer
  {
    private readonly MakeArpeggioPatternCommand _command;
    private readonly ChordPlayerTrack _track;

    public ArpeggioPlayer(ChordPlayerTrack track, MakeArpeggioPatternCommand command) : base(track)
    {
      _track = track ?? throw new ArgumentNullException(nameof(track));
      _command = command ?? throw new ArgumentNullException(nameof(command));
    }

    public override void PlayOneBarPattern(ChordChange chordChange)
    {
      var arpPatternRows = _command.Pattern.CopyFirstMeasures(1).Rows;
      playChordChanges(chordChange, arpPatternRows); 
    }

    public override void PlayTwoBarPattern(ChordChange chordChange)
    {
      var arpPatternRows = _command.Pattern.CopyFirstMeasures(2).Rows;
      playChordChanges(chordChange, arpPatternRows); 
    }

    public override void PlayThreeBarPattern(ChordChange chordChange)
    {
      var arpPatternRows = _command.Pattern.CopyFirstMeasures(3).Rows;
      playChordChanges(chordChange, arpPatternRows);        
    }

    public override void PlayFourBarPattern(ChordChange chordChange)
    {
      var arpPatternRows = _command.Pattern.Rows;
      playChordChanges(chordChange, arpPatternRows);
    }

    private void playChordChanges(ChordChange chordChange, List<ArpeggioPatternRow> arpPatternRows)
    {
      // https://melanchall.github.io/drywetmidi/articles/composing/Pattern.html
      var patterns = new List<Pattern>();
      foreach (var patternRow in arpPatternRows)
      {
        var pattern = MakePattern(patternRow, chordChange);
        patterns.Add(pattern);
      }

      var responsePattern = patterns.CombineInParallel();
      _track.PatternBuilder.Pattern(responsePattern);
    }

    private Pattern MakePattern
        (
            ArpeggioPatternRow arpPatternRow,
            ChordChange chordChange
        )
    {
      var defaultNoteLength = MusicalTimeSpan.Sixteenth;
      var defaultVelocity = (SevenBitNumber)90;
      var pattern = new PatternBuilder()
          .SetNoteLength(defaultNoteLength)
          .SetVelocity(defaultVelocity);

      var noteNumber = GetNoteNumber(arpPatternRow, chordChange);

      foreach (var character in arpPatternRow.Pattern)
        switch (character)
        {
          case 's':
            {
              var instrumentNote = noteNumber;
              pattern.SetNoteLength(MusicalTimeSpan.Sixteenth);
              pattern.Note(Note.Get((SevenBitNumber)instrumentNote));
              break;
            }
          case 'e':
            {
              var instrumentNote = noteNumber;
              pattern.SetNoteLength(MusicalTimeSpan.Eighth);
              pattern.Note(Note.Get((SevenBitNumber)instrumentNote));
              break;
            }
          case 'q':
            {
              var instrumentNote = noteNumber;
              pattern.SetNoteLength(MusicalTimeSpan.Quarter);
              pattern.Note(Note.Get((SevenBitNumber)instrumentNote));
              break;
            }
          case 'h':
            {
              var instrumentNote = noteNumber;
              pattern.SetNoteLength(MusicalTimeSpan.Half);
              pattern.Note(Note.Get((SevenBitNumber)instrumentNote));
              break;
            }
          case 'w':
            {
              var instrumentNote = noteNumber;
              pattern.SetNoteLength(MusicalTimeSpan.Whole);
              pattern.Note(Note.Get((SevenBitNumber)instrumentNote));
              break;
            }
          case '-':
            pattern.StepForward(MusicalTimeSpan.Sixteenth);
            break;
        }

      return pattern.Build();
    }

    private static int GetNoteNumber(ArpeggioPatternRow track, ChordChange chordChange)
    {
      // calculate noteNumber
      var noteNumber = chordChange.ChordRoot;

      // move the instrument number up based on octave
      noteNumber = noteNumber + (track.Octave - 1) * 24;

      switch (track.Type)
      {
        case ArpeggioPatternRowType.Root:
          break;
        case ArpeggioPatternRowType.Second:
          noteNumber += 2;
          break;
        case ArpeggioPatternRowType.Third:
          if (chordChange.ChordType == ChordType.Minor || chordChange.ChordType == ChordType.Minor7)
            noteNumber += 3;
          else
            // chord is some kind of major chord
            noteNumber += 4;

          break;
        case ArpeggioPatternRowType.Fifth:
          noteNumber += 7;
          break;
        case ArpeggioPatternRowType.Sixth:
          noteNumber += 9;
          break;
        case ArpeggioPatternRowType.Seventh:
          noteNumber += 10;
          break;
        case ArpeggioPatternRowType.MajorSeventh:
          noteNumber += 11;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      return noteNumber;
    }
  }
}