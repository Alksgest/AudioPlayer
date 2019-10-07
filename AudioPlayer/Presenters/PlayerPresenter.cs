using System;
using System.Linq;
using System.Collections.Generic;
using AudioPlayer.CustomEventArgs;
using AudioPlayer.Mappers;
using AudioPlayer.Models;
using AudioPlayer.Views;
using AudioWorker.Models;
using AudioPlayer.Managers;

namespace AudioPlayer.Presenters
{
    public class PlayerPresenter : Presenter<IMainView>
    {
        private const string FormatFilter =
            "Audio Files (*.mp3; *.wav; *.wma; *.flac; *.ogg; *.m4a) " +
            "|*.mp3;*.wav;*.wma;*.flac;*.ogg;*.m4a";

        private readonly IPlayer _player = PlayerFactory.GetPlayer();

        public AudioData CurrentAudioData => _player.CurrentAudioData;
        public IList<AudioData> AudioData => _player.AudioData;
        public IList<PathHolder> Files => _player.Files;

        public PlayerPresenter(IMainView view) : base(view) { }

        protected override void Initialize(object sender, EventArgs args)
        {
            base.Initialize(sender, args);
            View.LoadFiles += OnLoadFiles;
            View.ChangeAudio += OnChangeAudio;
            View.VolumeChanging += OnVolumeChanging;
        }

        private void SetAudioData()
        {
            foreach (var file in Files)
            {
                AudioData.Add(_player.GetAudioDataFromFile(file.FullPath));
            }
        }

        private void OnVolumeChanging(object sender, VolumeChangingEventArgs e) => _player.ChangeVolume(e.Volume);

        private void OnChangeAudio(object sender, PathHolderEventArgs e) => _player.ChangeAudio(e.PathHolder.FullPath);

        private void OnLoadFiles(object sender, EventArgs args)
        {
            var files = OpenFilesDialog();

            _player.Files = (List<PathHolder>)new PathHolderMapper().MapList(files);
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

        public void Play() => _player.Play();

        public void Stop() => _player.Stop();

        public void Pause() => _player.Pause();
    }
}