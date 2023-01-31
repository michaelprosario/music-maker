import { ArpeggioPattern, ArpeggioPatternRow, ChordChange, MakeMidiFromArpeggioCommand } from 'src/app/core/services/server-client';
import { ArpeggioPatternRowType } from 'src/app/core/enums/arp-pattern-row-type';
import { ArpTrackViewModel } from './arp-track-view-model';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MusicMakerService } from 'src/app/core/services/music-maker-service';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-edit-arpeggio',
  templateUrl: './edit-arpeggio.component.html',
  styleUrls: ['./edit-arpeggio.component.scss']
})
export class EditArpeggioComponent implements OnInit {

  tempo: number = 90;
  beatsPerMeasure: number = 4;
  numberOfMeasures: number = 1;
  tracks: ArpTrackViewModel[];
  midiUrl: string;
  currentId: string = '';
  instrument: number = 11;
  chordProgressionString: string = 'Am G F E';

  constructor(private musicMakerService: MusicMakerService) {
    this.tracks = [];
    this.setCurrentFile();
    this.midiUrl = `${environment.apiUrl}/api/MediaFiles/v1/File/${this.currentId}.mid`;
  }

  private setCurrentFile() {
    this.currentId = uuidv4();
  }  

  onDownload(){
    window.location.href=this.midiUrl;
  }

  onGetTracks(){
    console.log(this.tracks)
  }

  ngOnInit(): void {
    this.setupTrackRows();
  }

  private setupTrackRows() {
    this.tracks = [];    
    this.setupOctave(2);
    this.setupOctave(1);
    this.setupOctave(0);    
  }

  private setupOctave(octave: number) {
    let aTrack: ArpTrackViewModel;

    aTrack = new ArpTrackViewModel("II",ArpeggioPatternRowType.Second, octave, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);    
    aTrack = new ArpTrackViewModel("V",ArpeggioPatternRowType.Fifth,octave, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    
    aTrack = new ArpTrackViewModel("III",ArpeggioPatternRowType.Third, octave, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    
    aTrack = new ArpTrackViewModel("Root",ArpeggioPatternRowType.Root,octave, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    return aTrack;
  }

  onClearTracks(){
    this.setupTrackRows();
    for (let track of this.tracks) {
      for (let j = 0; j < track.trackData.length; j++) {
          track.trackData[j] = 0;
      }
    }
  }

  async onPlayTracks()
  {
    let command = this.buildCommand();

    let response = await this.musicMakerService.makeMidiFromArpeggio(command).toPromise();
    console.log(response);

    this.playCurrentFile();
  }

  private playCurrentFile() {
    let midiPlayer = document.getElementById("midiPlayer");
    // @ts-ignore
    midiPlayer.reload();
    // @ts-ignore
    setTimeout(() => { midiPlayer.start(); }, 3000);
  }

  private buildCommand() : MakeMidiFromArpeggioCommand {
    //debugger;
    let command = new MakeMidiFromArpeggioCommand();
    command.beatsPerMinute = this.tempo;
    command.userId = "user1";
    command.channel = 1;
    
    command.pattern = new ArpeggioPattern();
    command.pattern.rows = this.getTracks();
    command.instrument = this.instrument;

    command.id = this.currentId;    
    command.chordChangesAsString = this.chordProgressionString;    

    console.log(command);
    return command;
  }

  private setupChordProgression1(command: MakeMidiFromArpeggioCommand) {
    command.chordChanges.push(new ChordChange({
      "beatCount": 4,
      "chordRoot": 69,
      "chordType": 1
    }));
    command.chordChanges.push(new ChordChange({
      "beatCount": 4,
      "chordRoot": 67,
      "chordType": 0
    }));
    command.chordChanges.push(new ChordChange({
      "beatCount": 4,
      "chordRoot": 65,
      "chordType": 0
    }));
    command.chordChanges.push(new ChordChange({
      "beatCount": 4,
      "chordRoot": 64,
      "chordType": 0
    }));
  }

  private setupChordProgression2(command: MakeMidiFromArpeggioCommand) {
    command.chordChanges.push(new ChordChange({
      "beatCount": 4,
      "chordRoot": 48,
      "chordType": 0
    }));
    command.chordChanges.push(new ChordChange({
      "beatCount": 4,
      "chordRoot": 57,
      "chordType": 1
    }));
    command.chordChanges.push(new ChordChange({
      "beatCount": 4,
      "chordRoot": 53,
      "chordType": 0
    }));
    command.chordChanges.push(new ChordChange({
      "beatCount": 4,
      "chordRoot": 55,
      "chordType": 0
    }));
  }


  getTracks(): ArpeggioPatternRow[] {
    let arpTracks = [];
    for(let track of this.tracks)
    {
      let arpTrackRow = new ArpeggioPatternRow();
      arpTrackRow.type = track.rowType;
      arpTrackRow.octave = track.octave;
      let arpPatternString = "";
      for(let i=0; i< track.trackData.length; i++)
      {
        let currentValue = track.trackData[i];
        if(currentValue > 0)
        {
          arpPatternString += "s"
        }else{
          arpPatternString += "-"
        }
      }
      arpTrackRow.pattern = arpPatternString;

      arpTracks.push(arpTrackRow);
    }

    return arpTracks;
  }
}
