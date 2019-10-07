using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Managers
{
    class PlayerFactory
    {
        private static IPlayer _player;
        public static IPlayer GetPlayer()
        {
            return _player ?? (_player = new AudioManager());
        }
    }
}
