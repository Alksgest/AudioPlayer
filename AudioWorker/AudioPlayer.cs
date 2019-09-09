using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWorker
{
    public interface IAudioPlayer
    {
        void Play();
        void Stop();
        void Pause();

        void InitAudio(string path);

        TimeSpan? GetCurrentPosition { get; }
        TimeSpan? GetDuration { get; }
        //return sometype
        /*AudioData*/
        int GetAudioData { get; }

        void ChangeVolume(float value);
    }

    public static class AudioPlayerFactory
    {
        public static IAudioPlayer GetAudioPlayer()
        {
            return new AudioPlayer();
        }
    }

    public class AudioPlayer : IAudioPlayer
    {
        private WaveOutEvent _waveOutEvent;
        private AudioFileReader _fileReader;

        public TimeSpan? GetCurrentPosition => _fileReader?.CurrentTime;

        public TimeSpan? GetDuration => _fileReader.TotalTime;

        public int GetAudioData => 0;

        public AudioPlayer()
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
    }
}
