import { Component, OnInit } from '@angular/core';
import { MusicMakerService } from 'src/app/core/services/music-maker-service';
import { ArpeggioPattern, ArpeggioPatternRow, ChordChange, MakeMidiFromArpeggioCommand } from 'src/app/core/services/server-client';
import { environment } from 'src/environments/environment';
import { ArpTrackViewModel } from './arp-track-view-model';

@Component({
  selector: 'app-edit-arpeggio',
  templateUrl: './edit-arpeggio.component.html',
  styleUrls: ['./edit-arpeggio.component.scss']
})
export class EditArpeggioComponent implements OnInit {

  tempo: number = 120;
  beatsPerMeasure: number = 4;
  numberOfMeasures: number = 1;
  tracks: ArpTrackViewModel[];
  midiUrl: string;
  currentFile: string = '';

  constructor(private musicMakerService: MusicMakerService) {
    this.tracks = [];
    this.currentFile = "fc166a5b-22dd-41b0-86b5-d9c0144edd18";
    this.midiUrl = `${environment.apiUrl}/api/MediaFiles/v1/File/${this.currentFile}.mid`;
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
    this.setupOctave(1);
    this.setupOctave(0);    
  }

  private setupOctave(octave: number) {
    let aTrack: ArpTrackViewModel;
        
    aTrack = new ArpTrackViewModel("V | " + octave,3,octave, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    aTrack = new ArpTrackViewModel("III | " + octave,octave,1, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);

    aTrack = new ArpTrackViewModel("Root | " + octave,0,octave, this.numberOfMeasures, this.beatsPerMeasure);
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
    command.pattern.instrumentNumber = 1;

    command.id = this.currentFile;
    command.chordChanges = [];
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

    console.log(command);
    return command;
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
