import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ArpTrackViewModel } from '../arp-track-view-model';
import { NoteLengthConstants } from '../note-length-constants';
import { ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-arp-track-row',
  templateUrl: './arp-track-row.component.html',
  styleUrls: ['./arp-track-row.component.scss']
})
export class ArpTrackRowComponent implements OnInit {


  @Input()
  track: ArpTrackViewModel
  @Input()
  noteLength: string = NoteLengthConstants.SIXTEENTH
  @Output() notePlaced = new EventEmitter<ArpTrackViewModel>();

  // @ts-ignore
  @ViewChild('sltPattern') sltPattern: ElementRef;
    
  constructor() 
  {
    this.track = new ArpTrackViewModel("",0, 0, 0,0);
  }

  ngOnInit(): void {
  }

  onCellClick(track: ArpTrackViewModel, index: number): void {

    let cellValue = track.trackData[index];
    if(cellValue === 0)
    {
      this.addNoteAtIndex(track, index);      
      this.notePlaced.emit(track);
    }else{
      track.trackData[index] = 0;
    }
    console.log(track);
  }

  private addNoteAtIndex(track: ArpTrackViewModel, index: number) {
    if (this.noteLength === NoteLengthConstants.SIXTEENTH) {
      track.trackData[index] = 16;
    }
    else if (this.noteLength === NoteLengthConstants.EIGTH) {
      track.trackData[index] = 8;
    }
    else if (this.noteLength === NoteLengthConstants.QUARTER) {
      track.trackData[index] = 4;
    }
    else if (this.noteLength === NoteLengthConstants.HALF) {
      track.trackData[index] = 2;
    }
    else if (this.noteLength === NoteLengthConstants.WHOLE) {
      track.trackData[index] = 1;
    }
  }

  applyDownBeats(){
    let trackCopy = { ...this.track } as ArpTrackViewModel;
    for(let i=0; i<trackCopy.trackData.length; i++){
      if(i % 4 === 0){
        trackCopy.trackData[i] = NoteLengthConstants.QUARTER_NUMBER;
      }else{
        trackCopy.trackData[i] = 0;
      }
    }

    this.track = trackCopy;
  }

  applyEigthNotes(){
    let trackCopy = { ...this.track } as ArpTrackViewModel;
    for(let i=0; i<trackCopy.trackData.length; i++){
      if(i % 2 === 0){
        trackCopy.trackData[i] = NoteLengthConstants.EIGTH_NUMBER;
      }else{
        trackCopy.trackData[i] = 0;
      }
    }

    this.track = trackCopy;
  }

  applyOffBeats(){
    let trackCopy = { ...this.track } as ArpTrackViewModel;
    for(let i=0; i<trackCopy.trackData.length; i++){
      if(i % 8 === 4){
        trackCopy.trackData[i] = NoteLengthConstants.QUARTER_NUMBER;
      }else{
        trackCopy.trackData[i] = 0;
      }
    }

    this.track = trackCopy;
  }

  applyRandomPattern(){
    let trackCopy = { ...this.track } as ArpTrackViewModel;
    for(let i=0; i<trackCopy.trackData.length; i++){
      let k = Math.random();
      if(k < 0.2){
        trackCopy.trackData[i] = NoteLengthConstants.SIXTEENTH_NUMBER;
      }else{
        trackCopy.trackData[i] = 0;
      }
    }

    this.track = trackCopy;

  }

  clearRow(){
    let trackCopy = { ...this.track } as ArpTrackViewModel;
    for(let i=0; i<trackCopy.trackData.length; i++){
      trackCopy.trackData[i] = 0;
    }

    this.track = trackCopy;
  }

  onPatternChange(eventArgs: any)
  {
    const selectedValue = eventArgs.target.value;
    if (selectedValue.length === 0)
      return;

    switch(selectedValue)
    {
      case "random": 
        this.applyRandomPattern();
        break;
      case "down-beats": 
        this.applyDownBeats();
        break;
      case "off-beats":
        this.applyOffBeats();
        break;
      case "eigth-notes":
        this.applyEigthNotes()
        break;
      case "clear":
          this.clearRow();
          break;
    }

    this.sltPattern.nativeElement.selectedIndex = 0;
  }

}
