Curious about making music with code? As a software engineer and music guy, I have enjoyed seeing the connections between music and computers.   The first computer programmer, Ada Lovelace predicted that computers will move beyond doing boring math problems into the world of creative arts.   If a problem can be converted to a system of symbols, she reasoned that computers could help.  She used music as her example.

In previous experiments, I have explored the ideas of code and music using TypeScript, NodeJs, and Angular.  You can find this work here.  

- [NG-Music-Maker](https://github.com/michaelprosario/ng-music-maker)

After looking around GitHub, I found a really cool music library for C# devs. I'm hoping to use it to create tools to make quick backup tracks for practicing improv. It's just fun to explore electronic music, theory, and computational music.  Make sure to check out the blog post by Maxim. ( the author of DryWetMidi )  It's a pretty comprehensive guide to his library.  

- [DryWetMidi](https://github.com/melanchall/drywetmidi)
- [DryWetMIDI High level-processing of-MIDI files](https://www.codeproject.com/Articles/1200014/DryWetMIDI-High-level-processing-of-MIDI-files)

## What is MIDI?
MIDI stands for musical instrument digital interface.  (MIDI)  Under a file format like WAV or MP3, the computer stores the raw wave form data about sound.  The MIDI file format and protocols operate an conceptual later of music data.  You can think of a MIDI file having many tracks.   You can assign different instruments ( sounds to tracks ).   In each track, the musician can record songs as many events.   MIDI music events might include turning a note on, turning a note off, engaging the sustain pedal, and changing tempo.  MIDI music software like Garage band, Cakewalk and Bandlab can send the MIDI event data to a software synth which interprets the events into sound.  In general, the MIDI event paradigm can be extended to support other things like lighting, lyrics, and other stuff.   

## DryWetMidi Features
- Writing MIDI files: For my experiments, I have used DryWetMIDI to explore projects for making drum machines and arpeggio makers.   I'm really curious about using computers to generate the skeleton of songs.  Can computers generate a template for a POP song, a fiddle tune, or a ballad?  We're about to find out!  DryWetMIDI provides a lower level API for raw MIDI event data.  The higher level "Pattern" and "PatternBuilder" APIs enable coders to think of expressing a single thread of musical ideas.  Let's say you're trying to describe a piece for a string quartet.  The "PatternBuilder" API enables you to use a fluent syntax to describe the notes played by the cello player.  While playing with this API, I have to say that I loved the ability to combine musical patterns.  The framework can stack or combine musical patterns into a single pattern. Let's say you have three violin parts in 3 patterns.  The library enables you to blend those patterns into a single idea with one line of code.  Maxim showed great care in designing these APIs. 
- Music theory tools: The framework provides good concepts for working for notes, intervals, chords and other fundamental concepts of music.
- Reading MIDI files: The early examples show that DryWetMIDI can read MIDI files well. I've seen some utility functions that enable you to dump MIDI files to CSVs to support debugging.  The documentation hints at a chord extraction API that looks really cool.  Looking forward to testing this.   
- Device interaction:   DryWetMIDI enables makers to send MIDI events and receive them. This capability might become helpful if you're making a music tutor app.  You can use the music device interaction API to watch note events.   The system can provide feedback to the player if they're playing the right notes at the appropriate time.

## Visions for MusicMaker.NET for .NET Core

In the following code example, I've built an API to describe drum patterns using strings.
The strings represent sound at a resolution of 16th notes. Using the "MakeDrumTrack" service,
we can quickly express patterns of percussion.

``` csharp 
IMidiServices midiServices = new MidiServices();
var service = new MakeDrumTrackService(midiServices);
var command = new MakeDrumTrackCommand
{
    BeatsPerMinute = 50,
    FileName = fileName,
    Tracks = new List<DrumTrackRow>
    {
        new()
        {
            Pattern = "x-x-|x-x-|x-x-|x-x-|x-x-|x-x-|x-x-|x-x-|",
            InstrumentNumber = DrumConstants.HiHat
        },
        new()
        {
            Pattern = "x---|----|x---|----|x---|----|x---|----|",
            InstrumentNumber = DrumConstants.AcousticBassDrum
        },
        new()
        {
            Pattern = "----|x---|----|x--x|----|x---|----|x--x|",
            InstrumentNumber = DrumConstants.AcousticSnare
        },
        new()
        {
            Pattern = "-x-x|x-x-|-x-x|x--x|-xx-|xx--|-xx-|x--x|",
            InstrumentNumber = DrumConstants.HiBongo
        }
    },
    UserId = "system"
};

// act
var response = service.MakeDrumTrack(command);
```

Using the ArpeggioPlayer service, we'll be able to render out a small fragement of music given a list of chords and an arpeggio spec.

``` csharp
var tempo = 180;
var instrument = (byte)Instruments.AcousticGrandPiano;
var channel = 1;

var track = new ChordPlayerTrack(instrument, channel, tempo);
var command = ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand1();
var player = new ArpeggioPlayer(track, );
var chordChanges = GetChords1();  // Am | G | F | E

player.PlayFromChordChanges(chordChanges);

// Write MIDI file with DryWetMIDI
var midiFile = new MidiFile();
midiFile.Chunks.Add(track.MakeTrackChunk());
midiFile.Write("arp1.mid", true);
```

In the following method, the maker can describe the arpeggio patterns using ASCII art strings. The arpeggio patterns operate at resolution of sixteenth notes.  This works fine for most POP or eletronic music. In future work, we can build web apps or mobile UX to enable the user to design the arpeggio patterns or drum patterns.

``` csharp
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
                new() { Type = ArpeggioPatternRowType.Root, Octave = 2, Pattern =  "---s|-s-s|---s|-s--|" },
                new() { Type = ArpeggioPatternRowType.Fifth, Octave = 1, Pattern = "--s-|s---|--s-|--s-|" },
                new() { Type = ArpeggioPatternRowType.Third, Octave = 1, Pattern = "-s--|----|-s--|----|" },
                new() { Type = ArpeggioPatternRowType.Root, Octave = 1, Pattern =  "s---|----|s---|----|" }
            },
            InstrumentNumber = Instruments.Banjo
        },
        UserId = "mrosario",
        BeatsPerMinute = 120,
        Channel = 0
    };
    return command;
}
```

If you're interested in following my work here, check out the following repo.

- [MusicMaker for DotNet Core](https://github.com/michaelprosario/music-maker)

## Arp Composer
- Make MIDI "lego blocks" for your next song
- MIDI making leverages Microsoft .NET Core and MusicMaker.NET
- Check out ng-music-maker folder for Angular source code

