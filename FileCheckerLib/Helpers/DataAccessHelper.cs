using FileCheckerLib.Models;
using System.Collections.Generic;
using System.Data.Common;

namespace FileCheckerLib.Helpers
{
    /// <summary>
    /// Вспомогательный класс доступа к данным
    /// </summary>
    public static class DataAccessHelper
    {
        /// <summary>
        /// Возвращает из ответа СУБД список строк данных
        /// </summary>
        /// <param name="dataReader">Объект DbDataReader</param>
        /// <returns>Список строк данных</returns>
        public static List<FileRecordModel> GetData(this DbDataReader dataReader)
        {
            List<FileRecordModel> output = new List<FileRecordModel>();

            if (dataReader.HasRows) // если есть данные
            {
                while (dataReader.Read()) // построчно считываем данные
                {
                    FileRecordModel fileRecord = new FileRecordModel()
                    {
                        PkValue = int.Parse(dataReader.GetValue(0).ToString()),
                        Path = dataReader.GetValue(1).ToString()
                    };

                    output.Add(fileRecord);
                }
            }

            return output;
        }

        /// <summary>
        /// Возвращает из DbDataReader список связанных/дочерних таблиц
        /// </summary>
        /// <param name="dataReader">Объект DbDataReader</param>
        /// <returns>Список связанных/дочерних таблиц</returns>
        public static List<ChildTableModel> GetTables(this DbDataReader dataReader)
        {
            List<ChildTableModel> output = new List<ChildTableModel>();

            if (dataReader.HasRows) // если есть данные
            {
                while (dataReader.Read()) // построчно считываем данные
                {
                    ChildTableModel connectedTable = new ChildTableModel()
                    {
                        TableName = dataReader.GetValue(0).ToString(),
                        ForeigID = dataReader.GetValue(1).ToString(),
                        ParentTableName = dataReader.GetValue(2).ToString(),
                        ParentID = dataReader.GetValue(3).ToString()
                    };

                    output.Add(connectedTable);
                }
            }

            return output;
        }
    }
}