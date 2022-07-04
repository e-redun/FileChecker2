namespace FileCheckerLib.Helpers
{
    public interface ILogger
    {
        void Add(string record);
        string SaveLog(string logFolderPath);
    }
}