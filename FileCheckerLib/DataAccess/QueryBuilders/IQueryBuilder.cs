using FileCheckerLib.Models;
using System.Collections.Generic;

namespace FileCheckerLib.DataAccess
{
    /// <summary>
    /// Интерфейс-построитель динамимических sql-запросов
    /// </summary>
    public interface IQueryBuilder
    {
        /// <summary>
        /// Возвращает запрос на получение списка путей файлов подлежащих удалению
        /// </summary>
        /// <param name="connectedTables">Список связанных/дочерних таблиц</param>
        /// <returns>Запрос на получение списка путей файлов подлежащих удалению</returns>
        string GetQueryGetPathsToDelete(List<ChildTableModel> connectedTables, string filePkColumnName);

        /// <summary>
        /// Возвращает запрос на удаление записей в БД согласно списку путей файлов
        /// </summary>
        /// <param name="paths">Список путей файлов, подлежащих удалению</param>
        /// <returns>Запрос на удаление записей в БД согласно списку путей файлов</returns>
        string GetQueryToDeleteFilePaths(List<FileRecordModel> paths, string filePkColumnName);
    }
}
