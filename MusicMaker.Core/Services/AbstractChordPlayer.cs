using System.Collections.Generic;
using MusicMaker.Core.ValueObjects;

namespace MusicMaker.Core.Services
{
    public interface IChordPlayerTrack
    {
    }

    public abstract class AbstractChordPlayer
    {
        public abstract void PlayOneBarPattern(IChordPlayerTrack track, int channel, ChordChange2 chordChange);
        public abstract void PlayTwoBarPattern(IChordPlayerTrack track, int channel, ChordChange2 chordChange);
        public abstract void PlayThreeBarPattern(IChordPlayerTrack track, int channel, ChordChange2 chordChange);
        public abstract void PlayFourBarPattern(IChordPlayerTrack track, int channel, ChordChange2 chordChange);

        public void PlayFromChordChanges(IChordPlayerTrack track, List<ChordChange2> chordList, int channel)
        {
            foreach (var chordChange in chordList)
                if (chordChange.BeatCount == 2)
                {
                    PlayTwoBarPattern(track, channel, chordChange);
                }
                else if (chordChange.BeatCount == 4)
                {
                    PlayFourBarPattern(track, channel, chordChange);
                }
                else if (chordChange.BeatCount == 3)
                {
                    PlayFourBarPattern(track, channel, chordChange);
                }
                else if (chordChange.BeatCount == 1)
                {
                    PlayOneBarPattern(track, channel, chordChange);
                }
        }
    }
}