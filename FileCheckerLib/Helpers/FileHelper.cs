using FileCheckerLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileCheckerLib.Helpers
{
    /// <summary>
    /// Помощник работы с файлами
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Возвращает содержимое файла
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <returns>Содержимое файла</returns>
        public static string GetFileContent(this string filePath)
        {
            string output = "";

            if (File.Exists(filePath))
            {
                output = File.ReadAllText(filePath);
            }

            return output;
        }

        /// <summary>
        /// Возвращает список строк текстового файла
        /// </summary>
        /// <param name="filePath">Путь файла</param>
        /// <returns>Список строк текстового файла</returns>
        public static List<string> GetStringList(this string filePath)
        {
            List<string> output = new List<string>();
            bool b= File.Exists(filePath);

            if (File.Exists(filePath))
            {
                output = File.ReadAllLines(filePath).ToList();
            }

            return output;
        }


        /// <summary>
        /// Удаляет список файлов
        /// </summary>
        /// <param name="filePaths">Список путей к файлам подлежащим удалению</param>
        /// <param name="logger">Логгер</param>
        public static void DeleteFiles(List<FileRecordModel> fileRecords)
        {
            if (fileRecords.Count > 0)
            {
                foreach (var fileRecord in fileRecords)
                {
                    if (File.Exists(fileRecord.Path))
                    {
                        try
                        {
                            File.Delete(fileRecord.Path);

                            Logger.Add(StandardMessages.File.FileIsDeleted + fileRecord.Path);
                        }

                        catch (Exception ex)
                        {
                            Logger.Add(StandardMessages.File.ExceptionWhenDeleting + fileRecord.Path);
                        }
                    }
                    else
                    {
                        Logger.Add(StandardMessages.File.FileToDeleteDoesntExist + fileRecord.Path);
                    }
                }
            }
            else
            {
                Logger.Add(StandardMessages.File.NoFilesToDelete);
            }
        }


        /// <summary>
        /// Обновляет лог-файл отчетом об отправке e-mail
        /// </summary>
        /// <param name="filePath">Путь сохраненного лог-файла</param>
        /// <param name="report">Отчето об отправке e-mail</param>
        public static void UpDateFile(string filePath, string report)
        {
            if (File.Exists(filePath))
            {
                File.AppendAllText(filePath, report);
            }
        }


        /// <summary>
        /// Возвращает Sql-запрос по имени файла
        /// </summary>
        /// <param name="queryFileName">Имя файла</param>
        /// <returns>Sql-запрос</returns>
        public static string GetQueryFromFile(string queryFileName)
        {
            // путь к SQL-файлу
            string queryFilePath = Path.Combine(GlobalConfig.QueryFolderPath, queryFileName);

            return queryFilePath.GetFileContent();
        }
    }
}