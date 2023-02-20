import { Injectable } from "@angular/core";
import { ArpeggioPatternRowType } from "src/app/core/services/server-client";
import { ArpTrackViewModel } from "./arp-track-view-model";

@Injectable({
    providedIn: 'root'
})
export class ArpMakerService {
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


    constructor() {

    }



}