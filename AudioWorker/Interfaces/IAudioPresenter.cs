using System;
using System.Threading.Tasks;
using AudioWorker.Models;
using AudioWorker.Presenters;

namespace AudioWorker.Interfaces
{
    public interface IAudioPresenter : IPlayer, IAsyncPlayer
    {
        AudioData AudioData { get; }

        void ChangeVolume(float value);
        void InitAudio(string path);
    }
}
