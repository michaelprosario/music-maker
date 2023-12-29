
import { NoteLengthConstants } from '../view/edit-arpeggio/note-length-constants';
import { cellSize } from './edit-arpeggio2.component';

export class ArpCell {
	tick = 0;
	value = 0;
	width = cellSize;
	height = cellSize;
	x = 0;
	y = 0;
	noteLength: number = NoteLengthConstants.SIXTEENTH_NUMBER;
	cellSize: number = cellSize;
}
