using FileCheckerLib.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace FileCheckerLib.Helpers
{
    /// <summary>
    /// Валидатор
    /// </summary>
    public class Validator : IValidator
    {

        /// <summary>
        /// Валидирует папку
        /// </summary>
        /// <param name="folderPath">Путь папки</param>
        /// <returns>True, если папка существует</returns>
        public bool ValidateFolder(string folderPath)
        {
            return Directory.Exists(folderPath);
        }


        /// <summary>
        /// Валидирует файл получателей лога
        /// </summary>
        /// <returns>True, если файл существует</returns>
        public bool ValidateLogReceiversFile(string filePath)
        {
            return File.Exists(filePath);
        }


        /// <summary>
        /// Валидирует e-mail
        /// </summary>
        /// <param name="email">e-mail</param>
        public bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email, @".+@.+\..+") &&
                   !Regex.IsMatch(email, @"\s");
        }

        /// <summary>
        /// Валидирует тип базы данных
        /// </summary>
        public bool ValidateDbType(string strDbType)
        {
            return  Enum.TryParse(strDbType, out DbTypes asd);
        }


        /// <summary>
        /// Валидирует список e-mail
        /// </summary>
        /// <param name="emails">Cписок e-mail</param>
        public List<string> ValidateEmails(List<string> emails)
        {
            List<string> output = new List<string>();

            foreach (string email in emails)
            {
                bool valid = ValidateEmail(email);

                if (valid == true)
                {
                    output.Add(email);
                }
                else
                {
                    GlobalHelper.Logger.Add(email + StandardMessages.Validation.NotValidEmail);
                }
            }

            // пустая строка разделитель
            GlobalHelper.Logger.Add("");

            return output;
        }
    }
}
