using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWorker.Interfaces
{
    public interface IAsyncPlayer
    {
        Task PlayAsync();
        Task StopAsync();
        Task PauseAsync();
    }
}
