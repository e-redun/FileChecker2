using FileCheckerLib.Models;
using System.Collections.Generic;

namespace FileCheckerLib.DataAccess
{
    public interface IDataConnection
    {
        /// <summary>
        /// Возвращает из БД коллекцию путей файлов подлежащих удалению
        /// </summary>
        /// <param name="query">Запрос к БД</param>
        /// <returns>Коллекция путей файлов подлежащих удалению</returns>
        List<FileRecordModel> GetPathsToDelete(string query);


        /// <summary>
        /// Удаляет записи в таблице File БД
        /// </summary>
        /// <param name="query">Sql-запрос</param>
        void DeleteFilePaths(string query);


        /// <summary>
        /// Возвращает список связанных/дочерних таблиц
        /// </summary>
        /// <param name="query">Sql-запрос</param>
        /// <returns>Список связанных/дочерних таблиц</returns>
        List<ChildTableModel> GetChildTables(string query);

        /// <summary>
        /// Возвращает певичный ключ таблицы File
        /// </summary>
        /// <param name="str">Sql-запрос</param>
        string GetFilePkColumnName(string query);
    }
}