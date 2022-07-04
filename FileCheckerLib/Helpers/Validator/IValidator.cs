using System.Collections.Generic;

namespace FileCheckerLib.Helpers
{
    public interface IValidator
    {
        bool ValidateFolder(string folderPath);

        bool ValidateEmail(string email);

        List<string> ValidateEmails(List<string> emails);

        bool ValidateLogReceiversFile(string filePath);

        bool ValidateDbType(string strDbType);
    }
}