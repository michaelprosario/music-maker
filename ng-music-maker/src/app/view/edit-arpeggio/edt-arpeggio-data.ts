import { ArpTrackViewModel } from "./arp-track-view-model";

export class EditArpeggioData
{

constructor(
    public tempo: number,
    public beatsPerMeasure: number,
    public numberOfMeasures: number,
    public tracks: ArpTrackViewModel[],
    public id: string,
    public instrument: number
)
{
}

}