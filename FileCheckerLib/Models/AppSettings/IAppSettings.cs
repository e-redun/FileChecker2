using FileCheckerLib.Enums;

namespace FileCheckerLib.Models
{
    public interface IAppSettings
    {
        DbTypes DbType { get; }
        string LogFolder { get; set; }
        string OtherFolder { get; set; }
        string ReceiversFile { get; set; }
        string ReceiversFilePath { get; }
        string SenderEmail { get; set; }
        string StrDbType { get; set; }
    }
}