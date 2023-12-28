import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ArpMakerService } from '../view/edit-arpeggio/arp-maker-service';
import { ArpModel } from './arp-model';
import { ChordSequence } from './chord-sequence';

export let cellSize = 50;
export let MAX_VELOCITY = 127;



export let ROOT_OF_TRIAD = 0;
export let SECOND_OF_TRIAD = 2;
export let THIRD_OF_TRIAD = 3;
export let FIFTH_OF_TRIAD = 5;

export let PIANO = 0;


@Component({
  selector: 'app-edit-arpeggio2',
  standalone: false,  
  templateUrl: './edit-arpeggio2.component.html',
  styleUrl: './edit-arpeggio2.component.scss'
})
export class EditArpeggio2Component implements OnInit
{
	timerInterval: number = 0;
	isPlaying: boolean = false;
	currentTick: number = 0;
	currentChordTick: number = 0;
	playEnabled: boolean = true;
	numberOfMeasures: number;
	beatsPerMeasure: number;
	arpGridModel: ArpModel;
	instrument = 12;
	port: any;	
	tempo = 80;
	chordSchedule: any;
	chordProgression = "Em C D G";

	constructor(private arpMakerService: ArpMakerService)
	{
		this.numberOfMeasures = 2;
		this.beatsPerMeasure = 4;
		this.arpGridModel = new ArpModel();
	}

	start() {
		this.enablePlayButton();
	}

	setupChordProgression()
	{
		let chordSequence = new ChordSequence();		
		chordSequence.parse(this.chordProgression);	
		this.chordSchedule = chordSequence.getChordTickSchedule();		
	}	

	enablePlayButton() {
		this.playEnabled = true;
	}	
  
	ngOnInit(): void {
		this.arpGridModel = new ArpModel();
		this.arpGridModel.setup();		
	}

	onPlay() {
		this.setupChordProgression();
		
		// @ts-ignore
		var synth = JZZ.synth.Tiny();
		var s1 = synth.getSynth(this.instrument);
		synth.setSynth(0, s1);
		this.port = synth;	

		this.currentTick = 0;
		this.currentChordTick = 0;
		this.isPlaying = true;		
		this.onTick();
	}	

	onTick() {
		
		try {
			var bpm = this.tempo
			if (bpm < 50) {
				bpm = 100;
			}
			this.arpGridModel.bpm = bpm;
			this.timerInterval = this.arpGridModel.getMSFromBPM(bpm);
		}
		catch (e) {
			this.arpGridModel.bpm = 100;
			this.timerInterval = this.arpGridModel.getMSFromBPM(this.arpGridModel.bpm);
		}

		if (this.isPlaying) {
			this.onHandleTimeTick();
			setTimeout(() => this.onTick(), this.timerInterval)
		}
	}	

	@ViewChild('divTickVizRow', {read: ElementRef}) divTickVizRow: ElementRef<HTMLElement> | undefined;
	visualizeTickRow() {
		if(!this.divTickVizRow)
			return;

		for (let tick = 0; tick < this.arpGridModel.getTotalTicks(); tick++) {
			if (tick === this.currentTick) {
				this.divTickVizRow.nativeElement.children[1].children[tick].className = "spnTickActive";
			} else {
				this.divTickVizRow.nativeElement.children[1].children[tick].className = "spnTick";
			}
		}
	}	

	onHandleTimeTick() {
		// handle tick work ...
		this.visualizeTickRow();

		for (let instrument = 0; instrument < this.arpGridModel.instrumentCount; instrument++) {
			let cellValue = this.arpGridModel.getInstrumentRowValue(instrument, this.currentTick);
			if (cellValue > 0) 
			{
				this.playInstrument(instrument);
			}
		}

		// move to next tick
		
		this.currentTick = this.currentTick + 1;
		let totalTicks = this.arpGridModel.getTotalTicks();
		if (this.currentTick >= totalTicks) {
			this.currentTick = 0;
		}

		this.currentChordTick++;
		if(this.currentChordTick >= this.chordSchedule.length)
		{
			this.currentChordTick = 0;
		}
	
	}	

	playInstrument(arpRowIndex: number) {
		//console.log(arpRowIndex);
		let arpRow = this.arpGridModel.rows[arpRowIndex] 
		let velocity = 127;
		let channel = 1;

		// check root 1 - play it if needed
		let octaveLevel = arpRow.level+3;
		//alert(this.chordSchedule);
		//alert(this.currentChordTick);

		let currentChord = this.chordSchedule[this.currentChordTick];
		// @ts-ignore
		const triad = Tonal.Chord.degrees(currentChord);
		let triad2 = [1, 2, 3].map(triad);


		let rootTone:any = triad2[0];
		let thirdTone:any = triad2[1];
		let fifthTone:any = triad2[2];

		if(arpRow.triadNote === ROOT_OF_TRIAD)
		{
			this.port.noteOn(channel, rootTone + octaveLevel, 127).wait(500).noteOff(channel, rootTone + octaveLevel);
		}

		// check 2nd - play it if needed
		if(arpRow.triadNote === SECOND_OF_TRIAD)
		{			
			let rootTone2 = rootTone + octaveLevel;
			// @ts-ignore
			let rootToneNumber = Tonal.Note.midi(rootTone2);
			let secondTone = rootToneNumber! - 10;

			this.port.noteOn(channel, secondTone, 127).wait(500).noteOff(channel, secondTone);
		}

		// check third - play it if needed
		if(arpRow.triadNote === THIRD_OF_TRIAD)
		{
			this.port.noteOn(channel, thirdTone + octaveLevel, 127).wait(500).noteOff(channel, thirdTone + octaveLevel);
		}

		// check fifth - play it if needed
		if(arpRow.triadNote === FIFTH_OF_TRIAD)
		{
			this.port.noteOn(channel, fifthTone + octaveLevel, 127).wait(500).noteOff(channel, fifthTone + octaveLevel);
		}
	}	
}
