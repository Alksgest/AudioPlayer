using System;
using AudioPlayer.Models;

namespace AudioPlayer.CustomEventArgs
{
    public class PathHolderEventArgs : EventArgs
    {
        public PathHolder PathHolder { get; set; }
    }
}