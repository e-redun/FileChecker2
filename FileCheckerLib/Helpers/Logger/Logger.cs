using System;
using System.IO;
using System.Text;

namespace FileCheckerLib.Helpers
{
    public class Logger : ILogger
    {
        private StringBuilder log = new StringBuilder();

        public Logger()
        {
            Initialize();
        }

        /// <summary>
        /// Инициализирует логгер
        /// </summary>
        private void Initialize()
        {
            Add("Лог FileChecker");
            Add("");

            string dbTypesToUse = GlobalConfig.GetAppSettingsByKey("DbTypeToUse");

            Add("Тип СУБД: " + dbTypesToUse);
            Add("");
        }

        /// <summary>
        /// Добавляет запись в лог
        /// </summary>
        /// <param name="record">Запись</param>
        public void Add(string record)
        {
            log.AppendLine(record);
        }

        /// <summary>
        /// Создает лог-файл и возвращает путь к нему
        /// </summary>
        /// <param name="logFolderPath">Путь к лог-папке</param>
        /// <returns>Путь к лог-файлу</returns>
        public string SaveLog(string logFolderPath)
        {
            DateTime nowDateTime = DateTime.Now;

            // имя лог-файла
            string logFileName = nowDateTime.ToString("yyyyMMddHHmmss") + "_FileCheckerLog.txt";

            // путь лог-файла
            string logFilePath = Path.Combine(logFolderPath, logFileName);

            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            File.WriteAllText(logFilePath, log.ToString());

            return logFilePath;
        }
    }
}