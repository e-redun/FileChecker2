using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FileCheckerLib.Enums;
using FileCheckerLib.Models;

namespace FileCheckerLib.Helpers
{
    public interface ILogicHelper
    {
        void DeletePathsInDb(List<FileRecordModel> idPaths, string filePkColumnName);
        void DistinctRecords(List<FileRecordModel> recordsToDelete);
        string EmailLog(List<string> logReceivers, string logFilePath);
        List<ChildTableModel> GetChildTables();
        string GetFilePkColumnName();
        AppSettingsModel GetAppSettings();
        List<string> GetLogReceivers(string filePath);
        List<FileRecordModel> GetRecordsToDelete(List<ChildTableModel> childTables, string filePkColumnName);
        void InitializeConnection(DbTypes dbType);
        string  ValidateAppSettings3(IAppSettings settings);
    }
}