using System;
using System.Threading;
using KaeSoft.Core.Interfaces;

namespace KaeSoft.Core.Services
{
    public abstract class SubServiceBase : IStartable, IStoppable
    {
        
        private int _runState;
        private readonly ILoggingService _loggingService;
        private const int Stopped = 0;
        private const int Running = 1;

        protected SubServiceBase(ILoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        //public bool HasStarted { get; private set; }
        public bool IsDisposed { get; private set; }
        protected abstract void DoStart();
        protected abstract void DoStop();

        /// <summary>
        /// Starts the service either for the first time, or after being stopped.
        /// </summary>
        public void Start()
        {
            AssertNotDisposed();

            if (Interlocked.CompareExchange(ref _runState, Running, Stopped) == Stopped)
            {
                _loggingService.Info( GetType().Name + " starting");
                DoStart();
                //HasStarted = true;
                _loggingService.Info(GetType().Name + " started");
            }
            else
            {
                _loggingService.Info(GetType().Name + " is already started so ignoring start request");                
            }
        }

        /// <summary>
        /// Stops the service (but may be restarted using Start)
        /// </summary>
        public void Stop()
        {
            if (Interlocked.CompareExchange(ref _runState, Stopped, Running) == Running)
            {
                _loggingService.Info(GetType().Name + " stopping");
                DoStop();
                //HasStarted = true;
                _loggingService.Info(GetType().Name + " stopped");
            }
            else
            {
                _loggingService.Info(GetType().Name + " is not started so ignoring stop request");
            }
        }


        public bool IsStarted
        {
            get { return (_runState == Running); }
        }

        public void Dispose()
        {
            Stop();
            _loggingService.Info(GetType().Name + " is being disposed");
            DoDispose();
            _loggingService.Info(GetType().Name + " has been diisposed");
            IsDisposed = true;
        }

        /// <summary>
        /// Implement this to tear down resources or subscriptions that survive for the lifetime of the object.
        /// </summary>
        protected virtual void DoDispose()
        {
        }

        protected void AssertStarted()
        {
            if (!IsStarted)
                throw new InvalidOperationException(string.Format("{0} has not been started", GetType().Name));
        }

        protected void AssertNotDisposed()
        {
            if (IsDisposed)
                throw new InvalidOperationException(string.Format("{0} has been disposed", GetType().Name));
        }


    }
}
