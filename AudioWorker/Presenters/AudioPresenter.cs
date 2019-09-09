using System;
using System.Threading.Tasks;
using AudioWorker.Exceptions;
using NAudio.Wave;

namespace AudioWorker.Presenters
{
    public class AudioData
    {
        public String FileName { get; private set; }
        public TimeSpan? CurrentTime { get; private set; }
        public TimeSpan? TotalTime { get; private set; }
        public Single? Volume { get; private set; }

        internal AudioData() { }

        internal void Init(AudioFileReader audioFileReader)
        {
            if(audioFileReader == null)
            {
                throw new AudioFileReaderNotInitializedException();
            }
            this.FileName = audioFileReader.FileName;
            this.CurrentTime = audioFileReader.CurrentTime;
            this.TotalTime = audioFileReader.TotalTime;
            this.Volume = audioFileReader.Volume;
        }
    }

    internal class AudioPresenter : IAudioPresenter
    {
        private readonly WaveOutEvent _waveOutEvent;
        private AudioFileReader _fileReader;

        public AudioData AudioData { get; }

        public TimeSpan? GetCurrentPosition => AudioData?.CurrentTime;
        public TimeSpan? GetDuration => AudioData?.TotalTime;
        public String FileName => AudioData?.FileName;
        public Single? Volume => AudioData?.Volume;

        public int GetAudioData => 0;

        public AudioPresenter()
        {
            _waveOutEvent = new WaveOutEvent();
        }

        public void InitAudio(string path)
        {
            _fileReader = new AudioFileReader(path);
            _waveOutEvent.Init(_fileReader);
        }

        /// <summary>
        /// Value should be between 0.0 and 1.0
        /// </summary>
        public void ChangeVolume(float value)
        {
            if (value < 0 || value > 1)
                return;
            _waveOutEvent.Volume = value;
        }

        public void Pause()
        {
            if (_fileReader != null)
                _waveOutEvent.Pause();
        }

        public void Play()
        {
            if (_fileReader != null)
                _waveOutEvent.Play();
        }

        public void Stop()
        {
            if (_fileReader != null)
                _waveOutEvent.Stop();
        }

        public Task PlayAsync()
        {
            throw new NotImplementedException();
        }

        public Task StopAsync()
        {
            throw new NotImplementedException();
        }

        public Task PauseAsync()
        {
            throw new NotImplementedException();
        }
    }
}
