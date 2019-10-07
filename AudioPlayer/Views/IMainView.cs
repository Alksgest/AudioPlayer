using System;
using System.Windows.Controls;
using AudioPlayer.CustomEventArgs;

namespace AudioPlayer.Views
{
    public interface IMainView : IView
    {
        event EventHandler LoadFiles;
        event EventHandler<PathHolderEventArgs> ChangeAudio;
        event EventHandler<VolumeChangingEventArgs> VolumeChanging;

        DataGrid DataGrid { get; }
    }
}