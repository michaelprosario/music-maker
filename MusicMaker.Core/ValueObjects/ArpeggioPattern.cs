using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MusicMaker.Core.ValueObjects
{
    [DataContract]
    public enum ArpeggioPatternRowType
    {
        Root,
        Second,
        Third,
        Fifth,
        Sixth,
        Seventh,
        MajorSeventh
    }

    [DataContract]
    public class ArpeggioPatternRow
    {
        [DataMember] public int Octave { get; set; }
        [DataMember] public ArpeggioPatternRowType Type { get; set; }
        [DataMember] public string Pattern { get; set; } = "";

        public ArpeggioPatternRow Clone()
        {
            var clone = new ArpeggioPatternRow()
            {
                Octave = this.Octave,
                Type = this.Type,
                Pattern = this.Pattern
            };
            return clone;
        }
    }

    [DataContract]
    public class ArpeggioPattern
    {
        [DataMember] public List<ArpeggioPatternRow> Rows { get; set; } = new();

        public ArpeggioPattern Clone()
        {
            var pattern = new ArpeggioPattern();

            foreach (var row in Rows)
            {
                pattern.Rows.Add(row.Clone());    
            }
            
            return pattern;
        }

    public ArpeggioPattern CopyFirstMeasures(int measureCount)
    {
      var response = Clone();
      foreach(var row in response.Rows)
      {
        row.Pattern = row.Pattern.Replace("|", "");
        row.Pattern = row.Pattern.Substring(0, measureCount*4);
      }

      return response;
    }
  }
}