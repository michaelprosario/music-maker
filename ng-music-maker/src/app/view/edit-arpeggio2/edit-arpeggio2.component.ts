

import { ArpeggioPattern, MakeMidiFromArpeggioCommand } from '../../core/services/server-client';
import { ArpMakerService } from '../edit-arpeggio/arp-maker-service';
import { ArpModel } from './arp-model';
import { ChordSequence } from './chord-sequence';
import { ChordsService, ICommonChordProgression } from '../edit-arpeggio/chords-service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { environment } from 'src/environments/environment';
import { MusicMakerService } from '../../core/services/music-maker-service';
import { NoteInChord } from './note-in-chord';
import { NoteLengthConstants } from '../edit-arpeggio/note-length-constants';
import { v4 as uuidv4 } from 'uuid';

// Can we put this in a Arp composer constants class?
export let cellSize = 30;
export let MAX_VELOCITY = 127;

export let PIANO = 0;

@Component({
	selector: 'app-edit-arpeggio2',
	standalone: false,
	templateUrl: './edit-arpeggio2.component.html',
	styleUrl: './edit-arpeggio2.component.scss'
})
export class EditArpeggio2Component implements OnInit {
	timerInterval: number = 0;
	isPlaying: boolean = false;
	currentTick: number = 0;
	currentChordTick: number = 0;
	playEnabled: boolean = true;
	numberOfMeasures: number;
	beatsPerMeasure: number;
	arpGridModel!: ArpModel;
	instrument = 12;
	port: any;
	tempo = 80;
	chordSchedule: any;
	chordProgression = "Em C D G";
	currentId = '';
	commonChords: ICommonChordProgression[];
	noteLength: number = NoteLengthConstants.SIXTEENTH_NUMBER;

	onNoteLengthChange(event: any) {
		const selectedValue = event.target.value;
		this.noteLength = parseInt(selectedValue);
	}
	
	constructor(
		private arpMakerService: ArpMakerService,
		private musicMakerService: MusicMakerService,
		private chordsService: ChordsService
	) {
		this.numberOfMeasures = 2;
		this.beatsPerMeasure = 4;
		this.commonChords = this.chordsService.getCommonChords();
	}

	start() {
		this.enablePlayButton();
	}

	setupChordProgression() {
		let chordSequence = new ChordSequence();
		chordSequence.parse(this.chordProgression);
		this.chordSchedule = chordSequence.getChordTickSchedule();
	}

	enablePlayButton() {
		this.playEnabled = true;
	}

	ngOnInit(): void {
		this.setupGridModel(1);
	}

	private setupGridModel(measureCount: number) 
	{
		this.currentId = uuidv4();
		this.arpGridModel = new ArpModel();
		this.arpGridModel.measureCount = measureCount;
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

	private buildCommand(): MakeMidiFromArpeggioCommand {
		let command = new MakeMidiFromArpeggioCommand();
		command.beatsPerMinute = this.tempo;
		command.userId = "user1";
		command.channel = 1;

		command.pattern = new ArpeggioPattern();
		command.pattern.rows = this.arpMakerService.getArpPatternRows2(this.arpGridModel.rows);
		command.instrument = this.instrument;

		command.id = this.currentId;
		command.chordChangesAsString = this.chordProgression.trim();

		console.log(command);
		return command;
	}

	async onDownload() {
		let command = this.buildCommand();

		let response = await this.musicMakerService.makeMidiFromArpeggio(command).toPromise();
		if (response?.code == 200) {
			this.openMidiFile();
		} else {
			alert('Failed to make midi file.  See console.')
			console.log(response);
		}
	}

	openMidiFile() {
		var k = "?k=" + Math.random();
		let midiUrl = `${environment.midiBlobStorage}/${this.currentId}.mid${k}`;
		window.open(midiUrl);
	}

	onStop() {
		this.isPlaying = false;
	}

	onClear() {
		if (confirm("Press OK to clear the grid")) {
			this.arpGridModel.clearRows();
		}
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

	@ViewChild('divTickVizRow', { read: ElementRef }) divTickVizRow: ElementRef<HTMLElement> | undefined;
	visualizeTickRow() {
		if (!this.divTickVizRow)
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
			if (cellValue > 0) {
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
		if (this.currentChordTick >= this.chordSchedule.length) {
			this.currentChordTick = 0;
		}

	}

	makeRandomSong() {
		this.chordProgression = this.chordsService.getRandomProgression();
	}

	onProgressionChange(event: any) {
		const selectedValue = event.target.value;
		this.chordProgression = selectedValue;
	}

	setMeasureCount(event: any)
	{
		if(confirm("Press OK to resize grid"))
		{
			let x = parseInt(event.target.value);
			this.setupGridModel(x);	
		}
	}

	playInstrument(arpRowIndex: number) {
		//console.log(arpRowIndex);
		let arpRow = this.arpGridModel.rows[arpRowIndex]
		let velocity = 127;
		let channel = 1;

		// check root 1 - play it if needed
		let octaveLevel = arpRow.level + 3;
		//alert(this.chordSchedule);
		//alert(this.currentChordTick);

		let currentChord = this.chordSchedule[this.currentChordTick];
		// @ts-ignore
		const triad = Tonal.Chord.degrees(currentChord);
		let triad2 = [1, 2, 3].map(triad);


		let rootTone: any = triad2[0];
		let thirdTone: any = triad2[1];
		let fifthTone: any = triad2[2];

		if (arpRow.triadNote === NoteInChord.ROOT_OF_TRIAD) {
			this.port.noteOn(channel, rootTone + octaveLevel, 127).wait(500).noteOff(channel, rootTone + octaveLevel);
		}

		// check 2nd - play it if needed
		if (arpRow.triadNote === NoteInChord.SECOND_OF_TRIAD) {
			let rootTone2 = rootTone + octaveLevel;
			// @ts-ignore
			let rootToneNumber = Tonal.Note.midi(rootTone2);
			let secondTone = rootToneNumber! - 10;

			this.port.noteOn(channel, secondTone, 127).wait(500).noteOff(channel, secondTone);
		}

		// check third - play it if needed
		if (arpRow.triadNote === NoteInChord.THIRD_OF_TRIAD) {
			this.port.noteOn(channel, thirdTone + octaveLevel, 127).wait(500).noteOff(channel, thirdTone + octaveLevel);
		}

		// check fifth - play it if needed
		if (arpRow.triadNote === NoteInChord.FIFTH_OF_TRIAD) {
			this.port.noteOn(channel, fifthTone + octaveLevel, 127).wait(500).noteOff(channel, fifthTone + octaveLevel);
		}
	}
}
