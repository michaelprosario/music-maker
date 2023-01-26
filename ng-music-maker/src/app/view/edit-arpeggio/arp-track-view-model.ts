
export class ArpTrackViewModel {
  trackData: number[];
  constructor(
    public rowName: string,
    public rowType: number,
    public octave: number,
    public measureCount: number,
    public beatsPerMeasure: number
  ) 
  {
    let itemCount = measureCount * beatsPerMeasure * 4;
    this.trackData = [];
    for (let i = 0; i < itemCount; i++) {
      this.trackData.push(0);
    }
  }
}
