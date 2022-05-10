namespace FileCheckerLib.Helpers
{
    /// <summary>
    /// Набор стандартных сообщений
    /// </summary>
    public struct StandardMessages
    {
        /// <summary>
        /// при работе с файлами
        /// </summary>
        public struct File
        {
            public static string FileToDeleteDoesntExist { get; } = "Удаляемый файл не существует ";

            public static string NoFilesToDelete { get; } = "Нет файлов подлежащих удалению";

            public static string FileIsDeleted { get; } = "Удален файл ";

            public static string ExceptionWhenDeleting { get; } = "Возникло исключение при удалении файла";
        }

        /// <summary>
        /// при отправке e-mail
        /// </summary>
        public struct Mail
        {
            public static string MailBody { get; } = "Cм. лог-файл во вложении";

            public static string LogSentSuccesfully { get; } = " успешно отправлен по адресу: ";

            public static string LogWasntSent { get; } = " не был отправлен по адресу: ";

            public static string SmtpExMessage { get; } = "Проверьте настройки smtp-сервера.";

        }

        /// <summary>
        /// при валидации
        /// </summary>
        public struct Validation
        {
            public static string NotValidEmail { get; } = " не является корректным e-mail";

            public static string ReceiverListIsEmpty { get; } = "Список валидных e-mail пустой";
        }

        /// <summary>
        /// при работе с БД
        /// </summary>
        public struct Db
        {
            public static string DbError { get; } = "При работе с БД возникли ошибка.";
            public static string GetChildTablesError { get; } = "Ошибка возникла при получении списка зависимых/дочерних таблиц";
            public static string GetPathsToDeleteError { get; } = "Ошибка возникла при получении списка путей удаляемых файлов";
            public static string DeleteRecordsError { get; } = "Ошибка возникла при удалении записей";
        }
    }
}
