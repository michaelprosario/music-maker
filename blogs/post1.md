Curious about making music with code? As a software engineer and music guy, I have enjoyed seeing the connections between music and computers.   The first computer programmer, Ada Lovelace predicted that computers will move beyond doing boring stats problems into the world of creative arts.   If a problem can be converted to a system of symbols, she reasoned that computers could help.  She used music as her example.

In previous experiments, I have explored the ideas of code and music using TypeScript, NodeJs, and Angular.  You can find this work here.  

- [NG-Music-Maker](https://github.com/michaelprosario/ng-music-maker)

After looking around GitHub, I found a really cool music library for C# devs. I'm hoping to use it create tools to make quick practice backup tracks for practicing improv. It's just fun to explore electronic music, music theory, and computational music.  Make sure to check out the blog post by Maxim.  It's a pretty comprehensive guide to his library.

- [drywetmidi](https://github.com/melanchall/drywetmidi)
- [DryWetMIDI High level-processing of-MIDI files](https://www.codeproject.com/Articles/1200014/DryWetMIDI-High-level-processing-of-MIDI-files)


# What is MIDI?
- talk about MIDI for the novice


# DryWetMidi Features
- Reading MIDI files
- Writing MIDI files
    - low level
    - higher level api
- Productive composition API
- Tools to playback MIDI files on Windows
- MIDI chord extraction - 
- Device interaction
    - Sending MIDI events
    - Receiving events



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

``` csharp
var tempo = 180;
var instrument = (byte)Instruments.AcousticGrandPiano;
var channel = 1;

var track = new ChordPlayerTrack(instrument, channel, tempo);
var command = ArpeggioPatternCommandFactory.MakeArpeggioPatternCommand1();
var player = new ArpeggioPlayer(track, );
var chordChanges = GetChords1();

player.PlayFromChordChanges(chordChanges);

var midiFile = new MidiFile();
midiFile.Chunks.Add(track.MakeTrackChunk());
midiFile.Write("arp1.mid", true);
```


