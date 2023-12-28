import { ChordChange } from './ChordChange';


export class ChordSequence {
	chords: Array<ChordChange> = [];

	parse(strChordProgression: string) {
		let tokens = strChordProgression.split(" ");
		this.chords = [];
		for (let token of tokens) {
			if (token.indexOf(':') === -1) {
				let chordChange = new ChordChange();
				chordChange.chordSymbol = token;
				chordChange.beatCount = 4;
				this.chords.push(chordChange);
			} else {
				let splitToken = token.split(":");
				let chordChange = new ChordChange();
				chordChange.chordSymbol = splitToken[0];
				chordChange.beatCount = parseInt(splitToken[1]);
				this.chords.push(chordChange);
			}
		}

		return true;
	}

	getChordTickSchedule() {
		let response = [];
		let currentTick = 0;
		for (let chordChange of this.chords) {
			chordChange.startTick = currentTick;
			for (let tick = 0; tick < chordChange.beatCount * 4; tick++) {
				response.push(chordChange.chordSymbol);
				currentTick++;
			}
		}
		return response;
	}

	getTotalBeats() {
		let beatCount = 0;
		for (let chordChange of this.chords) {
			beatCount = beatCount + chordChange.beatCount;
		}

		return beatCount;
	}
}
