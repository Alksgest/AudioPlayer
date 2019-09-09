using AudioWorker.Interfaces;
using AudioWorker.Presenters;

namespace AudioWorker.Factories
{
    public static class AudioPlayerFactory
    {
        public static IAudioPresenter GetAudioPlayer()
        {
            return new AudioPresenter();
        }
    }
}
