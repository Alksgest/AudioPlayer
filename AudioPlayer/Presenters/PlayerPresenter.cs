using System;
using System.Collections.Generic;
using System.Windows.Documents;
using AudioPlayer.Mappers;
using AudioPlayer.Models;
using AudioPlayer.Views;
using AudioWorker.Factories;
using AudioWorker.Interfaces;
using AudioWorker.Models;

namespace AudioPlayer.Presenters
{
    public class PlayerPresenter : Presenter<IMainView>, IPlayer
    {

        private const string FormatFilter =
            "Audio Files (*.mp3; *.wav; *.wma; *.flac; *.ogg; *.m4a) " +
            "|*.mp3;*.wav;*.wma;*.flac;*.ogg;*.m4a";

        private readonly IAudioProvider _provider = AudioProviderFactory.GetAudioPlayer();

        public List<PathHolder> Files { get; private set; } = new List<PathHolder>();

        public AudioData CurrentData
        {
            get
            {
                return _provider.AudioData;
            }
        }

        public List<AudioData> AudioData { get; private set; } = new List<AudioData>();

        public PlayerPresenter(IMainView view) : base(view) { }

        private void SetAudioData()
        {
            foreach(var file in Files)
            {
                AudioData.Add(_provider.GetAudioData(file.FullPath));
            }
        }

        protected override void Initialize(object sender, EventArgs args)
        {
            base.Initialize(sender, args);
            View.LoadFiles += OnLoadFiles;
            View.ChangeAudio += OnChangeAudio;
            View.VolumeChanging += OnVolumeChanging;
        }

        private void OnVolumeChanging(object sender, VolumeChangingEventArgs e) => _provider.ChangeVolume(e.Volume);

        private void OnChangeAudio(object sender, PathHolderEventArgs e)
        {
            Stop();
            
            _provider.InitAudio(e.PathHolder.FullPath);

            Play();
        }

        private void OnLoadFiles(object sender, EventArgs args)
        {
            var files = OpenFilesDialog();

            Files = (List<PathHolder>)new PathHolderMapper().MapList(files);
            SetAudioData();
        }

        private string[] OpenFilesDialog()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".mp3",
                Filter = FormatFilter,
                Multiselect = true
            };

            dlg.ShowDialog();
            return dlg.FileNames;
        }

        public void Play()
        {
            _provider.Play();
        }

        public void Stop()
        {
            _provider.Stop();
        }

        public void Pause()
        {
            _provider.Pause();
        }
    }
}