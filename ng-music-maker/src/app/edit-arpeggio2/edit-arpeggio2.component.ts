import { Component, OnInit } from '@angular/core';
import { ArpMakerService } from '../view/edit-arpeggio/arp-maker-service';
import { ArpTrackViewModel } from '../view/edit-arpeggio/arp-track-view-model';



export class ChordChange
{
	chordSymbol: string = "";
	beatCount: number = 4;
	startTick: number = 0;
}

export class ChordSequence{
	chords: Array<ChordChange> = [];

	parse(strChordProgression: string){
		let tokens = strChordProgression.split(" ");
		this.chords = [];
		for(let token of tokens)
		{
			if(token.indexOf(':') === -1)
			{
				let chordChange = new ChordChange();
				chordChange.chordSymbol = token;
				chordChange.beatCount = 4;
				this.chords.push(chordChange);
			}else{
				let splitToken = token.split(":");
				let chordChange = new ChordChange();
				chordChange.chordSymbol = splitToken[0];
				chordChange.beatCount = parseInt(splitToken[1]);
				this.chords.push(chordChange);
			}
		}

		return true;
	}

	getChordTickSchedule()
	{
		let response = [];
		let currentTick = 0;
		for(let chordChange of this.chords)
		{
			chordChange.startTick = currentTick;
			for(let tick=0;  tick<chordChange.beatCount*4;  tick++)
			{
				response.push(chordChange.chordSymbol);
				currentTick++;
			}
		}
		return response;
	}

	getTotalBeats()
	{
		let beatCount = 0;
		for(let chordChange of this.chords)
		{
			beatCount = beatCount + chordChange.beatCount;
		}

		return beatCount;
	}
}

let cellSize = 50;
let MAX_VELOCITY = 127;

let SIXTEENTH = 1;
let EIGTH = 2;
let QUARTER = 4;
let HALF = 8;
let WHOLE = 16;

let ROOT_OF_TRIAD = 0;
let SECOND_OF_TRIAD = 2;
let THIRD_OF_TRIAD = 3;
let FIFTH_OF_TRIAD = 5;

let PIANO = 0;


export class ArpCell {
	tick = 0;
	value = 0;
	width = cellSize;
	height = cellSize;
	x = 0;
	y = 0;
	noteLength = SIXTEENTH;
  cellSize: number = cellSize;
}

class ArpRow {
	cells: Array<ArpCell> = [];
	instrumentNumber = 0;
	patchNumber = 0;
	instrumentName = '';
	level = 1;
	triadNote = 0;

	clearRow(totalTicks: number) {
		for (let tick = 0; tick < totalTicks; tick++) {
			let currentCell = this.cells[tick];
			currentCell.value = 0;
		}
	}

	pattern1(totalTicks: number) {
		for (let tick = 0; tick < totalTicks; tick++) {
			let currentCell = this.cells[tick];
			if (tick % 4 === 0) {
				currentCell.value = MAX_VELOCITY;
			} else {
				currentCell.value = 0;
			}

		}
	}

	pattern2(totalTicks: number) {
		for (let tick = 0; tick < totalTicks; tick++) {
			let currentCell = this.cells[tick];
			if (tick % 8 === 4) {
				currentCell.value = MAX_VELOCITY;
			} else {
				currentCell.value = 0;
			}

		}
	}

	randomPattern(totalTicks: number) {
		for (let tick = 0; tick < totalTicks; tick++) {
			let currentCell = this.cells[tick];

			let r = Math.random();

			if (r < 0.2) {
				currentCell.value = MAX_VELOCITY;
			} else {
				currentCell.value = 0;
			}

		}
	}
}

export class ArpModel {
	beats = 4;  // number of beats in view
	ticksPerBeat = 4;
	measureCount = 1;
	beatsPerMeasure = 4;
	instrumentCount = 0;
	rows: Array<ArpRow> = [];
	totalTicks = 0;
	bpm = 120;

	setup() {
		this.rows = [];

		this.setupInstrumentRow(4, "Fifth-2", FIFTH_OF_TRIAD, 2, PIANO);
		this.setupInstrumentRow(5, "Third-2", THIRD_OF_TRIAD, 2, PIANO);
		this.setupInstrumentRow(6, "2nd-2", SECOND_OF_TRIAD, 2, PIANO);
		this.setupInstrumentRow(7, "Root-2", ROOT_OF_TRIAD, 2, PIANO);


		this.setupInstrumentRow(8, "Fifth-1", FIFTH_OF_TRIAD, 1, PIANO);
		this.setupInstrumentRow(9, "Third-1", THIRD_OF_TRIAD, 1, PIANO);
		this.setupInstrumentRow(10, "2nd-1", SECOND_OF_TRIAD, 1, PIANO);
		this.setupInstrumentRow(11, "Root-1", ROOT_OF_TRIAD, 1, PIANO);

		this.instrumentCount = this.rows.length;
	}

	setupInstrumentRow(rowNumber: number, instrumentName: string, triadNote: number, level: number, patchNumber: number) {
		let row = new ArpRow();
		row.instrumentNumber = rowNumber; // row number
		row.patchNumber = patchNumber; // MIDI instrument used
		row.instrumentName = instrumentName; // Instrument name (i.e. piano )
		row.triadNote = triadNote;  // See ROOT_OF_TRIAD ... FIFTH_OF_TRIAD
		row.level = level; // Octave {1,2,3}
		this.rows.push(row);

		this.totalTicks = this.getTotalTicks();
		for (let t = 0; t < this.totalTicks; t++) {
			let cell = new ArpCell();
			cell.tick = t;
			cell.cellSize = cellSize;
			row.cells.push(cell);
		}
	}

	clearRows() {
		for (let row = 0; row < this.instrumentCount; row++) {
			let aprRow = this.rows[row];
			let totalTicks = this.getTotalTicks();
			aprRow.clearRow(totalTicks);
		}
	}

	getTotalTicks() {
		return this.measureCount * this.beatsPerMeasure * this.ticksPerBeat;
	}

	getMSFromBPM(bpm: number) {
		var numerator = 60 * 1000; // 60 seconds times 1000 measureCount
		var denominator = bpm * 4;
		var response = numerator / denominator;

		return response;
	}

	getInstrumentRowValue(intRowIndex: number, intTimeIndex: number) {
		let instrumentRow = this.rows[intRowIndex];
		if (!instrumentRow)
			throw new Error("InstrumentRow is not defined");

		let cell = instrumentRow.cells[intTimeIndex];
		if (!cell)
			throw new Error("Cell is not defined");

		return cell.value;
	}
}


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
	tracks: ArpTrackViewModel[] = [];

	constructor(private arpMakerService: ArpMakerService)
	{
		this.numberOfMeasures = 2;
		this.beatsPerMeasure = 4;
	}

	start() {
		this.enablePlayButton();
	}

	enablePlayButton() {
		this.playEnabled = true;
	}	
  
	ngOnInit(): void {
		this.setupTrackRows();
	}
	
	private setupTrackRows() 
	{
		this.tracks = this.arpMakerService.setupTrackRows(this.numberOfMeasures, this.beatsPerMeasure);
	}
	


}
