using System;
using System.Threading.Tasks;
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

        void ChangeVolume(float value);
        void InitAudio(string path);

        AudioData GetAudioData(string fullPath);

        PlaybackState PlaybackState { get; }

    }
}
