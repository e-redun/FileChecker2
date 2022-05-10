using FileCheckerLib.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FileCheckerLib.Helpers
{
    /// <summary>
    /// Валидатор
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Валидирует список e-mail
        /// </summary>
        /// <param name="emails">Cписок e-mail</param>
        /// <param name="logger">Логгер</param>
        public static List<string> ValidateEmails(List<string> emails)
        {
            List<string> output = new List<string>();

            foreach (string email in emails)
            {
                bool valid = Regex.IsMatch(email, @".+@.+\..+");

                if (valid == true)
                {
                    output.Add(email);
                }
                else
                {
                    Logger.Add(email + StandardMessages.Validation.NotValidEmail);
                }
            }

            // пустая строка разделитель
            Logger.Add("");

            return output;
        }
    }
}
