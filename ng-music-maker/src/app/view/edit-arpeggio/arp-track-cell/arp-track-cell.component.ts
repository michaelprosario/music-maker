import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-arp-track-cell',
  templateUrl: './arp-track-cell.component.html',
  styleUrls: ['./arp-track-cell.component.scss']
})
export class ArpTrackCellComponent implements OnInit {

  @Input()
  trackCellValue: number = 0;
  constructor() { }

  ngOnInit(): void {
  }

}
