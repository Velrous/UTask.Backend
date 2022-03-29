namespace UTask.Backend.Common.Base.Contexts
{
    /// <summary>
    /// Контекст сервера
    /// </summary>
    public static class ServerContext
    {
        [ThreadStatic]
        private static int _userId;
        [ThreadStatic]
        private static int _userRoleId;
        [ThreadStatic]
        private static string _userLogin;
        [ThreadStatic]
        private static string _language;

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public static int UserId
        {
            get => _userId;
            set
            {
                if (value.Equals(_userId)) return;
                _userId = value;
            }
        }

        /// <summary>
        /// Идентификатор роли пользователя
        /// </summary>
        public static int UserRoleId
        {
            get => _userRoleId;
            set
            {
                if (value.Equals(_userRoleId)) return;
                _userRoleId = value;
            }
        }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public static string UserLogin
        {
            get => _userLogin;
            set
            {
                if (value.Equals(_userLogin)) return;
                _userLogin = value;
            }
        }

        /// <summary>
        /// Локаль пользователя
        /// </summary>
        public static string Language
        {
            get => _language;
            set
            {
                if (value.Equals(_language)) return;
                _language = value;
            }
        }
    }
}
