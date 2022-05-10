using FileCheckerLib.Helpers;
using FileCheckerLib.Models;
using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace FileCheckerLib.DataAccess
{
    public class PostgreSqlConnector : IDataConnection
    {
        /// <summary>
        /// Имя строки подключния в файле App.config
        /// </summary>
        private const string db = "PostgreSql";

        /// <summary>
        /// Возвращает певичный ключ таблицы File
        /// </summary>
        /// <param name="query">Sql-запрос</param>
        public string GetFilePkColumnName(string query)
        {
            string output = "";

            using (IDbConnection connection = new NpgsqlConnection(GlobalConfig.ConnectionString(db)))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand();

                command.CommandText = query;

                command.Connection = (NpgsqlConnection)connection;

                output = command.ExecuteScalar().ToString();

                connection.Close();
            }

            return output;
        }

        /// <summary>
        /// Возвращает список путей к файлам подлежащим удалению
        /// </summary>
        /// <param name="query">Sql-запрос</param>
        /// <returns>Cписок путей к файлам подлежащим удалению</returns>
        public List<FileRecordModel> GetPathsToDelete(string query)
        {
            List<FileRecordModel> output = new List<FileRecordModel>();

            using (IDbConnection connection = new NpgsqlConnection(GlobalConfig.ConnectionString(db)))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand();
                command.CommandText = query;

                command.Connection = (NpgsqlConnection)connection;

                using (NpgsqlDataReader reader = command.ExecuteReader())
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
            using (IDbConnection connection = new NpgsqlConnection(GlobalConfig.ConnectionString(db)))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand();
                command.CommandText = query;

                command.Connection = (NpgsqlConnection)connection;

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

            using (IDbConnection connection = new NpgsqlConnection(GlobalConfig.ConnectionString(db)))
            {
                connection.Open();

                NpgsqlCommand command = new NpgsqlCommand();

                command.CommandText = query;

                command.Connection = (NpgsqlConnection)connection;

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    output = reader.GetTables();
                }

                connection.Close();
            }

            return output;
        }
    }
}