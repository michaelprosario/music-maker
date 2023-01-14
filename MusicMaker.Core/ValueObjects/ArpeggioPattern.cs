using System.Collections.Generic;

namespace MusicMaker.Core.ValueObjects
{
    public enum ArpeggioPatternRowType
    {
        Root, Second, Third, Fifth, Sixth, Seventh, MajorSeventh
    }
    
    public class ArpeggioPatternRow
    {
        public int Octave { get; set; }
        public ArpeggioPatternRowType Type { get; set; }
        public string Pattern { get; set; } = "";
    }
    
    public class ArpeggioPattern
    {
        public List<ArpeggioPatternRow> Rows { get; set; } = new List<ArpeggioPatternRow>();
        public int InstrumentNumber { get; set; }
    }
}