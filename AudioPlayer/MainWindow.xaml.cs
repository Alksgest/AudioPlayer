using AudioWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AudioPlayer
{
    public partial class MainWindow : Window
    {
        IAudioPlayer _player;

        private const string _sampleAudio = @"C:\Personal Data\01 - Black Star.mp3";
        public MainWindow()
        {
            InitializeComponent();

            _player = AudioPlayerFactory.GetAudioPlayer();
            _player.InitAudio(_sampleAudio);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _player.Play();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _player.Stop();
        }
    }
}
