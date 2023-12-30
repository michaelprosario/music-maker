import { ArpRow } from './arp-row';
import { PIANO, cellSize } from './edit-arpeggio2.component';
import { ArpCell } from './arp-cell';
import { NoteInChord } from './note-in-chord';


export class ArpModel {
	beats = 4; // number of beats in view
	ticksPerBeat = 4;
	measureCount = 1;
	beatsPerMeasure = 4;
	instrumentCount = 0;
	rows: Array<ArpRow> = [];
	totalTicks = 0;
	bpm = 120;
	ticks: Array<number> = [];

	setup() {
		this.rows = [];
		this.totalTicks = this.getTotalTicks();

		this.setupInstrumentRow(0, "Fifth-3", NoteInChord.FIFTH_OF_TRIAD, 3, PIANO);
		this.setupInstrumentRow(1, "Third-3", NoteInChord.THIRD_OF_TRIAD, 3, PIANO);
		this.setupInstrumentRow(2, "2nd-3", NoteInChord.SECOND_OF_TRIAD, 3, PIANO);
		this.setupInstrumentRow(3, "Root-3", NoteInChord.ROOT_OF_TRIAD, 3, PIANO);

		this.setupInstrumentRow(4, "Fifth-2", NoteInChord.FIFTH_OF_TRIAD, 2, PIANO);
		this.setupInstrumentRow(5, "Third-2", NoteInChord.THIRD_OF_TRIAD, 2, PIANO);
		this.setupInstrumentRow(6, "2nd-2", NoteInChord.SECOND_OF_TRIAD, 2, PIANO);
		this.setupInstrumentRow(7, "Root-2", NoteInChord.ROOT_OF_TRIAD, 2, PIANO);

		this.setupInstrumentRow(8, "Fifth-1", NoteInChord.FIFTH_OF_TRIAD, 1, PIANO);
		this.setupInstrumentRow(9, "Third-1", NoteInChord.THIRD_OF_TRIAD, 1, PIANO);
		this.setupInstrumentRow(10, "2nd-1", NoteInChord.SECOND_OF_TRIAD, 1, PIANO);
		this.setupInstrumentRow(11, "Root-1", NoteInChord.ROOT_OF_TRIAD, 1, PIANO);

		for(let i=0; i<this.totalTicks; i++)
		{
			this.ticks.push(i);
		}
		this.instrumentCount = this.rows.length;
	}

	setupInstrumentRow(rowNumber: number, instrumentName: string, triadNote: number, level: number, patchNumber: number) {
		let row = new ArpRow();
		row.instrumentNumber = rowNumber; // row number
		row.patchNumber = patchNumber; // MIDI instrument used
		row.instrumentName = instrumentName; // Instrument name (i.e. piano )
		row.triadNote = triadNote; // See ROOT_OF_TRIAD ... FIFTH_OF_TRIAD
		row.level = level; // Octave {1,2,3}
		this.rows.push(row);
		
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
