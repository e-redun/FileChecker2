using System.Text;

namespace FileCheckerLib.Models
{
    public class ChildTableModel
    {
        public string TableName { get; set; }
        public string ForeigID { get; set; }
        public string ParentTableName { get; set; }
        public string ParentID { get; set; }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            output.Append(TableName); output.Append("\t");
            output.Append(ForeigID); output.Append("\t");
            output.Append(ParentTableName); output.Append("\t");
            output.Append(ParentID); output.AppendLine();

            return output.ToString();
        }
    }
}
