using System;
using AudioWorker.CustomEventArgs;
using AudioWorker.Models;

namespace AudioWorker.Interfaces
{

    public interface IAudioProvider : IPlayable, IAsyncPlayable
    {
        AudioData AudioData { get; }
        AudioData GetAudioDataFromFile(string fullPath);
        PlaybackState PlaybackState { get; }

        void ChangeVolume(float value);
        void ChangeAudioPosition(int seconds);
        void InitAudio(string path);

        void SubscribeOnPlaybackStopped(EventHandler<PlaybackStoppedEventArgs> method);

    }
}
