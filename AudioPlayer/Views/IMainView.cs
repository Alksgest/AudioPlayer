using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using AudioPlayer.Models;

namespace AudioPlayer.Views
{
    public interface IMainView : IView
    {
        event EventHandler LoadFiles;
        event EventHandler<PathHolderEventArgs> ChangeAudio;
        event EventHandler<VolumeChangingEventArgs> VolumeChanging;
    }

    public class VolumeChangingEventArgs : EventArgs
    {
        public Single Volume { get; set; }
    }

    public class PathHolderEventArgs : EventArgs
    {
        public PathHolder PathHolder { get; set; }
    }
}