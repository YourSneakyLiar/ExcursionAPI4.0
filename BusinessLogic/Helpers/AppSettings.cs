namespace BusinessLogic.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; } // Секретный ключ для JWT
        public int RefreshTokenTTL { get; set; } // Время жизни Refresh Token (в днях)
        public string EmailFrom { get; set; } // Email отправителя
        public string SmtpHost { get; set; } // SMTP сервер
        public int SmtpPort { get; set; } // Порт SMTP
        public string SmtpUser { get; set; } // Логин SMTP
        public string SmtpPass { get; set; } // Пароль SMTP
    }
}