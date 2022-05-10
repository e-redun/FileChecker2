using System;
using System.IO;
using System.Text;

namespace FileCheckerLib.Models
{
    public static class Logger
    {
        private static StringBuilder log = new StringBuilder();

        /// <summary>
        /// Добавляет запись в лог
        /// </summary>
        /// <param name="record">Запись</param>
        public static void Add(string record)
        {
            log.AppendLine(record);
        }

        /// <summary>
        /// Создает лог=файл и возвращает путь к нему
        /// </summary>
        /// <param name="logFileFolder">Папка с логами</param>
        /// <returns>Возвращает путь к лог-файлу</returns>
        public static string SaveLog()
        {
            DateTime nowDateTime = DateTime.Now;

            // путь папки с лог-файлами
            string logFileFolder = GlobalConfig.GetAppSettingsByKey("logFolder");

            // имя лог-файла
            string logFileName = nowDateTime.ToString("yyyyMMddHHmmss") + "_FileCheckerLog.txt";

            // путь лог-файла
            string logFilePath = Path.Combine(logFileFolder, logFileName);

            if (!Directory.Exists(logFileFolder))
            {
                Directory.CreateDirectory(logFileFolder);
            }

            File.WriteAllText(logFilePath, log.ToString());

            return logFilePath;
        }
    }
}