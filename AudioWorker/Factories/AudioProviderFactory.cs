using AudioWorker.Interfaces;
using AudioWorker.Providers;

namespace AudioWorker.Factories
{
    public static class AudioProviderFactory
    {
        public static IAudioProvider GetAudioPlayer()
        {
            return new AudioProvider();
        }
    }
}
