using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using AudioWorker.CustomEventArgs;
using AudioWorker.Interfaces;
using AudioWorker.Models;
using NAudio.Wave;
using PlaybackState = AudioWorker.Models.PlaybackState;

namespace AudioWorker.Providers
{
    internal class AudioProvider : IAudioProvider
    {
        private readonly WaveOutEvent _waveOutEvent;
        private AudioFileReader _fileReader;

        private readonly Timer _timer = new Timer();

        public AudioData AudioData { get; set; }

        public PlaybackState PlaybackState
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

            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
        }

        public void SubscribeOnPlaybackStopped(EventHandler<PlaybackStoppedEventArgs> method)
        {
            _waveOutEvent.PlaybackStopped += (sender, args) =>
            {
                method(sender, args as PlaybackStoppedEventArgs);
            };
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.AudioData.Init(_fileReader);
        }

        public void InitAudio(string path)
        {
            _fileReader = new AudioFileReader(path);
            _waveOutEvent.Init(_fileReader);

            InitializeAudioData();
        }

        public AudioData GetAudioDataFromFile(string fullPath)
        {
            var reader = new AudioFileReader(fullPath);
            AudioData data = new AudioData();
            data.Init(reader);

            return data;
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

        public void ChangeAudioPosition(int seconds)
        {
            this._fileReader.CurrentTime = new TimeSpan(0, 0, seconds);
            this.AudioData.Init(_fileReader);
        }

        public void Pause()
        {
            if (_fileReader != null)
            {
                _waveOutEvent.Pause();
                _timer.Stop();
            }
        }

        public void Play()
        {
            if (_fileReader != null)
            {
                _waveOutEvent.Play();
                _timer.Start();
            }
        }

        public void Stop()
        {
            if (_fileReader != null)
            {
                _waveOutEvent.Stop();
                _timer.Stop();
            }
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
