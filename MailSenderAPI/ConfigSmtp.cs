using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailSenderAPI
{
   public class ConfigSmtp
   {
      /// <summary>
      /// Хост SMTP сервера
      /// </summary>
      public string Host { get; set; }
      /// <summary>
      /// Порт SMTP сервера
      /// </summary>
      public int Port { get; set; }
      /// <summary>
      /// Использовать ssl шифрование при соединении
      /// </summary>
      public bool UseSsl { get; set; }
      /// <summary>
      /// Логин для подключение к SMTP серверу
      /// </summary>
      public string UserName { get; set; }
      /// <summary>
      /// Пароль для подключение к SMTP серверу
      /// </summary>
      public string Password { get; set; }
      /// <summary>
      /// Email адрес с корого будут отправлятся письма
      /// </summary>
      public string MailAddress { get; set; }
   }
}
