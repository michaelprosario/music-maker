import { ArpeggioPattern, ArpeggioPatternRow, ChordChange, MakeMidiFromArpeggioCommand } from 'src/app/core/services/server-client';
import { ArpMakerService } from './arp-maker-service';
import { ArpTrackViewModel } from './arp-track-view-model';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { EditArpeggioData } from './edt-arpeggio-data';
import { environment } from 'src/environments/environment';
import { MusicMakerService } from 'src/app/core/services/music-maker-service';
import { v4 as uuidv4 } from 'uuid';
import { IInstrumentItem } from '../../core/services/instrument-item';
import { InstrumentsService } from '../../core/services/instruments-service';
import { NoteLengthConstants } from './note-length-constants';
import * as Tone from 'tone'
import { Note, Scale } from "tonal";
import { ChordsService, ICommonChordProgression } from './chords-service';

@Component({
  selector: 'app-edit-arpeggio',
  templateUrl: './edit-arpeggio.component.html',
  styleUrls: ['./edit-arpeggio.component.scss']
})
export class EditArpeggioComponent implements OnInit {
  
  @ViewChild("txtFile", {static: false})
  txtFile: ElementRef | undefined;

  beatsPerMeasure: number = 4;
  chordProgressionString: string = 'Am G F E';
  commonChords: ICommonChordProgression[];
  currentId: string = '';
  displayModalLoadArpFile: boolean = false;
  displayModalSaveArpFile: boolean = false;
  displayModalSelectInstrument: boolean = false;
  displayModalPianoRoll: boolean = false;
  exportFileName: string = '';
  instrument: number = 13;
  instruments: IInstrumentItem[];  
  midiUrl: string = '';
  noteLength: string = NoteLengthConstants.SIXTEENTH;
  numberOfMeasures: number = 1;
  playButtonEnabled: boolean = true;
  selectedInstrument: IInstrumentItem = { name: 'Foo', id: 1};
  tempo: number = 90;
  tracks: ArpTrackViewModel[];
  synth:any 

  constructor(
    private arpMakerService: ArpMakerService,
    private chordsService: ChordsService,
    private instrumentService: InstrumentsService,
    private musicMakerService: MusicMakerService,
    private sanitizer: DomSanitizer,
  ) 
  {
    this.tracks = [];
    this.setCurrentFile();
    this.setMidiFile();
    this.instruments = this.instrumentService.getInstruments();
    this.commonChords = this.chordsService.getCommonChords();
  }

  onNotePlaced(eventArgs: ArpTrackViewModel)
  {
    console.log("play note ....");
    console.log(eventArgs);
    // this.playTrackNote(eventArgs);
  }

  /* todo - fix me 
  private playTrackNote(eventArgs: ArpTrackViewModel) {
    const map1 = new Map();

    map1.set(1, "2M");
    map1.set(2, "3M");
    map1.set(3, "5P");

    const octave = eventArgs.octave;
    const rowType = eventArgs.rowType;
    let noteToPlay = "C" + (3 + octave);

    // todo - fix me!
    //if (rowType > 0)
    //noteToPlay = Note.transpose(noteToPlay, map1.get(rowType));

    this.synth.triggerAttackRelease(noteToPlay, "8n");
  }
  */

  private setMidiFile() {
    var k = "?k=" + Math.random();
    this.midiUrl = `${environment.midiBlobStorage}/${this.currentId}.mid${k}`;
    this.synth = new Tone.Synth().toDestination();
  }

  onMusicView(){
    this.displayModalPianoRoll = true;
  }

  onNoteLengthChange(event: any) {
    const selectedValue = event.target.value;
    this.noteLength = selectedValue;
  }

  private setCurrentFile() {
    this.currentId = uuidv4();
  }

  onDownload() {
    window.open(this.midiUrl);
  }

  ngOnInit(): void {
    this.setupTrackRows();
  }

  private setupTrackRows() {
    this.tracks = this.arpMakerService.setupTrackRows(this.numberOfMeasures, this.beatsPerMeasure);
  }

  onClearTracks() {
    if(confirm("Press OK to clear the current work space"))
      this.setupTrackRows();
  }

  onInstrumentSelect()
  {
    this.displayModalSelectInstrument = true;
  }

  onInstrumentSelected()
  {
    this.displayModalSelectInstrument = false;
    this.instrument = this.selectedInstrument.id;
  }

  onLoadPattern() {
    this.displayModalLoadArpFile = true;
    if(this.txtFile)
      this.txtFile.nativeElement.value = "";
  }

  onSavePattern() {
    this.exportFileName = this.currentId + ".json";
    this.displayModalSaveArpFile = true;
  }

  onSaveArpFile() {
    this.displayModalSaveArpFile = false;
    this.savePatternToFile();
  }

  savePatternToFile() {
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

  fileChangeListener(event: Event) {
    // @ts-ignore
    const files = event.target.files;

    if (files && files.length > 0) {
      let file: File | null = files.item(0);
      if (!file) {
        return;
      }

      let reader: FileReader = new FileReader();
      reader.readAsText(file);
      reader.onload = (e) => {
        let json: string = reader.result as string;
        this.loadArpFromJsonString(json);
        this.displayModalLoadArpFile = false;
      }
    }
  }

  private loadArpFromJsonString(json: string) {
    let data = JSON.parse(json) as EditArpeggioData;

    this.numberOfMeasures = data.numberOfMeasures;
    this.beatsPerMeasure = data.beatsPerMeasure;
    this.setupTrackRows();

    this.tracks = data.tracks;
    this.tempo = data.tempo;
    this.instrument = data.instrument;
  }

  async onPlayTracks() {
    let command = this.buildCommand();

    let response = await this.musicMakerService.makeMidiFromArpeggio(command).toPromise();
    if(response?.code == 200)
      this.playCurrentFile();
  }

  private playCurrentFile() {
    let midiPlayer = document.getElementById("midiPlayer");
    let myVisualizer = document.getElementById("myVisualizer");

    this.setMidiFile();
    // @ts-ignore
    midiPlayer.reload();
    // @ts-ignore
    
    // @ts-ignore
    setTimeout(() => { 
      // @ts-ignore
      midiPlayer.start();
      // @ts-ignore
      midiPlayer.loop = true; 
      // @ts-ignore
      myVisualizer.reload();
    }, 5000);
  }

  private buildCommand(): MakeMidiFromArpeggioCommand {
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

  onBeatsPerMeasureChange(event: any)
  {
    const selectedValue = event.target.value;
    const map1 = new Map();
    map1.set('3', 3);
    map1.set('4', 4);
    map1.set('5', 5);
    map1.set('6', 6);
    this.beatsPerMeasure = map1.get(selectedValue);
  }

  onProgressionChange(event: any) 
  {
    const selectedValue = event.target.value;
    this.chordProgressionString = selectedValue;
  }

  makeRandomSong()
  {
    this.chordProgressionString = this.chordsService.getRandomProgression();
  }
}
