using FileCheckerLib.Delegates;
using FileCheckerLib.Enums;
using FileCheckerLib.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace FileCheckerLib.Helpers
{
    /// <summary>
    /// Логика FileChecker-а
    /// </summary>
    public static class FileCheckerLogic
    {
        /// <summary>
        /// Инициализирует лог
        /// </summary>
        public static void InitializeLog()
        {
            Logger.Add("Лог FileChecker"); Logger.Add("");

            string dbTypesToUse = GlobalConfig.GetAppSettingsByKey("DbTypeToUse");

            Logger.Add("Тип СУБД: " + dbTypesToUse); Logger.Add("");
        }


        /// <summary>
        /// Инициализирует соединение с БД
        /// </summary>
        public static void InitializeConnection()
        {
            string dbTypesToUse = GlobalConfig.GetAppSettingsByKey("DbTypeToUse");

            if (dbTypesToUse == DbTypes.MsSql.ToString())
            {
                GlobalConfig.InitializeConnection(DbTypes.MsSql);
            };

            if (dbTypesToUse == DbTypes.PostgreSql.ToString())
            {
                GlobalConfig.InitializeConnection(DbTypes.PostgreSql);
            };
        }


        /// <summary>
        /// Возвращает различающиеся по полю Path элементы списка
        /// </summary>
        /// <param name="recordsToDelete">Список элементов</param>
        public static void DistinctRecords(List<FileRecordModel> recordsToDelete)
        {
            // группировка по полю Path (устранение дублирования)
            recordsToDelete = recordsToDelete.GroupBy(p => p.Path)
                                             .Select(p => p.First())
                                             .ToList();


            // удаление элементов списка с пустым полем Path
            recordsToDelete = recordsToDelete.Where(p => !string.IsNullOrWhiteSpace(p.Path)).ToList();
        }

        /// <summary>
        /// Возвращает первичный ключ таблицы File
        /// </summary>
        /// <returns>Первичный ключ таблицы File</returns>
        public static string GetFilePkColumnName()
        {
            // запрос
            string query = FileHelper.GetQueryFromFile("GetFilePkColumnName.sql");

            // делегат
            StringDel GetFilePkColumnName = GlobalConfig.Connection.GetFilePkColumnName;

            // доп. сообщение об ошибке
            string addErrorMessage = StandardMessages.Db.GetChildTablesError;

            // try-catch-оболочка
            return GetFilePkColumnName.TryCatchShell(query, addErrorMessage);
        }


        /// <summary>
        /// Возвращает список связанных/дочерних таблиц
        /// </summary>
        /// <returns>Список связанных/дочерних таблиц</returns>
        public static List<ChildTableModel> GetChildTables()
        {
            // запрос
            string query = FileHelper.GetQueryFromFile("GetChildTables.sql");

            // делегат
            GetChildTablesDel GetChildTables = GlobalConfig.Connection.GetChildTables;

            // доп. сообщение об ошибке
            string addErrorMessage = StandardMessages.Db.GetChildTablesError;

            // try-catch-оболочка
            return GetChildTables.TryCatchShell(query, addErrorMessage);
        }

        /// <summary>
        /// Возвращает список путей к файлам подлежащим удалению
        /// </summary>
        /// <param name="childTables">Список связанных/дочерних таблиц</param>
        /// <returns>Cписок путей к файлам подлежащим удалению</returns>
        public static List<FileRecordModel> GetRecordsToDelete(List<ChildTableModel> childTables, string filePkColumnName)
        {
            // запрос
            string query = GlobalConfig.QueryBuilder.GetQueryGetPathsToDelete(childTables, filePkColumnName);

            // делегат
            GetFileRecordDel GetFileRecords = GlobalConfig.Connection.GetPathsToDelete;

            // доп. сообщение об ошибке
            string addErrorMessage = StandardMessages.Db.GetPathsToDeleteError;

            // try/catch-оболочка
            return GetFileRecords.TryCatchShell(query, addErrorMessage);
        }


        /// <summary>
        /// Удаляет записи в таблице File БД 
        /// </summary>
        /// <param name="idPaths">Cписок путей к файлам подлежащим удалению</param>
        public static void DeletePathsInDb(List<FileRecordModel> idPaths, string filePkColumnName)
        {
            if (idPaths.Count > 0)
            {
                // запрос
                string query = GlobalConfig.QueryBuilder.GetQueryToDeleteFilePaths(idPaths, filePkColumnName);

                // делегат
                VoidDel DeleteFilePaths = GlobalConfig.Connection.DeleteFilePaths;

                // доп. сообщение об ошибке
                string addErrorMessage = StandardMessages.Db.DeleteRecordsError;

                // оболочка
                DeleteFilePaths.TryCatchShell(query, addErrorMessage);
            }
        }


        /// <summary>
        /// Возвращает список адресатов-получателей лог-файла
        /// </summary>
        /// <returns>Список адресатов-получателей лог-файла</returns>
        public static List<string> GetLogReceivers()
        {
            string filePath = Path.Combine(GlobalConfig.GetAppSettingsByKey("otherFolder"),
                                           GlobalConfig.GetAppSettingsByKey("receiversFile"));

            return FileHelper.GetStringList(filePath);
        }


        /// <summary>
        /// Отправляет лог-файл и возвращает Отчет об отправке
        /// </summary>
        /// <param name="logReceivers">Список адресатов</param>
        /// <param name="logFilePath">Путь к лог-файлу</param>
        /// <returns>Отчет об отправке</returns>
        public static string EmailLog(List<string> logReceivers, string logFilePath)
        {
            string output;

            if (logReceivers.Count > 0)
            {
                // получение отчета по отправке
                output = logReceivers.SendEmail(logFilePath);
            }
            else
            {
                // получение отчета по отправке
                output = StandardMessages.Validation.ReceiverListIsEmpty;
            }

            return output;
        }

    }
}
