using AudioPlayer.Managers;
using AudioPlayer.Views;
using AudioWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Presenters
{
    class ControlBarPresenter : Presenter<IControlBarView>
    {

        private readonly IPlayer _player = PlayerFactory.GetPlayer();

        public AudioData DataContext => _player.CurrentAudioData;

        public bool IsAudioFinished => _player.CurrentAudioData.CurrentTime >= _player.CurrentAudioData.TotalTime;

        public ControlBarPresenter(IControlBarView view) : base(view) { }

        protected override void Initialize(Object sender, EventArgs args)
        {
            base.Initialize(sender, args);

            View.PlayEventInvoked += View_PlayButtonPressed;
            View.NextAudioInvoked += View_NextButtonPressed;
            View.PreviousAudioInvoked += View_PreviousButtonPressed;
        }

        private void View_PlayButtonPressed(Object sender, EventArgs e) => PlayOrStop();
        private void View_NextButtonPressed(Object sender, EventArgs e) => PlayNextAudio();
        private void View_PreviousButtonPressed(Object sender, EventArgs e) => PlayPreviousAudio();

        private void PlayNextAudio() => _player.PlayNextAudio();
        private void PlayPreviousAudio() => _player.PlayPreviousAudio();
        private void PlayOrStop() => _player.PlayOrStop();
        public void ChangeCurrentAudioPosition(Int32 value) => _player.ChangeCurrentAudioPosition(value);
    }
}
