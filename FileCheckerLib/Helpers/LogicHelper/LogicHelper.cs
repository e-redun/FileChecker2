using FileCheckerLib.Delegates;
using FileCheckerLib.Enums;
using FileCheckerLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace FileCheckerLib.Helpers
{
    public class LogicHelper : ILogicHelper
    {
        /// <summary>
        /// Инициализирует соединение с БД
        /// </summary>
        public void InitializeConnection()
        {
            string dbTypesToUse = GlobalConfig.GetAppSettingsByKey("DbTypeToUse");

            DbTypes dbType = (DbTypes)Enum.Parse(typeof(DbTypes), dbTypesToUse);

            GlobalConfig.InitializeConnection(dbType);
        }


        /// <summary>
        /// Возвращает настройки приложения
        /// </summary>
        /// <returns>Настройки приложения</returns>
        public AppSettingsModel GetAppSettings()
        {
            AppSettingsModel output = new AppSettingsModel();

            output.LogFolder = GlobalConfig.GetAppSettingsByKey("logFolder");

            output.OtherFolder = GlobalConfig.GetAppSettingsByKey("otherFolder");

            output.ReceiversFile = GlobalConfig.GetAppSettingsByKey("receiversFile");

            output.SenderEmail = GlobalConfig.GetAppSettingsByKey("senderEmail");

            output.StrDbType = GlobalConfig.GetAppSettingsByKey("DbTypeToUse");

            return output;
        }


        /// </summary>
        /// Валидирует настройки программы
        /// </summary>
        /// <param name="settings">Настройки приложения</param>
        /// <returns>Результат валидации - Список описаний ошибок</returns>
        public string ValidateAppSettings3(IAppSettings settings)
        {
            StringBuilder output = new StringBuilder();

            // Лог-папка
            if (!GlobalHelper.Validator.ValidateFolder(settings.LogFolder))
            {
                output.AppendLine("Папка сохранения лог-файлов невалидная.");
            }

            // папка Other
            if (!GlobalHelper.Validator.ValidateFolder(settings.OtherFolder))
            {
                output.AppendLine("Папка Other невалидная.");
            }

            // файл получателей лога
            if (!GlobalHelper.Validator.ValidateLogReceiversFile(settings.ReceiversFilePath))
            {
                output.AppendLine("Путь к файлу получателей лог-файла невалидный.");
            }

            // email отправителя
            if (!GlobalHelper.Validator.ValidateEmail(settings.SenderEmail))
            {
                output.AppendLine("E-mail отправителя невалидный.");
            }

            // тип базы данных
            if (!GlobalHelper.Validator.ValidateDbType(settings.StrDbType))
            {
                output.AppendLine("Тип БД невалидный.");
            }

            return output.ToString();
        }


        /// <summary>
        /// Возвращает различающиеся по полю Path элементы списка
        /// </summary>
        /// <param name="recordsToDelete">Список элементов</param>
        public void DistinctRecords(List<FileRecordModel> recordsToDelete)
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
        public string GetFilePkColumnName()
        {
            // делегат
            StringDel getFilePkColumnName = GlobalConfig.Connection.GetFilePkColumnName;
            
            // запрос
            string query = GlobalHelper.FileIO.GetQueryFromFile("GetFilePkColumnName.sql");

            // доп. сообщение об ошибке
            string addErrorMessage = StandardMessages.Db.GetChildTablesError;

            // try-catch-оболочка
            return GlobalHelper.ExceptionHandler.TryCatchShell(getFilePkColumnName, query, addErrorMessage);
        }


        /// <summary>
        /// Возвращает список связанных/дочерних таблиц
        /// </summary>
        /// <returns>Список связанных/дочерних таблиц</returns>
        public List<ChildTableModel> GetChildTables()
        {
            // делегат
            GetChildTablesDel getChildTables = GlobalConfig.Connection.GetChildTables;
            
            // запрос
            string query = GlobalHelper.FileIO.GetQueryFromFile("GetChildTables.sql");

            // доп. сообщение об ошибке
            string addErrorMessage = StandardMessages.Db.GetChildTablesError;

            // try-catch-оболочка
            return GlobalHelper.ExceptionHandler.TryCatchShell(getChildTables, query, addErrorMessage);
        }

        /// <summary>
        /// Возвращает список путей к файлам подлежащим удалению
        /// </summary>
        /// <param name="childTables">Список связанных/дочерних таблиц</param>
        /// <returns>Cписок путей к файлам подлежащим удалению</returns>
        public List<FileRecordModel> GetRecordsToDelete(List<ChildTableModel> childTables, string filePkColumnName)
        {
            // делегат
            GetFileRecordDel getFileRecords = GlobalConfig.Connection.GetPathsToDelete;
            
            // запрос
            string query = GlobalConfig.QueryBuilder.GetQueryGetPathsToDelete(childTables, filePkColumnName);

            // доп. сообщение об ошибке
            string addErrorMessage = StandardMessages.Db.GetPathsToDeleteError;

            // try/catch-оболочка
            return GlobalHelper.ExceptionHandler.TryCatchShell(getFileRecords, query, addErrorMessage);
        }


        /// <summary>
        /// Удаляет записи в таблице File БД 
        /// </summary>
        /// <param name="idPaths">Cписок путей к файлам подлежащим удалению</param>
        public void DeletePathsInDb(List<FileRecordModel> idPaths, string filePkColumnName)
        {
            if (idPaths.Count > 0)
            {
                // делегат
                VoidDel deleteFilePaths = GlobalConfig.Connection.DeleteFilePaths;
                
                // запрос
                string query = GlobalConfig.QueryBuilder.GetQueryToDeleteFilePaths(idPaths, filePkColumnName);

                // доп. сообщение об ошибке
                string addErrorMessage = StandardMessages.Db.DeleteRecordsError;

                // оболочка
                GlobalHelper.ExceptionHandler.TryCatchShell(deleteFilePaths, query, addErrorMessage);
            }
        }


        /// <summary>
        /// Возвращает список адресатов-получателей лог-файла
        /// </summary>
        /// <returns>Список адресатов-получателей лог-файла</returns>
        public List<string> GetLogReceivers(string filePath)
        {
            return GlobalHelper.FileIO.GetStringList(filePath);
        }


        /// <summary>
        /// Отправляет лог-файл и возвращает Отчет об отправке
        /// </summary>
        /// <param name="logReceivers">Список адресатов</param>
        /// <param name="logFilePath">Путь к лог-файлу</param>
        /// <returns>Отчет об отправке</returns>
        public string EmailLog(List<string> logReceivers, string logFilePath)
        {
            string output;

            if (logReceivers.Count > 0)
            {
                // получение отчета по отправке
                output = GlobalHelper.Emailer.SendEmail(logReceivers, logFilePath);
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
