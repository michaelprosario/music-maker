import { CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EditDrumTrackComponent } from './view/edit-drum-track/edit-drum-track.component';
import { FormsModule } from '@angular/forms';
import {PanelModule} from 'primeng/panel';
import { DrumTrackRowComponent } from './view/edit-drum-track/drum-track-row/drum-track-row.component';
import { DrumTrackCellComponent } from './view/edit-drum-track/drum-track-cell/drum-track-cell.component';
import { HttpClientModule } from '@angular/common/http';
import { EditProgressionPlayerComponent } from './view/edit-progression-player/edit-progression-player.component';
import { EditArpeggioComponent } from './view/edit-arpeggio/edit-arpeggio.component';
import { ArpTrackCellComponent } from './view/edit-arpeggio/arp-track-cell/arp-track-cell.component';
import { ArpTrackRowComponent } from './view/edit-arpeggio/arp-track-row/arp-track-row.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {DialogModule} from 'primeng/dialog';
import {ButtonModule} from 'primeng/button';


@NgModule({
  schemas: [ CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA ],
  declarations: [
    AppComponent,
    EditDrumTrackComponent,
    DrumTrackRowComponent,
    DrumTrackCellComponent,
    EditProgressionPlayerComponent,
    EditArpeggioComponent,
    ArpTrackCellComponent,
    ArpTrackRowComponent
  ],
  imports: [
    AppRoutingModule,
    ButtonModule,
    BrowserModule,
    BrowserAnimationsModule,
    DialogModule,
    FormsModule,
    PanelModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
