import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { EditArpeggioComponent } from './view/edit-arpeggio/edit-arpeggio.component';
import { EditDrumTrackComponent } from './view/edit-drum-track/edit-drum-track.component';
import { EditProgressionPlayerComponent } from './view/edit-progression-player/edit-progression-player.component';

const routes: Routes = [
  { path: '', component: EditDrumTrackComponent },
  { path: 'edit-progression', component: EditProgressionPlayerComponent },
  { path: 'drum-editor', component: EditDrumTrackComponent },
  { path: 'arp-editor', component: EditArpeggioComponent },
  { path: 'about', component: AboutComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
