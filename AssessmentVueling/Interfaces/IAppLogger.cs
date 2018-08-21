using System;

namespace AssessmentVueling.Interfaces
{
    public interface IAppLogger
    {
        void Info(Exception ex, string message);

        void Warning(Exception ex, string message);

        void Error(Exception ex, string message);

        void Fatal(Exception ex, string message);

        void Trace(Exception ex, string message);

        void Debug(Exception ex, string message);
    }
}
