import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ArpCell } from '../edit-arpeggio2/arp-cell';
import { NoteLength } from '../edit-arpeggio2/note-length';

@Component({
  selector: 'app-arp2-track-cell',
  standalone: false,  
  templateUrl: './arp2-track-cell.component.html',
  styleUrl: './arp2-track-cell.component.scss'
})
export class Arp2TrackCellComponent implements OnChanges {
  @Input() cell: ArpCell;
  @Input() cellValue: number;
  cellClass = 'spnTrackCell spnCellNotSelected';

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
  }

  onCellToggle()
  {
    if(this.cellValue === 0)
    {
      this.cell.value = NoteLength.SIXTEENTH
      this.cellValue = NoteLength.SIXTEENTH;
      this.cellClass = 'spnTrackCell spnCellSelected';
    }else{
      this.cell.value = 0;
      this.cellValue = 0;
      this.cellClass = 'spnTrackCell spnCellNotSelected';
    }
  }
}
