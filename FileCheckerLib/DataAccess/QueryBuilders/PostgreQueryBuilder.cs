using System.Collections.Generic;
using System.Text;
using FileCheckerLib.Models;

namespace FileCheckerLib.DataAccess
{
    public class PostgreQueryBuilder : IQueryBuilder
    {
        /// <summary>
        /// Возвращает запрос на получение путей/Path файлов подлежащих удалению
        /// </summary>
        /// <param name="childTables">Дочерние таблицы от таблицы File</param>
        /// <returns>Запрос на получение путей/Path файлов подлежащих удалению</returns>
        public string GetQueryGetPathsToDelete(List<ChildTableModel> childTables)
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("SELECT Path");
            output.AppendLine("FROM File");
            output.AppendLine("WHERE 1=1");

            if (childTables.Count > 0)
            {
                foreach (ChildTableModel table in childTables)
                {
                    output.AppendLine("AND " + table.ParentID + " NOT IN (SELECT " + table.ForeigID + " FROM " + table.TableName + ")");
                }
            }

            return output.ToString();
        }

        /// <summary>
        /// Возвращает запрос на получение списка путей файлов подлежащих удалению
        /// </summary>
        /// <param name="childTables">Список связанных/дочерних таблиц</param>
        /// <returns>Запрос на получение списка путей файлов подлежащих удалению</returns>
        public string GetQueryGetPathsToDelete(List<ChildTableModel> childTables, string filePkColumnName)
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("SELECT " + filePkColumnName + ", path");
            output.AppendLine("FROM file");
            output.AppendLine("WHERE 1=1");

            if (childTables.Count > 0)
            {
                foreach (ChildTableModel table in childTables)
                {
                    output.AppendLine("AND " + table.ParentID + " NOT IN (SELECT " + table.ForeigID + " FROM " + table.TableName + ")");
                }
            }

            return output.ToString();
        }

        /// <summary>
        /// Возвращает запрос на удаление записей в таблице File
        /// </summary>
        /// <param name="fileRecords">Список путей/Path в записях File, подлежащих удалению</param>
        /// <param name="filePkColumnName">Колонка первичного ключа</param>
        /// <returns>Запрос на удаление записей в таблице File</returns>
        public string GetQueryToDeleteFilePaths(List<FileRecordModel> fileRecords, string filePkColumnName)
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("DELETE FROM file");
            output.AppendLine("WHERE 1=2");

            foreach (var fileRecord in fileRecords)
            {
                output.AppendLine("OR " + filePkColumnName + " = " + fileRecord.PkValue);
            }

            return output.ToString();
        }
    }
}
