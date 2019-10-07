using System;
using System.Collections.Generic;
using System.ComponentModel;
using AudioPlayer.Models;
using AudioWorker.Interfaces;
using AudioWorker.Models;

namespace AudioPlayer.Managers
{
    public interface IAudioPlayer : IPlayable, INotifyPropertyChanged
    {
        AudioData CurrentAudioData { get; }
        IList<PathHolder> Files { get; set; }
        IList<AudioData> AudioData { get; set; }
        Int32 IndexOfCurrentAudio { get; }

        PlaybackState PlaybackState { get; }

        void PlayOrStop();
        void ChangeAudio(String path);
        void ChangeCurrentAudioPosition(Int32 value);
        void ChangeVolume(Single value);
        void PlayPreviousAudio();
        void PlayNextAudio();
        AudioData GetAudioDataFromFile(string path);
    }
}