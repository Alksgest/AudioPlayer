using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AudioPlayer.CustomEventArgs;
using AudioPlayer.Models;
using AudioPlayer.Presenters;
using AudioPlayer.Views;
using AudioWorker.Models;

namespace AudioPlayer
{
    public partial class MainWindow : Window, IMainView
    {

        public event EventHandler Initialize;
        public event EventHandler LoadFiles;
        public event EventHandler AudioStopped;
        public event EventHandler<PathHolderEventArgs> ChangeAudio;
        public event EventHandler<VolumeChangingEventArgs> VolumeChanging;

        private readonly PlayerPresenter _presenter;

        public MainWindow()
        {
            _presenter = new PlayerPresenter(this);
            InitializeComponent();

            InvokeInitialization(new EventArgs());
        }

        private void InvokeInitialization(EventArgs args) => this.Initialize?.Invoke(this, args);

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _presenter.Play();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _presenter.Stop();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            _presenter.Pause();
        }

        private void OpenFolderButton_Click(object sender, RoutedEventArgs e)
        {
            LoadFiles?.Invoke(sender, e);
            this.AudioDataGrid.ItemsSource = _presenter.AudioData;
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VolumeChanging?.Invoke(sender, new VolumeChangingEventArgs
            {
                Volume = (Single)e.NewValue
            });
        }

        private void AudioDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var data = AudioDataGrid.SelectedItem as AudioData;
            ChangeAudioViaNewAudioData(sender, data);
            SetDataContext();
        }

        private void SetDataContext()
        {
            CurrentTimeLabel.DataContext = _presenter.CurrentData;
            FullTimeLabel.DataContext = _presenter.CurrentData;
            DurationSlider.DataContext = _presenter.CurrentData;

            AudioDataGrid.SelectedIndex = _presenter.IndexOfCurrentAudio();
        }

        private void ChangeAudioViaNewAudioData(object sender, AudioData data)
        {
            ChangeAudio?.Invoke(sender, new PathHolderEventArgs
            {
                PathHolder = new PathHolder
                {
                    FullPath = data.FilePath,
                    Title = data.FileName
                }
            });
        }

        private void DurationSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            _presenter.ChangeCurrentAudioPosition((e.Source as Slider).Value);
        }

        private void DurationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //if ((Int32)(e.Source as Slider).Value >= (Int32)_presenter.CurrentData.TotalTime.Value.TotalSeconds)
            //    AudioStopped?.Invoke(sender, e);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _presenter.PlayNextAudio(true);
            SetDataContext();
        }

        private void PrevtButton_Click(object sender, RoutedEventArgs e)
        {
            _presenter.PlayNextAudio(false);
            SetDataContext();
        }
    }
}
