import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-arp-track-cell',
  templateUrl: './arp-track-cell.component.html',
  styleUrls: ['./arp-track-cell.component.scss']
})
export class ArpTrackCellComponent implements OnInit {

  @Input()
  trackCellValue: number = 0;
  @Input()
  cellIndex: number = 0;

  constructor() { }

  ngOnInit(): void {
  }

  setCellStyle(){

    let backgroundColor = '';
    if(this.trackCellValue === 0)
    {
      if(this.cellIndex % 4 === 0)
      {
        backgroundColor = '#C09BD8';
      }
      else
      {
        backgroundColor = '#EDE3E9';
      }
    }
    else
    {
      backgroundColor = 'purple';
    }


    return {'background-color': backgroundColor}
  }

}
