using System;
using System.Windows;
using System.Windows.Input;
using AudioPlayer.Models;
using AudioPlayer.Presenters;
using AudioPlayer.Views;
using AudioWorker.Factories;
using AudioWorker.Interfaces;

namespace AudioPlayer
{
    public partial class MainWindow : Window, IMainView
    {

        public event EventHandler Initialize;
        public event EventHandler LoadFiles;
        public event EventHandler<PathHolderEventArgs> ChangeAudio;

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
            this.FilesListView.ItemsSource = _presenter.Files;
        }

        private void FilesListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChangeAudio?.Invoke(sender, new PathHolderEventArgs
            {
                PathHolder = FilesListView.SelectedItem as PathHolder
            });
        }
    }
}
