using FileCheckerLib.Delegates;
using FileCheckerLib.Models;
using System;
using System.Collections.Generic;

namespace FileCheckerLib.Helpers
{
    /// <summary>
    /// Обработчик исключений
    /// </summary>
    public static class ExceptionHandler
    {
        /// <summary>
        /// Метод-обертка конструкции try/catch для работы с БД
        /// </summary>
        /// <param name="del"></param>
        /// <param name="query">Sql-запрос</param>
        /// <param name="addErrorMessage">Дополнительное сообщение об ошибке</param>
        /// <returns></returns>
        public static List<ChildTableModel> TryCatchShell(this GetChildTablesDel del, string query, string addErrorMessage)
        {
            List<ChildTableModel> output = new List<ChildTableModel>();
            try
            {
                output = del?.Invoke(query);
            }

            catch(Exception ex)
            {
                Logger.Add(StandardMessages.Db.DbError);

                Logger.Add(addErrorMessage);
            }

            return output;
        }

        /// <summary>
        /// Метод-обертка конструкции try/catch для работы с БД
        /// </summary>
        /// <param name="del"></param>
        /// <param name="query">Sql-запрос</param>
        /// <param name="addErrorMessage">Дополнительное сообщение об ошибке</param>
        /// <returns></returns>
        public static List<FileRecordModel> TryCatchShell(this GetFileRecordDel del, string query, string addErrorMessage)
        {
            List<FileRecordModel> output = new List<FileRecordModel>();
            try
            {
                output = del?.Invoke(query);
            }

            catch (Exception ex)
            {
                Logger.Add(StandardMessages.Db.DbError);

                Logger.Add(addErrorMessage);
            }

            return output;
        }


        /// <summary>
        /// Метод-обертка конструкции try/catch для работы с БД
        /// </summary>
        /// <param name="del"></param>
        /// <param name="query">Sql-запрос</param>
        /// <param name="addErrorMessage">Дополнительное сообщение об ошибке</param>
        public static void TryCatchShell(this VoidDel del, string query, string addErrorMessage)
        {
            List<string> output = new List<string>();
            try
            {
                del?.Invoke(query);
            }

            catch (Exception ex)
            {
                Logger.Add(StandardMessages.Db.DbError);

                Logger.Add(addErrorMessage);
            }
        }

        /// <summary>
        /// Метод-обертка конструкции try/catch для работы с БД
        /// </summary>
        /// <param name="del"></param>
        /// <param name="query">Sql-запрос</param>
        /// <param name="addErrorMessage">Дополнительное сообщение об ошибке</param>
        public static string TryCatchShell(this StringDel del, string query, string addErrorMessage)
        {
            string output = "";
            try
            {
                output = del?.Invoke(query);
            }

            catch (Exception ex)
            {
                Logger.Add(StandardMessages.Db.DbError);

                Logger.Add(addErrorMessage);
            }

            return output;
        }
    }
}
