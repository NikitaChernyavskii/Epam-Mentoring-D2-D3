using System;

namespace Service.Contract
{
    internal interface IFileSystemWatcherService : IDisposable
    {
        void WatchFolder();
    }
}
