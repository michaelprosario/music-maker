import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ArpCell } from '../arp-cell';
import { NoteLengthConstants } from '../../edit-arpeggio/note-length-constants';

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
      case NoteLengthConstants.SIXTEENTH_NUMBER: 
        this.displayChar = 's';
        break;
      case NoteLengthConstants.EIGTH_NUMBER: 
        this.displayChar = 'e';
        break;
      case NoteLengthConstants.QUARTER_NUMBER: 
        this.displayChar = 'q';
        break;
      case NoteLengthConstants.HALF_NUMBER: 
        this.displayChar = 'h';
        break;
      case NoteLengthConstants.WHOLE_NUMBER: 
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
      if(this.cell.tick % 4 === 0)
      {
        this.cellClass = 'spnTrackCell spnCellNotSelected1';
      }else{
        this.cellClass = 'spnTrackCell spnCellNotSelected2';
      }      
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
