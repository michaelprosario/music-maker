import { ArpRow } from './arp-row';
import { FIFTH_OF_TRIAD, PIANO, THIRD_OF_TRIAD, SECOND_OF_TRIAD, ROOT_OF_TRIAD, cellSize } from './edit-arpeggio2.component';
import { ArpCell } from './arp-cell';


export class ArpModel {
	beats = 4; // number of beats in view
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
		row.triadNote = triadNote; // See ROOT_OF_TRIAD ... FIFTH_OF_TRIAD
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
