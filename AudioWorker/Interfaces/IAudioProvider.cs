using System;
using AudioWorker.CustomEventArgs;
using AudioWorker.Models;

namespace AudioWorker.Interfaces
{
    public enum PlaybackState
    {
        Playing,
        Paused,
        Stoped
    }

    public interface IAudioProvider : IPlayer, IAsyncPlayer
    {
        AudioData AudioData { get; }
        AudioData GetAudioData(string fullPath);
        PlaybackState PlaybackState { get; }

        void ChangeVolume(float value);
        void ChangeAudioPosition(int seconds);
        void InitAudio(string path);

        void SubscribeOnPlaybackStopped(EventHandler<PlaybackStoppedEventArgs> method);

    }
}
