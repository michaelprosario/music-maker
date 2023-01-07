
catch
- what
- where
- when
- why
- how



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
var instrument = Instruments.ElectricPiano1;
var channel = 1;

ChordPlayerTrack track = new(instrument, channel, tempo);

var player = new BassOnePlayer(track);
var chordChanges = new List<ChordChange2>
{
    new(Note.Parse("C4").NoteNumber, ChordType.Major, 4),
    new(Note.Parse("A4").NoteNumber, ChordType.Major, 4),
    new(Note.Parse("F4").NoteNumber, ChordType.Major, 4),
    new(Note.Parse("G4").NoteNumber, ChordType.Major, 4)
};

player.PlayFromChordChanges(track, chordChanges, channel);

var midiFile = new MidiFile();
midiFile.Chunks.Add(track.MakeTrackChunk());
midiFile.Write("bass3.mid", true);
```


