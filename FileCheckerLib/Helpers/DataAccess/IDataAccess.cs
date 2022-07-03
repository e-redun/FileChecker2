using System.Collections.Generic;
using System.Data.Common;
using FileCheckerLib.Models;

namespace FileCheckerLib.Helpers
{
    public interface IDataAccess
    {
        List<FileRecordModel> GetData(DbDataReader dataReader);
        List<ChildTableModel> GetTables(DbDataReader dataReader);
    }
}