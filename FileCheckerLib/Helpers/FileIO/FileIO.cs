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
    public class FileIO : IFileIO
    {
        /// <summary>
        /// Возвращает содержимое файла
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <returns>Содержимое файла</returns>
        public string GetFileContent(string filePath)
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
        public List<string> GetStringList(string filePath)
        {
            List<string> output = new List<string>();

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
        public void DeleteFiles(List<FileRecordModel> fileRecords)
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

                            GlobalHelper.Logger.Add(StandardMessages.File.FileIsDeleted + fileRecord.Path);
                        }

                        catch (Exception ex)
                        {
                            GlobalHelper.Logger.Add(StandardMessages.File.ExceptionWhenDeleting + fileRecord.Path);
                        }
                    }
                    else
                    {
                        GlobalHelper.Logger.Add(StandardMessages.File.FileToDeleteDoesntExist + fileRecord.Path);
                    }
                }
            }
            else
            {
                GlobalHelper.Logger.Add(StandardMessages.File.NoFilesToDelete);
            }
        }


        /// <summary>
        /// Обновляет лог-файл отчетом об отправке e-mail
        /// </summary>
        /// <param name="filePath">Путь сохраненного лог-файла</param>
        /// <param name="report">Отчето об отправке e-mail</param>
        public void UpDateFile(string filePath, string report)
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
        public string GetQueryFromFile(string queryFileName)
        {
            // путь к SQL-файлу
            string queryFilePath = Path.Combine(GlobalConfig.QueryFolderPath, queryFileName);

            return GetFileContent(queryFilePath);
        }
    }
}