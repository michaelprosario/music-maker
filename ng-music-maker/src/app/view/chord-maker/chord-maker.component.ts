import { Component, OnInit } from '@angular/core';
import { ChordsService } from '../edit-arpeggio/chords-service';
import { Chord } from 'tonal';

@Component({
  selector: 'app-chord-maker',
  templateUrl: './chord-maker.component.html',
  styleUrls: ['./chord-maker.component.scss']
})
export class ChordMakerComponent implements OnInit {

  group1Chords: string = '';
  group2Chords: string = '';
  group3Chords: string = '';

  constructor(private chordServices: ChordsService) { }

  ngOnInit(): void 
  {
    this.makeChords();
  }

  /*
  transposeChords(progression: string)
  {
    const chords = progression.split(' ');
    let response = '';
    for(let chord of chords)
    {
      if(chord.indexOf(":") === -1)
        response += Chord.transpose(chord, "5P") + " ";
      else
        response += Chord.transpose(chord, "5P") + " ";
    }

    return response;
  }
  */

  makeChords()
  {
    this.group1Chords = this.chordServices.getMajorProgressions().value;
    this.group2Chords = this.chordServices.getMinorProgressions().value;
    this.group3Chords = this.chordServices.getMinor2Progressions().value;

    /*
    this.group1Chords = this.transposeChords(this.group1Chords);
    this.group2Chords = this.transposeChords(this.group2Chords);
    this.group3Chords = this.transposeChords(this.group3Chords);
    */
  }

}
