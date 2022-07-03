using FileCheckerLib.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCheckerLib.Models
{
    public class AppSettingsModel : IAppSettings
    {
        /// <summary>
        /// Папка сохранения лог-файлов (относитительный путь)
        /// </summary>
        public string LogFolder { get; set; }

        /// <summary>
        /// Папка Other (относитительный путь)
        /// </summary>
        public string OtherFolder { get; set; }

        /// <summary>
        /// Файл получателей лога (имя с расширением)
        /// </summary>
        public string ReceiversFile { get; set; }


        /// <summary>
        /// Путь файла получателей лога
        /// </summary>
        public string ReceiversFilePath
        {
            get
            {
                return Path.Combine(OtherFolder, ReceiversFile);
            }
        }

        /// <summary>
        /// e-mail отправителя лога
        /// </summary>
        public string SenderEmail { get; set; }

        /// <summary>
        /// Стоковое представление типа используемой БД
        /// </summary>
        public string StrDbType { get; set; }

        /// <summary>
        /// Тип используемой БД
        /// </summary>
        public DbTypes DbType 
        {
            get
            {
                return (DbTypes)Enum.Parse(typeof(DbTypes), StrDbType);
            }
        }

    }
}
