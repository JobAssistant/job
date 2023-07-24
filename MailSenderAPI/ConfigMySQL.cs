using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailSenderAPI
{
   public class ConfigMySQL
   {
      /// <summary>
      /// ip адрес сервера MySQL
      /// </summary>
      public string server { get; set; }
      /// <summary>
      /// База данных на сервере MySQL
      /// </summary>
      public string database { get; set; }
      /// <summary>
      /// Логин на сервере MySQL
      /// </summary>
      public string user { get; set; }
      /// <summary>
      /// Пароль на сервере MySQl
      /// </summary>
      public string password { get; set; }
   }
}
