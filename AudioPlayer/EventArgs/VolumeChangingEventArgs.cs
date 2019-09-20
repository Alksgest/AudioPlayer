using System;

namespace AudioPlayer.CustomEventArgs
{
    public class VolumeChangingEventArgs : EventArgs
    {
        public Single Volume { get; set; }
    }
}