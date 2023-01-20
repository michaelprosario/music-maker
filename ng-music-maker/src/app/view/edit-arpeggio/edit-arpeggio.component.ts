import { Component, Input, OnInit } from '@angular/core';
import { MusicMakerService } from 'src/app/core/services/music-maker-service';
import { DrumTrackRow, MakeDrumTrackCommand } from 'src/app/core/services/server-client';
import { DrumTrackViewModel } from '../edit-drum-track/drum-track-view-model';
import { environment } from 'src/environments/environment';

/*
next steps
--- create command to test arp
--- create row component
--- create cell component


data required to play an arpeggio
- chord progression 
- instrument x
- channel x
- tempo x
- ArpeggioPattern x
- File x

*/
@Component({
  selector: 'app-edit-arpeggio',
  templateUrl: './edit-arpeggio.component.html',
  styleUrls: ['./edit-arpeggio.component.scss']
})
export class EditArpeggioComponent implements OnInit {

  tempo: number = 120;
  beatsPerMeasure: number = 4;
  numberOfMeasures: number = 4;
  tracks: DrumTrackViewModel[];
  midiUrl: string;
  currentFile: string = '';

  constructor(private musicMakerService: MusicMakerService) {
    this.tracks = [];
    this.currentFile = "drumTest1.mid";
    this.midiUrl = `${environment.apiUrl}/api/MediaFiles/v1/File/${this.currentFile}`;
  }

  onDownload(){
    window.location.href=this.midiUrl;
  }

  onGetTracks(){
    console.log(this.tracks)
  }

  ngOnInit(): void {
    this.setupDrumStuff();
  }

  private setupDrumStuff() {
    this.tracks = [];
    this.setupBasicDrumKit();
    this.addCongaDrums();
    this.addToms();

    // Needs more cow bell
    let aTrack = new DrumTrackViewModel("Cow bell", 56, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);

    aTrack = new DrumTrackViewModel("Shaker", 70, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
  }

  private addToms() {
    let aTrack = new DrumTrackViewModel("Low Tom", 45, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    aTrack = new DrumTrackViewModel("Medium Tom", 47, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    aTrack = new DrumTrackViewModel("High Tom", 50, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    return aTrack;
  }

  private addCongaDrums() {
    let aTrack = new DrumTrackViewModel("Conga 1", 60, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    aTrack = new DrumTrackViewModel("Conga 2", 61, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    aTrack = new DrumTrackViewModel("Conga 3", 62, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    aTrack = new DrumTrackViewModel("Conga 4", 63, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    aTrack = new DrumTrackViewModel("Conga 5", 64, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    return aTrack;
  }

  private setupBasicDrumKit() {
    let aTrack = new DrumTrackViewModel("Base Drum", 36, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    aTrack = new DrumTrackViewModel("Snare Drum", 38, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    aTrack = new DrumTrackViewModel("Closed High hat", 42, this.numberOfMeasures, this.beatsPerMeasure);
    this.tracks.push(aTrack);
    return aTrack;
  }

  onClearTracks(){
    this.setupDrumStuff();
    for (let track of this.tracks) {
      for (let j = 0; j < track.trackData.length; j++) {
          track.trackData[j] = 0;
      }
    }
  }

  onRandomTracks() {
    for (let track of this.tracks) {
      for (let j = 0; j < track.trackData.length; j++) {

        let k = Math.random();
        if (k < 0.25) {
          track.trackData[j] = 120;
        } else {
          track.trackData[j] = 0;
        }
      }
    }
  }

  async onPlayTracks()
  {
    let command = this.buildCommand();

    let response = await this.musicMakerService.makeDrumTrack(command).toPromise();
    console.log("response from make drum track .........")
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

  private buildCommand() {
    let command = new MakeDrumTrackCommand();
    command.beatsPerMinute = this.tempo;
    command.userId = "user1";
    command.tracks = this.getTracks();
    command.fileName = this.currentFile;
    console.log(command);
    return command;
  }

  getTracks(): DrumTrackRow[] {
    let drumTracks = [];
    for(let track of this.tracks)
    {
      let drumTrackRow = new DrumTrackRow();
      drumTrackRow.instrumentNumber = track.instrumentNumber;
      let drumString = "";
      for(let i=0; i< track.trackData.length; i++)
      {
        let currentValue = track.trackData[i];
        if(currentValue > 0)
        {
          drumString += "x"
        }else{
          drumString += "-"
        }
      }
      drumTrackRow.pattern = drumString;

      drumTracks.push(drumTrackRow);
    }

    return drumTracks;
  }
}
