import { Component, Input } from '@angular/core';
import { ArpRow } from '../arp-row';

@Component({
  selector: 'app-arp2-track-row',
  standalone: false,  
  templateUrl: './arp2-track-row.component.html',
  styleUrl: './arp2-track-row.component.scss'
})
export class Arp2TrackRowComponent {
  @Input() row: ArpRow;
  @Input() totalTicks: number = 16;
  @Input() noteLength: number = 16;

  constructor()
  {
    this.row = new ArpRow();
  }

  onPattern1(){
    this.row.pattern1(this.totalTicks);
    this.row = this.row;
  }

  onPattern2(){
    this.row.pattern2(this.totalTicks);
    this.row = this.row;
  }  

  onClear(){
    this.row.clearRow(this.totalTicks);
    this.row = this.row;
  }  

  onRandom(){
    this.row.randomPattern(this.totalTicks);
    this.row = this.row;
  }    
}

