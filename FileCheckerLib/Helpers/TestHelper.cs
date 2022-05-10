using FileCheckerLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileCheckerLib
{
    /// <summary>
    /// Логика работы программы
    /// </summary>
    public static class TestHelper
    {
        /// <summary>
        /// Возвращает путь к лог-папке
        /// </summary>
        /// <param name="logFolderName">Имя лог-папки</param>
        /// <returns>Путь к лог-папке</returns>
        public static string GetLogFolderPath(this string logFolderName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logFolderName);
        }



        public static string GetContent(this List<string> list)
        {
            StringBuilder output = new StringBuilder();

            foreach (var item in list)
            {
                output.AppendLine(item);
            }

            return output.ToString();
        }

        public static string GetContent(this List<ChildTableModel> list)
        {
            StringBuilder output = new StringBuilder();

            foreach (var item in list)
            {
                output.AppendLine(item.ToString());
            }

            return output.ToString();
        }
    }
}
