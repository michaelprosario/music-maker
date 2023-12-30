import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ArpCell } from '../edit-arpeggio2/arp-cell';
import { NoteLengthConstants } from '../view/edit-arpeggio/note-length-constants';

@Component({
  selector: 'app-arp2-track-cell',
  standalone: false,  
  templateUrl: './arp2-track-cell.component.html',
  styleUrl: './arp2-track-cell.component.scss'
})
export class Arp2TrackCellComponent implements OnChanges {
  @Input() cell: ArpCell;
  @Input() cellValue: number;
  @Input() noteLength: number = 16;
  displayChar: string = "s";

  cellClass = 'spnTrackCell spnCellNotSelected';

  setDisplayChar()
  {
    switch(this.cellValue)
    {
      case 16: 
        this.displayChar = 's';
        break;
      case 8: 
        this.displayChar = 'e';
        break;
      case 4: 
        this.displayChar = 'q';
        break;
      case 2: 
        this.displayChar = 'h';
        break;
      case 1: 
        this.displayChar = 'w';
        break;

    } 
  }

  constructor()
  {
    this.cell = new ArpCell();
    this.cellValue = 0;
  }
  
  ngOnChanges(changes: SimpleChanges): void {
    if(this.cellValue === 0)
    {      
      this.cellClass = 'spnTrackCell spnCellNotSelected';
    }else{
      this.cellClass = 'spnTrackCell spnCellSelected';
    }

    this.setDisplayChar();
  }

  onCellToggle()
  {
    if(this.cellValue === 0)
    {
      this.cell.value = this.noteLength;
      this.cellValue = this.noteLength;
    }else{
      this.cell.value = 0;
      this.cellValue = 0;
    }
  }
}
