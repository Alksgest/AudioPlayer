using System;
using System.Threading.Tasks;
using AudioWorker.Models;
using NAudio.Wave;
using PlaybackState = AudioWorker.Interfaces.PlaybackState;

namespace AudioWorker.Providers
{
    internal class AudioProvider : Interfaces.IAudioProvider
    {
        private readonly WaveOutEvent _waveOutEvent;
        private AudioFileReader _fileReader;

        public AudioData AudioData { get; private set; }

        public Interfaces.PlaybackState PlaybackState
        {
            get
            {
                switch (_waveOutEvent.PlaybackState)
                {
                    case NAudio.Wave.PlaybackState.Playing:
                        return PlaybackState.Playing;
                    case
                        NAudio.Wave.PlaybackState.Paused:
                        return PlaybackState.Paused;
                    case NAudio.Wave.PlaybackState.Stopped:
                        return PlaybackState.Stoped;
                }
                return default;
            }
        }

        public AudioProvider()
        {
            _waveOutEvent = new WaveOutEvent();
        }

        public void InitAudio(string path)
        {
            _fileReader = new AudioFileReader(path);
            _waveOutEvent.Init(_fileReader);

            InitializeAudioData();
        }

        

        private void InitializeAudioData()
        {
            AudioData = new AudioData();
            AudioData.Init(_fileReader);
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
