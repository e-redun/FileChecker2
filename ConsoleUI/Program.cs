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
            // список связанных таблиц
            List<ChildTableModel> childTables;


            // список файлов подлежащих удалению
            List<FileRecordModel> recordsToDelete;


            // "инициализация" лога
            FileCheckerLogic.InitializeLog();
            
            
            // инициализация соединения с БД
            FileCheckerLogic.InitializeConnection();
            

            // получение списка связанных/"дочерних" таблиц
            childTables = FileCheckerLogic.GetChildTables();


            // первичный ключ таблицы File
            string filePkColumnName = FileCheckerLogic.GetFilePkColumnName();


            // получение списка удаляемых записей
            recordsToDelete = FileCheckerLogic.GetRecordsToDelete(childTables, filePkColumnName);
            

            // удаление записей в БД
            FileCheckerLogic.DeletePathsInDb(recordsToDelete, filePkColumnName);


            // устранение дублирования по полю Path
            FileCheckerLogic.DistinctRecords(recordsToDelete);


            // удаление файлов
            FileHelper.DeleteFiles(recordsToDelete);
            

            // получение списка получателей лог-файла
            List<string> logReceivers = FileCheckerLogic.GetLogReceivers();


            // валидация e-mail адресов
            logReceivers = Validator.ValidateEmails(logReceivers);


            // сохранение лог-файла
            string logFilePath = Logger.SaveLog();


            // отправка лог-файла получателям
            // получение отчета об отправке
            string emailingReport = FileCheckerLogic.EmailLog(logReceivers, logFilePath);


            // дополнение сохраненного лог-файла отчетом об отправке
            FileHelper.UpDateFile(logFilePath, emailingReport);


            // вывод лог-файла на экран
            string report = FileHelper.GetFileContent(logFilePath);
            Console.WriteLine(report);
            

            Console.WriteLine("Обработка закончена");
            Console.ReadKey();
        }
    }
}