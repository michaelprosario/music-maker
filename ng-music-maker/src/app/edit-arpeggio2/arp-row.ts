import { NoteLengthConstants } from '../view/edit-arpeggio/note-length-constants';
import { ArpCell } from './arp-cell';

export class ArpRow {
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
				currentCell.value = NoteLengthConstants.SIXTEENTH_NUMBER;
			} else {
				currentCell.value = 0;
			}

		}
	}

	pattern2(totalTicks: number) {
		for (let tick = 0; tick < totalTicks; tick++) {
			let currentCell = this.cells[tick];
			if (tick % 8 === 4) {
				currentCell.value = NoteLengthConstants.SIXTEENTH_NUMBER;
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
				currentCell.value = NoteLengthConstants.SIXTEENTH_NUMBER;
			} else {
				currentCell.value = 0;
			}

		}
	}
}
