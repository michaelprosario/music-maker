using System.Collections.Generic;
using MusicMaker.Core.ValueObjects;

namespace MusicMaker.Core.Services
{
    public interface IChordPlayerTrack
    {
    }

    public abstract class AbstractChordPlayer
    {
        private readonly IChordPlayerTrack _chordPlayerTrack;

        protected AbstractChordPlayer(IChordPlayerTrack chordPlayerTrack)
        {
            _chordPlayerTrack = chordPlayerTrack;
        }
        
        public abstract void PlayOneBarPattern(ChordChange chordChange);
        public abstract void PlayTwoBarPattern(ChordChange chordChange);
        public abstract void PlayThreeBarPattern(ChordChange chordChange);
        public abstract void PlayFourBarPattern(ChordChange chordChange);

        public void PlayFromChordChanges(List<ChordChange> chordList)
        {
            foreach (var chordChange in chordList)
                if (chordChange.BeatCount == 2)
                {
                    PlayTwoBarPattern(chordChange);
                }
                else if (chordChange.BeatCount == 4)
                {
                    PlayFourBarPattern(chordChange);
                }
                else if (chordChange.BeatCount == 3)
                {
                    PlayFourBarPattern(chordChange);
                }
                else if (chordChange.BeatCount == 1)
                {
                    PlayOneBarPattern(chordChange);
                }
        }
    }
}