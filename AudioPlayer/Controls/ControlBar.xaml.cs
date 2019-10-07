using AudioPlayer.Presenters;
using AudioPlayer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AudioPlayer.Controls
{
    /// <summary>
    /// Interaction logic for ControlBar.xaml
    /// </summary>
    public partial class ControlBar : UserControl, IControlBarView
    {
        private readonly ControlBarPresenter _presenter;

        public event EventHandler PlayEventInvoked;
        public event EventHandler NextAudioInvoked;
        public event EventHandler PreviousAudioInvoked;
        public event EventHandler Initialize;

        public ControlBar()
        {
            _presenter = new ControlBarPresenter(this);

            InitializeComponent();
            SetDataContext();

            InvokeInitialization(new EventArgs());
        }

        private void InvokeInitialization(EventArgs args) => this.Initialize?.Invoke(this, args);

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            NextAudioInvoked?.Invoke(sender, e);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PlayEventInvoked?.Invoke(sender, e);
            PlayButton.Content = FindResource("Stop");
            PlayButton.Content = FindResource("Play");
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            PreviousAudioInvoked?.Invoke(sender, e);
        }

        private void DurationSlider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            _presenter.ChangeCurrentAudioPosition((Int32)(e.Source as Slider).Value);
        }

        private void DurationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<Double> e)
        {
            if (_presenter.IsAudioFinished)
            {
                NextAudioInvoked.Invoke(sender, e);
            }
        }

        public void SetDataContext()
        {
            CurrentTimeLabel.DataContext = _presenter.DataContext;
            FullTimeLabel.DataContext = _presenter.DataContext;
            DurationSlider.DataContext = _presenter.DataContext;
        }
    }
}
