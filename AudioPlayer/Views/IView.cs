using System;

namespace AudioPlayer.Views
{
    public interface IView
    {
        event EventHandler Initialize;
    }
}