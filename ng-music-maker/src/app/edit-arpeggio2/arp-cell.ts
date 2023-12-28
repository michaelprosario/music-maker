import { NoteLength } from './note-length';
import { cellSize } from './edit-arpeggio2.component';

export class ArpCell {
	tick = 0;
	value = 0;
	width = cellSize;
	height = cellSize;
	x = 0;
	y = 0;
	noteLength = NoteLength.SIXTEENTH;
	cellSize: number = cellSize;
}
