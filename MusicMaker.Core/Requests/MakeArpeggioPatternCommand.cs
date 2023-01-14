using MusicMaker.Core.ValueObjects;

namespace MusicMaker.Core.Requests
{
    public class MakeArpeggioPatternCommand : IRequest
    {
        public ArpeggioPattern Pattern { get; set; } = new ArpeggioPattern();

        public string UserId { get; set; } = "";

        public int BeatsPerMinute { get; set; } = 120;
        public byte Channel { get; set; } = 0;

        public MakeArpeggioPatternCommand()
        {
            
        }
    }
}