import { Component, OnInit } from '@angular/core';
import { ArpMakerService } from '../view/edit-arpeggio/arp-maker-service';
import { ArpModel } from './arp-model';

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
	

	constructor(private arpMakerService: ArpMakerService)
	{
		this.numberOfMeasures = 2;
		this.beatsPerMeasure = 4;
		this.arpGridModel = new ArpModel();
	}

	start() {
		this.enablePlayButton();
	}

	enablePlayButton() {
		this.playEnabled = true;
	}	
  
	ngOnInit(): void {
		this.arpGridModel = new ArpModel();
		this.arpGridModel.setup();		
	}
}
