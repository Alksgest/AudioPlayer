using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.Views
{
    interface IControlBarView : IView
    {
        event EventHandler PlayEventInvoked;
        event EventHandler NextAudioInvoked;
        event EventHandler PreviousAudioInvoked;

        void SetDataContext();
    }
}
