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
        /// Создает лог=файл и возвращает путь к нему
        /// </summary>
        /// <param name="logFileFolder">Папка с логами</param>
        /// <returns>Возвращает путь к лог-файлу</returns>
        public string SaveLog()
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