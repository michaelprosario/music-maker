import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { ChordMakerComponent } from './view/chord-maker/chord-maker.component';
import { EditArpeggioComponent } from './view/edit-arpeggio/edit-arpeggio.component';
import { EditDrumTrackComponent } from './view/edit-drum-track/edit-drum-track.component';
import { EditProgressionPlayerComponent } from './view/edit-progression-player/edit-progression-player.component';
import { EditArpeggio2Component } from './edit-arpeggio2/edit-arpeggio2.component';

const routes: Routes = [
  { path: '', component: EditDrumTrackComponent },
  { path: 'edit-progression', component: EditProgressionPlayerComponent },
  { path: 'drum-editor', component: EditDrumTrackComponent },
  { path: 'arp-editor', component: EditArpeggioComponent },
  { path: 'arp-editor2', component: EditArpeggio2Component },
  { path: 'about', component: AboutComponent },
  { path: 'chord-maker', component: ChordMakerComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
