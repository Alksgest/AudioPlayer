using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioPlayer.Views;

namespace AudioPlayer.Presenters
{
    public abstract class Presenter<T> where T : IView
    {
        protected T View;
        protected Presenter(T view)
        {
            View = view;
            View.Initialize += Initialize;
        }

        protected virtual void Initialize(object sender, EventArgs args) { }
    }
}
