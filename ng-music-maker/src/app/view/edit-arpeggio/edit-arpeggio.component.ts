import { ArpeggioPattern, ArpeggioPatternRow, ChordChange, MakeMidiFromArpeggioCommand } from 'src/app/core/services/server-client';
import { ArpTrackViewModel } from './arp-track-view-model';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MusicMakerService } from 'src/app/core/services/music-maker-service';
import { v4 as uuidv4 } from 'uuid';
import { ArpMakerService } from './arp-maker-service';
import { DomSanitizer } from '@angular/platform-browser';
import { EditArpeggioData } from './edt-arpeggio-data';

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
  instrument: number = 13;
  chordProgressionString: string = 'Am G F E';
  playButtonEnabled: boolean = true;
  displayModalSaveArpFile: boolean = false;
  exportFileName: string = '';

  constructor(
     private musicMakerService: MusicMakerService, 
     private arpMakerService: ArpMakerService,
     private sanitizer: DomSanitizer
     ) 
  {
    this.tracks = [];
    this.setCurrentFile();
    this.midiUrl = `${environment.midiBlobStorage}/${this.currentId}.mid`;
  }

  private setCurrentFile() {
    this.currentId = uuidv4();
  }  

  onDownload(){
    window.open(this.midiUrl);
  }

  ngOnInit(): void 
  {
    this.tracks = this.arpMakerService.setupTrackRows(this.numberOfMeasures, this.beatsPerMeasure);
  }

  onClearTracks(){
    this.tracks = this.arpMakerService.setupTrackRows(this.numberOfMeasures, this.beatsPerMeasure);
  }

  onSavePattern()
  {
    this.exportFileName = this.currentId + ".json";
    this.displayModalSaveArpFile = true;
  }

  onSaveArpFile()
  {
    this.displayModalSaveArpFile = false;    
    this.savePatternToFile();
  }

  savePatternToFile()
  {
    const data = new EditArpeggioData(this.tempo, this.beatsPerMeasure, this.numberOfMeasures, this.tracks, this.currentId, this.instrument)
    const json = JSON.stringify(data);
    var element = document.createElement('a');
    element.setAttribute('href', "data:text/json;charset=UTF-8," + encodeURIComponent(json));
    element.setAttribute('download', this.exportFileName);
    element.style.display = 'none';
    document.body.appendChild(element);
    element.click(); // simulate click
    document.body.removeChild(element);
  }

  public changeListener(event: Event)
  {
    // @ts-ignore
    const files = event.target.files;

    if(files && files.length > 0) 
    {
       let file : File | null = files.item(0); 
        if(!file)
        {
            return;
        }
        console.log(file.name);
        console.log(file.size);
        console.log(file.type);
        let reader: FileReader = new FileReader();
        reader.readAsText(file);
        reader.onload = (e) => {
          let json: string = reader.result as string;
          this.loadArpFromJsonString(json);          
        }
      }
  }

  private loadArpFromJsonString(json: string) {
    let data = JSON.parse(json) as EditArpeggioData;

    this.numberOfMeasures = data.numberOfMeasures;
    this.beatsPerMeasure = data.beatsPerMeasure;
    this.tracks = this.arpMakerService.setupTrackRows(this.numberOfMeasures, this.beatsPerMeasure);

    this.tracks = data.tracks;
    this.tempo = data.tempo;
    this.instrument = data.instrument;
  }

  async onPlayTracks()
  {
    //this.playButtonEnabled = false;
    let command = this.buildCommand();

    let response = await this.musicMakerService.makeMidiFromArpeggio(command).toPromise();

    this.playCurrentFile();
  }

  private playCurrentFile() {
    let midiPlayer = document.getElementById("midiPlayer");
    // @ts-ignore
    midiPlayer.reload();
    // @ts-ignore
    setTimeout(() => { midiPlayer.start(); this.playButtonEnabled = true; }, 5000);
  }

  private buildCommand() : MakeMidiFromArpeggioCommand {
    let command = new MakeMidiFromArpeggioCommand();
    command.beatsPerMinute = this.tempo;
    command.userId = "user1";
    command.channel = 1;
    
    command.pattern = new ArpeggioPattern();
    command.pattern.rows = this.arpMakerService.getArpPatternRows(this.tracks);
    command.instrument = this.instrument;

    command.id = this.currentId;    
    command.chordChangesAsString = this.chordProgressionString.trim();    

    console.log(command);
    return command;
  }

  onProgressionChange(event: any) {
    const selectedValue = event.target.value;
    if(selectedValue.length === 0)
      return;
      
    if(selectedValue === "major-1"){
      this.chordProgressionString = "C G Am F";
    }
    else if(selectedValue === "major-2")
    {
      this.chordProgressionString = "C F G F";
    }
    else if(selectedValue === "major-3")
    {
      this.chordProgressionString = "C Am F G";
    }
    else if(selectedValue === "major-4")
    {
      this.chordProgressionString = "C G Am Em F C F G";
    }
    else if(selectedValue === "major-5")
    {
      this.chordProgressionString = "G Am F C";
    }
    else if(selectedValue === "major-6")
    {
      this.chordProgressionString = "F C G Am";
    }
    else if(selectedValue === "minor-1")
    {
      this.chordProgressionString = "Dm G C C";
    }    
    else if(selectedValue === "minor-2")
    {
      this.chordProgressionString = "Am F C G";  
    }
    else if(selectedValue  === "minor-3")
    {
      this.chordProgressionString = "Am G F E";
    }
    else if(selectedValue  === "minor-4")
    {
      this.chordProgressionString = "Dm A Dm C F C Dm A Dm A Dm C F C Dm:2 A:2 Dm";
    }
  }  
}
