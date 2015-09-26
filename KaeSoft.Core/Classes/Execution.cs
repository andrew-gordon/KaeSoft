using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using KaeSoft.Core.Properties;
using log4net;

namespace KaeSoft.Core.Classes
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public static class Execution
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Wait(TimeSpan waitPeriodIfDebuggerAttached, TimeSpan waitPeriodIfDebuggerNotAttached)
        {
            if (!Debugger.IsAttached)
            {
                Log.Info(string.Format("Pausing for {0}", waitPeriodIfDebuggerAttached));
                Thread.Sleep(waitPeriodIfDebuggerAttached);
            }
            else
            {
                if (waitPeriodIfDebuggerNotAttached != TimeSpan.Zero)
                {
                    Log.Info(string.Format("Pausing for {0}", waitPeriodIfDebuggerNotAttached));
                    Thread.Sleep(waitPeriodIfDebuggerNotAttached);
                }
            }
        }
    }
}
