using FileCheckerLib.Models;
using System.Collections.Generic;

namespace FileCheckerLib.Delegates
{
    public delegate List<ChildTableModel> GetChildTablesDel(string str);

    public delegate List<FileRecordModel> GetFileRecordDel(string str);

    public delegate void VoidDel(string str);

    public delegate string StringDel(string str);
}
