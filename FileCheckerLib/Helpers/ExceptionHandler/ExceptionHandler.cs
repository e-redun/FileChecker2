using FileCheckerLib.Delegates;
using FileCheckerLib.Models;
using System;
using System.Collections.Generic;

namespace FileCheckerLib.Helpers
{
    /// <summary>
    /// Обработчик исключений
    /// </summary>
    public class ExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// Метод-обертка конструкции try/catch для работы с БД
        /// </summary>
        /// <param name="del">Делегат-исполняемый метод</param>
        /// <param name="query">Sql-запрос</param>
        /// <param name="addErrorMessage">Дополнительное сообщение об ошибке</param>
        /// <returns></returns>
        public List<ChildTableModel> TryCatchShell(GetChildTablesDel del, string query, string addErrorMessage)
        {
            List<ChildTableModel> output = new List<ChildTableModel>();

            try
            {
                output = del?.Invoke(query);
            }

            catch(Exception ex)
            {
                GlobalHelper.Logger.Add(StandardMessages.Db.DbError);

                GlobalHelper.Logger.Add(addErrorMessage);
            }

            return output;
        }

        /// <summary>
        /// Метод-обертка конструкции try/catch для работы с БД
        /// </summary>
        /// <param name="del">Делегат-исполняемый метод</param>
        /// <param name="query">Sql-запрос</param>
        /// <param name="addErrorMessage">Дополнительное сообщение об ошибке</param>
        /// <returns></returns>
        public List<FileRecordModel> TryCatchShell(GetFileRecordDel del, string query, string addErrorMessage)
        {
            List<FileRecordModel> output = new List<FileRecordModel>();

            try
            {
                output = del?.Invoke(query);
            }
            catch (Exception ex)
            {
                GlobalHelper.Logger.Add(StandardMessages.Db.DbError);

                GlobalHelper.Logger.Add(addErrorMessage);
            }

            return output;
        }


        /// <summary>
        /// Метод-обертка конструкции try/catch для работы с БД
        /// </summary>
        /// <param name="del">Делегат-исполняемый метод</param>
        /// <param name="query">Sql-запрос</param>
        /// <param name="addErrorMessage">Дополнительное сообщение об ошибке</param>
        public void TryCatchShell(VoidDel del, string query, string addErrorMessage)
        {
            List<string> output = new List<string>();

            try
            {
                del?.Invoke(query);
            }
            catch (Exception ex)
            {
                GlobalHelper.Logger.Add(StandardMessages.Db.DbError);

                GlobalHelper.Logger.Add(addErrorMessage);
            }
        }

        /// <summary>
        /// Метод-обертка конструкции try/catch для работы с БД
        /// </summary>
        /// <param name="del">Делегат-исполняемый метод</param>
        /// <param name="query">Sql-запрос</param>
        /// <param name="addErrorMessage">Дополнительное сообщение об ошибке</param>
        public string TryCatchShell(StringDel del, string query, string addErrorMessage)
        {
            string output = "";
            try
            {
                output = del?.Invoke(query);
            }

            catch (Exception ex)
            {
                GlobalHelper.Logger.Add(StandardMessages.Db.DbError);

                GlobalHelper.Logger.Add(addErrorMessage);
            }

            return output;
        }
    }
}