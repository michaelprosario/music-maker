import { Injectable } from "@angular/core";

export interface ICommonChordProgression
{
    name: string;
    value: string;
}

@Injectable({
    providedIn: 'root'
})
export class ChordsService
{
    constructor(){}

    getRandom(progressions: ICommonChordProgression[])
    {
        return progressions[Math.floor(Math.random()*progressions.length)];
    }

    getRandomProgression() : string
    {
        debugger;
        let commonChords = this.getCommonChords();

        // find minor idea
        let minorProgressions = commonChords.filter(r => r.value.startsWith('Am'));
        let minorPart = this.getRandom(minorProgressions);

        // find major idea
        let majorProgressions = commonChords.filter(r => r.value.startsWith('C'));
        let majorPart = this.getRandom(majorProgressions);        

        // mix
        let verseRefrain = `${minorPart.value} ${minorPart.value} ${majorPart.value} ${majorPart.value}`;
        
        return `${verseRefrain} ${verseRefrain}`
    }

    getCommonChords()
    {

        let list = Array<ICommonChordProgression>();
        list.push({ value: 'Am Am F G', name: '6m-6m-4-5' })
        list.push({ value: 'Am C F G', name: '6m-1-4-5' })
        list.push({ value: 'Am Dm F G', name: '6m-2m-4-5' })
        list.push({ value: 'Am Dm G C', name: '6m-2m-5-1' })
        list.push({ value: 'Am Em Dm G', name: '6m-3m-2m-5' })
        list.push({ value: 'Am Em F G', name: '6m-3m-4-5' })
        list.push({ value: 'Am F C G', name: '6m-4-1-5' })
        list.push({ value: 'Am G F E', name: '6m-5-4-3Major' })
        list.push({ value: 'Am G F G', name: '6m-5-4-5' })
        list.push({ value: 'C Am F G', name: '1-6m-4-5' })           
        list.push({ value: 'C C Am G', name: '1-1-6m-5' })
        list.push({ value: 'C C F G', name: '1-1-4-5' })
        list.push({ value: 'C Dm Am G', name: '1-2m-6m-5' })
        list.push({ value: 'C Dm F G', name: '1-2m-4-5' })
        list.push({ value: 'C Em Dm G', name: '1-3m-2m-5' })
        list.push({ value: 'C Em F G', name: '1-3m-4-5' })
        list.push({ value: 'C Em F G', name: '1-3m-6m-5' })
        list.push({ value: 'C Em G G', name: '1-3m-5-5' })
        list.push({ value: 'C F Am G', name: '1-4-6m-5' })
        list.push({ value: 'C F Dm G', name: '1-4-6m-5' })
        list.push({ value: 'C F G F', name: '1-4-5-4' })
        list.push({ value: 'C F G G', name: '1-4-5-5' })
        list.push({ value: 'C G Am Em F C F G', name: 'canon' })
        list.push({ value: 'C G Am F', name: '1-5-6m-4' })
        list.push({ value: 'C G C G', name: '1-5-1-5' })
        list.push({ value: 'C G Dm G', name: '1-5-2m-5' })
        list.push({ value: 'C G F G', name: '1-5-4-5' })
        list.push({ value: 'Dm A Dm C F C Dm A Dm A Dm C F C Dm:2 A:2 Dm', name: 'lafolia' })
        list.push({ value: 'Dm C F G', name: '2m-1-4-5' })
        list.push({ value: 'Dm Dm Am G', name: '2m-2m-6m-5' })
        list.push({ value: 'Dm Dm F G', name: '2m-2m-4-5' })
        list.push({ value: 'Dm G C Am', name: '2m-5-1-6m' })
        list.push({ value: 'Dm G C C', name: '2m-5-1-1' })
        list.push({ value: 'Dm G C F', name: '2m-5-1-4' })
        list.push({ value: 'F C G Am', name: '4-1-5-6m' })
        list.push({ value: 'G Am F C', name: '5-6m-4-1' })
        list.push({ value: 'F E F E Am:2 Dm:2 E Am:2 Dm:2 E Am:2 Dm:2 E', name: 'bruno' })
    
        return list;
    }

}