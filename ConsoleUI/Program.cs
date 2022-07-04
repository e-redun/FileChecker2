using FileCheckerLib.Helpers;
using FileCheckerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            // инициализация глобального хэлпера
            GlobalHelper.Initialize();


            ILogicHelper logicHelper = new LogicHelper();

            //получение настроек приложения
            IAppSettings appSettings = logicHelper.GetAppSettings();

            //валидация настроек приложения
            string validationResults = logicHelper.ValidateAppSettings3(appSettings);

            if (validationResults.Length > 0)
            {
                Console.WriteLine(StandardMessages.Validation.NotValidSettings); Console.WriteLine();

                Console.WriteLine(validationResults);

                Console.ReadKey();

                Environment.Exit(0);
            }


            // инициализация соединения с БД
            logicHelper.InitializeConnection(appSettings.DbType);

            
            // получение списка связанных/"дочерних" таблиц
            List<ChildTableModel> childTables = logicHelper.GetChildTables();


            // первичный ключ таблицы File
            string filePkColumnName = logicHelper.GetFilePkColumnName();


            // получение списка удаляемых записей
            List<FileRecordModel> recordsToDelete = logicHelper.GetRecordsToDelete(childTables, filePkColumnName);


            // удаление записей в БД
            logicHelper.DeletePathsInDb(recordsToDelete, filePkColumnName);


            // устранение дублирования по полю Path
            logicHelper.DistinctRecords(recordsToDelete);


            // удаление файлов
            GlobalHelper.FileIO.DeleteFiles(recordsToDelete);
            

            // получение списка получателей лог-файла
            List<string> logReceivers = logicHelper.GetLogReceivers(appSettings.ReceiversFilePath);


            // валидация e-mail адресов
            logReceivers = GlobalHelper.Validator.ValidateEmails(logReceivers);


            // сохранение лог-файла
            string logFilePath = GlobalHelper.Logger.SaveLog();


            // отправка лог-файла получателям
            // получение отчета об отправке
            string emailingReport = logicHelper.EmailLog(logReceivers, logFilePath);


            // дополнение сохраненного лог-файла отчетом об отправке
            GlobalHelper.FileIO.UpDateFile(logFilePath, emailingReport);


            // вывод лог-файла на экран
            string report = GlobalHelper.FileIO.GetFileContent(logFilePath);

            Console.WriteLine(report);

            Console.WriteLine();
            Console.WriteLine("Обработка закончена");
            Console.ReadKey();
        }
    }
}