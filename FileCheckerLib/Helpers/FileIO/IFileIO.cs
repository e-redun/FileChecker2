using System.Collections.Generic;
using FileCheckerLib.Models;

namespace FileCheckerLib.Helpers
{
    public interface IFileIO
    {
        void DeleteFiles(List<FileRecordModel> fileRecords);
        string GetFileContent(string filePath);
        string GetQueryFromFile(string queryFileName);
        List<string> GetStringList(string filePath);
        void UpDateFile(string filePath, string report);
    }
}