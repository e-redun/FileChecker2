using System.Collections.Generic;

namespace FileCheckerLib.Helpers
{
    public interface IEmailer
    {
        string SendEmail(List<string> logReceivers, string logFilePath);
    }
}