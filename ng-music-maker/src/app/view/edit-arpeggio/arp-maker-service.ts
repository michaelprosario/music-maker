import { Injectable } from "@angular/core";
import { ArpeggioPatternRow, ArpeggioPatternRowType } from "src/app/core/services/server-client";
import { ArpTrackViewModel } from "./arp-track-view-model";
import { NoteLengthConstants } from "./note-length-constants";

@Injectable({
    providedIn: 'root'
})
export class ArpMakerService {
    constructor() {

    }


    numberOfMeasures: number = 0;
    beatsPerMeasure: number = 0;
    setupTrackRows(numberOfMeasures: number, beatsPerMeasure: number) {
        this.numberOfMeasures = numberOfMeasures;
        this.beatsPerMeasure = beatsPerMeasure;
        let tracks = Array<ArpTrackViewModel>();
        this.setupOctave(2, tracks);
        this.setupOctave(1, tracks);
        this.setupOctave(0, tracks);

        for (let track of tracks) {
            for (let j = 0; j < track.trackData.length; j++) {
                track.trackData[j] = 0;
            }
          }
      
        return tracks;
    }

    private setupOctave(octave: number, tracks: ArpTrackViewModel[]) {
        let aTrack: ArpTrackViewModel;

        aTrack = new ArpTrackViewModel("V", ArpeggioPatternRowType.Fifth, octave, this.numberOfMeasures, this.beatsPerMeasure);
        tracks.push(aTrack);
        aTrack = new ArpTrackViewModel("III", ArpeggioPatternRowType.Third, octave, this.numberOfMeasures, this.beatsPerMeasure);
        tracks.push(aTrack);
        aTrack = new ArpTrackViewModel("II", ArpeggioPatternRowType.Second, octave, this.numberOfMeasures, this.beatsPerMeasure);
        tracks.push(aTrack);
        aTrack = new ArpTrackViewModel("Root", ArpeggioPatternRowType.Root, octave, this.numberOfMeasures, this.beatsPerMeasure);
        tracks.push(aTrack);
        return aTrack;
    }

    getArpPatternRows(tracks: ArpTrackViewModel[]): ArpeggioPatternRow[] {
        let arpTracks = [];
        for(let track of tracks)
        {
          let arpTrackRow = new ArpeggioPatternRow();
          arpTrackRow.type = track.rowType;
          arpTrackRow.octave = track.octave;
          let arpPatternString = "";
          arpPatternString = this.placeNoteStartSymbols(track, arpPatternString);

          // handle eigth notes ...
          arpPatternString = arpPatternString.replace(/e./g, "e~");

          // handle quarter notes ...
          arpPatternString = arpPatternString.replace(/q.../g, "q~~~");

          // handle half notes ...
          arpPatternString = arpPatternString.replace(/h......./g, "h~~~~~~~");

          // handle whole notes ....
          arpPatternString = arpPatternString.replace(/w.............../g, "h~~~~~~~~~~~~~~~");
          arpTrackRow.pattern = arpPatternString;
    
          arpTracks.push(arpTrackRow);
        }
    
        return arpTracks;
      }

  private placeNoteStartSymbols(track: ArpTrackViewModel, arpPatternString: string) {
    for (let i = 0; i < track.trackData.length; i++) {
      let currentValue = track.trackData[i];

      switch (currentValue) {
        case NoteLengthConstants.SIXTEENTH_NUMBER:
          arpPatternString += "s";
          break;
        case NoteLengthConstants.EIGTH_NUMBER:
          arpPatternString += "e";
          break;
        case NoteLengthConstants.QUARTER_NUMBER:
          arpPatternString += "q";
          break;
        case NoteLengthConstants.HALF_NUMBER:
          arpPatternString += "h";
          break;
        case NoteLengthConstants.WHOLE_NUMBER:
          arpPatternString += "w";
          break;
        case 0:
          arpPatternString += "-";
      }

    }
    return arpPatternString;
  }
}