using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCheckerLib.Helpers
{
    public class GlobalHelper
    {
        public static IDataAccess DataAccess { get; set; }

        public static IEmailer Emailer { get; set; }

        public static IFileIO FileIO { get; set; }

        public static IValidator Validator { get; set; }

        public static IExceptionHandler ExceptionHandler { get; set; }

        public static ILogger Logger { get; set; }

        public static void Initialize()
        {
            DataAccess = new DataAccess();

            Emailer = new Emailer();

            FileIO = new FileIO();

            Validator = new Validator();

            ExceptionHandler = new ExceptionHandler();

            Logger = new Logger();
        }
    }
}
