using System.Collections.Generic;
using System.Text;
using FileCheckerLib.Models;

namespace FileCheckerLib.DataAccess
{
    /// <summary>
    /// Построитель запросов для MS SQL
    /// </summary>
    public class MsQueryBuilder : IQueryBuilder
    {
        /// <summary>
        /// Возвращает запрос на получение списка путей файлов подлежащих удалению
        /// </summary>
        /// <param name="childTables">Список связанных/дочерних таблиц</param>
        /// <returns>Запрос на получение списка путей файлов подлежащих удалению</returns>
        public string GetQueryGetPathsToDelete(List<ChildTableModel> childTables, string filePkColumnName)
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("SELECT [" + filePkColumnName + "], [Path]");
            output.AppendLine("FROM [File]");
            output.AppendLine("WHERE 1=1");

            if (childTables.Count > 0)
            {
                foreach (ChildTableModel table in childTables)
                {
                    output.AppendLine("AND [" + table.ParentID + "] NOT IN (SELECT [" + table.ForeigID + "] FROM [" + table.TableName + "])");
                }
            }

            return output.ToString();
        }

        /// <summary>
        /// Возвращает запрос на удаление записей в БД согласно списку путей файлов
        /// </summary>
        /// <param name="fileRecords">Список путей файлов, подлежащих удалению</param>
        /// <param name="filePkColumnName">Колонка первичного ключа</param>
        /// <returns>Запрос на удаление записей в БД согласно списку путей файлов</returns>
        public string GetQueryToDeleteFilePaths(List<FileRecordModel> fileRecords, string filePkColumnName)
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("DELETE FROM [File]");
            output.AppendLine("WHERE 1=2");

            foreach (var fileRecord in fileRecords)
            {
                output.AppendLine("OR [" + filePkColumnName + "] = " + fileRecord.PkValue);
            }

            return output.ToString();
        }
    }
}
