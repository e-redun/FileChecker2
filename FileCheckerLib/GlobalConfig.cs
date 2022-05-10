using FileCheckerLib.DataAccess;
using FileCheckerLib.Enums;
using System.Configuration;

namespace FileCheckerLib
{
    /// <summary>
    /// Класс конфигурации
    /// </summary>
    public static class GlobalConfig
    {
        /// <summary>
        /// Путь папки с sql-файлами
        /// </summary>
        public static string QueryFolderPath { get; set; }

        /// <summary>
        /// Соединение с БД
        /// </summary>
        public static IDataConnection Connection { get; private set; }

        /// <summary>
        /// Построитель sql-запросов
        /// </summary>
        public static IQueryBuilder QueryBuilder { get; private set; }

        /// <summary>
        /// Инициализирует соединение с БД
        /// </summary>
        /// <param name="db">Тип БД</param>
        public static void InitializeConnection(DbTypes db)
        {
            switch (db)
            {
                case DbTypes.MsSql:
                    Connection = new MsSqlConnector();
                    QueryBuilder = new MsQueryBuilder();
                    QueryFolderPath = @".\SqlQueries\MsSql";
                    break;

                case DbTypes.PostgreSql:
                    Connection = new PostgreSqlConnector();
                    QueryBuilder = new PostgreQueryBuilder();
                    QueryFolderPath = @".\SqlQueries\PostgreSql";
                    break;
            }
        }

        /// <summary>
        /// Возвращает строку подключения из фала App.config
        /// </summary>
        /// <param name="name">Имя строки подключения</param>
        /// <returns>Строка подключения</returns>
        public static string ConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        /// <summary>
        /// Возвращает настройки хранимые в файле App.config по ключу
        /// </summary>
        /// <param name="key">Ключ настройки</param>
        /// <returns>Настройка</returns>
        public static string GetAppSettingsByKey(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}