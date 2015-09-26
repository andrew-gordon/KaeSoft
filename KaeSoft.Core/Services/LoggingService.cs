using System;
using System.Diagnostics.CodeAnalysis;
using KaeSoft.Core.Interfaces;
using KaeSoft.Core.Properties;
using log4net;

namespace KaeSoft.Core.Services
{
    [ExcludeFromCodeCoverage, UsedImplicitly]
    public class LoggingService : ILoggingService
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public LoggingService()
        {
            log4net.Config.XmlConfigurator.Configure();    
        }

        public void Debug(string text)
        {
            Log.Debug(text);
        }

        public void Info(string text)
        {
            Log.Info(text);
        }

        public void Error(Exception ex)
        {
            Log.Error("", ex);
        }

        public void Error(string text, Exception ex)
        {
            Log.Error(text, ex);
        }

        public void Warn(string text)
        {
            Log.Warn(text);
        }
    }
}