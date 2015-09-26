﻿using System;

namespace KaeSoft.Core.Interfaces
{
    public interface ILoggingService
    {
        void Debug(string text);
        void Info(string text);
        void Error(string text, Exception ex);
        void Error(Exception ex);
        void Warn(string text);
    }
}