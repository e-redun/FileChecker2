using FileCheckerLib.Helpers;
using FileCheckerLib.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FileCheckerLib.DataAccess
{
    public class MsSqlConnector : IDataConnection
    {
        /// <summary>
        /// Имя строки подключния в файле App.config
        /// </summary>
        private const string db = "MsSql";

        /// <summary>
        /// Возвращает певичный ключ таблицы File
        /// </summary>
        /// <param name="query">Sql-запрос</param>
        public string GetFilePkColumnName(string query)
        {
            string output = "";

            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString(db)))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = query;

                command.Connection = (SqlConnection)connection;

                output = command.ExecuteScalar().ToString();

                connection.Close();
            }

            return output;
        }


        /// <summary>
        /// Возвращает из БД коллекцию путей файлов подлежащих удалению
        /// </summary>
        /// <param name="query">Запрос к БД</param>
        /// <returns>Коллекция путей файлов подлежащих удалению</returns>
        public List<FileRecordModel> GetPathsToDelete(string query)
        {
            List<FileRecordModel> output = new List<FileRecordModel>();

            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString(db)))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = query;

                command.Connection = (SqlConnection)connection;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    output = reader.GetData();
                }

                connection.Close();
            }

            return output;
        }

        /// <summary>
        /// Удаляет записи в таблице File БД
        /// </summary>
        /// <param name="query">Sql-запрос</param>
        public void DeleteFilePaths(string query)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString(db)))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = query;

                command.Connection = (SqlConnection)connection;

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        
        /// <summary>
        /// Возвращает список связанных/дочерних таблиц
        /// </summary>
        /// <param name="query">Sql-запрос</param>
        /// <returns>Список связанных/дочерних таблиц</returns>
        public List<ChildTableModel> GetChildTables(string query)
        {
            List<ChildTableModel> output = new List<ChildTableModel>();

            using (IDbConnection connection = new SqlConnection(GlobalConfig.ConnectionString(db)))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();

                command.CommandText = query;

                command.Connection = (SqlConnection)connection;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    output = reader.GetTables();
                }

                connection.Close();
            }
            
            return output;
        }
    }
}