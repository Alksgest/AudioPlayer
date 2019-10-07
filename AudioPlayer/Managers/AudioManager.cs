using System;
using System.Collections.Generic;
using System.Linq;
using AudioPlayer.Models;
using AudioWorker.Factories;
using AudioWorker.Interfaces;
using AudioWorker.Models;

namespace AudioPlayer.Managers
{
    public class AudioManager : IPlayer
    {
        private readonly IAudioProvider _provider = AudioProviderFactory.GetAudioPlayer();

        public IList<PathHolder> Files { get; set; } = new List<PathHolder>();
        public IList<AudioData> AudioData { get; set; } = new List<AudioData>();

        public AudioData CurrentAudioData
        {
            get
            {
                return _provider.AudioData;
            }
        }

        public void Play() => _provider.Play();
        public void Stop() => _provider.Stop();
        public void Pause() => _provider.Pause();

        public void PlayOrStop()
        {
            if (_provider.PlaybackState != PlaybackState.Stoped || _provider.PlaybackState == PlaybackState.Paused)
                Stop();
            else
                Play();
        }

        public void ChangeAudio(String path)
        {
            if (_provider.PlaybackState != PlaybackState.Stoped)
                Stop();

            _provider.InitAudio(path);

            Play();
        }

        public void ChangeCurrentAudioPosition(Int32 value) => _provider.ChangeAudioPosition(value);

        public void ChangeVolume(Single value) => _provider.ChangeVolume(value);

        public AudioData GetAudioDataFromFile(String path) => _provider.GetAudioDataFromFile(path);

        public void PlayNextAudio()
        {
            Int32 size = AudioData.Count;
            Int32 position = IndexOfCurrentAudio();
            var nextPosition = position - 1 <= 0 ? size - 1 : position - 1;

            ChangeAudio(Files[nextPosition].FullPath);
        }

        public void PlayPreviousAudio()
        {
            Int32 size = AudioData.Count;
            Int32 position = IndexOfCurrentAudio();
            var nextPosition = position + 1 == size ? 0 : position + 1;

            ChangeAudio(Files[nextPosition].FullPath);
        }

        private Int32 IndexOfCurrentAudio() 
            => AudioData.IndexOf(AudioData.SingleOrDefault(d => d.FilePath == CurrentAudioData?.FilePath));
    }
}
