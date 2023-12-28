import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ng-music-maker';

  constructor(private router: Router){

  }

  openDrumEditor(){
    this.router.navigate(["/drum-editor"])
  }

  openProgressionEditor(){
    this.router.navigate(["/edit-progression"])
  }

  openArpeggioEditor(){
    this.router.navigate(["/arp-editor"])
  }

  openArpeggioEditor2(){
    this.router.navigate(["/arp-editor2"])
  }  

  openChordMaker(){
    this.router.navigate(["/chord-maker"])
  }

  openAbout(){
    this.router.navigate(["/about"])
  }  
}
