using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Managers
{
    class AudioPlayerFactory
    {
        private static IAudioPlayer _player;
        public static IAudioPlayer Player => _player ?? (_player = new AudioManager());
    }
}
