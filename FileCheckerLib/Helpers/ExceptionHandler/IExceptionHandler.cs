using System.Collections.Generic;
using FileCheckerLib.Delegates;
using FileCheckerLib.Models;

namespace FileCheckerLib.Helpers
{
    public interface IExceptionHandler
    {
        List<ChildTableModel> TryCatchShell(GetChildTablesDel del, string query, string addErrorMessage);
        List<FileRecordModel> TryCatchShell(GetFileRecordDel del, string query, string addErrorMessage);
        string TryCatchShell(StringDel del, string query, string addErrorMessage);
        void TryCatchShell(VoidDel del, string query, string addErrorMessage);
    }
}