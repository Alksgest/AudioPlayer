using System;
using System.Collections.Generic;
using AudioPlayer.Models;
using AudioWorker.Interfaces;
using AudioWorker.Models;

namespace AudioPlayer.Managers
{
    public interface IPlayer : IPlayable
    {
        AudioData CurrentAudioData { get; }
        IList<PathHolder> Files { get; set; }
        IList<AudioData> AudioData { get; set; }

        void PlayOrStop();
        void ChangeAudio(String path);
        void ChangeCurrentAudioPosition(Int32 value);
        void ChangeVolume(Single value);
        void PlayNextAudio();
        void PlayPreviousAudio();
        AudioData GetAudioDataFromFile(string path);
    }
}