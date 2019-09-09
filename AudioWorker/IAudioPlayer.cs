using System;
using System.Threading.Tasks;

namespace AudioWorker
{
    public interface IAudioPresenter
    {
        void Play();
        void Stop();
        void Pause();

        Task PlayAsync();
        Task StopAsync();
        Task PauseAsync();

        void InitAudio(string path);

        TimeSpan? GetCurrentPosition { get; }

        TimeSpan? GetDuration { get; }

        //return sometype
        /*AudioData*/
        int GetAudioData { get; }

        void ChangeVolume(float value);
    }
}
