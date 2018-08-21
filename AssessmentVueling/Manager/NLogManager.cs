using AssessmentVueling.Interfaces;
using NLog;
using System;
namespace AssessmentVueling.Manager
{
    public class NLogManager : IAppLogger
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public void Debug(Exception ex, string message)
        {
            logger.Debug(ex, message);            
        }

        public void Error(Exception ex, string message)
        {
            logger.Error(ex, message);
        }

        public void Fatal(Exception ex, string message)
        {
            logger.Fatal(ex, message);
        }

        public void Info(Exception ex, string message)
        {
            logger.Info(ex, message);
        }

        public void Trace(Exception ex, string message)
        {
            logger.Trace(ex, message);
        }

        public void Warning(Exception ex, string message)
        {
            logger.Warn(ex, message);
        }
    }
}