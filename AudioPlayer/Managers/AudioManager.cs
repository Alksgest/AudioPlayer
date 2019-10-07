using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AudioPlayer.Models;
using AudioWorker.Factories;
using AudioWorker.Interfaces;
using AudioWorker.Models;

namespace AudioPlayer.Managers
{
    public class AudioManager : IAudioPlayer
    {
        private readonly IAudioProvider _provider = AudioProviderFactory.GetAudioPlayer();

        public event PropertyChangedEventHandler PropertyChanged;

        public IList<PathHolder> Files { get; set; } = new List<PathHolder>();
        public IList<AudioData> AudioData { get; set; } = new List<AudioData>();
        public PlaybackState PlaybackState => _provider.PlaybackState;

        public AudioData CurrentAudioData => _provider.AudioData;
        public Int32 IndexOfCurrentAudio
            => AudioData.IndexOf(AudioData.SingleOrDefault(d => d.FilePath == CurrentAudioData?.FilePath));

        public void Play() => _provider.Play();
        public void Stop() => _provider.Stop();
        public void Pause() => _provider.Pause();

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PlayOrStop()
        {
            if (_provider.PlaybackState != PlaybackState.Stoped || _provider.PlaybackState == PlaybackState.Paused)
                Stop();
            else
                Play();
        }

        public void ChangeAudio(String path)
        {
            var previousState = _provider.PlaybackState;

            if (_provider.PlaybackState != PlaybackState.Stoped)
                Stop();

            _provider.InitAudio(path);

            if (previousState == PlaybackState.Playing)
                Play();

            NotifyPropertyChanged("CurrentAudioData");
        }

        public void ChangeCurrentAudioPosition(Int32 value) => _provider.ChangeAudioPosition(value);

        public void ChangeVolume(Single value) => _provider.ChangeVolume(value);

        public AudioData GetAudioDataFromFile(String path) => _provider.GetAudioDataFromFile(path);

        public void PlayPreviousAudio()
        {
            Int32 size = AudioData.Count;
            Int32 position = IndexOfCurrentAudio;
            var nextPosition = position - 1 <= 0 ? size - 1 : position - 1;

            ChangeAudio(Files[nextPosition].FullPath);
            NotifyPropertyChanged("IndexOfCurrentAudio");
        }

        public void PlayNextAudio()
        {
            Int32 size = AudioData.Count;
            Int32 position = IndexOfCurrentAudio;
            var nextPosition = position + 1 == size ? 0 : position + 1;

            ChangeAudio(Files[nextPosition].FullPath);
            NotifyPropertyChanged("IndexOfCurrentAudio");
        }
    }
}
