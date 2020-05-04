using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailSenderAPI.Models
{
   public partial class Mails
   {

      private static readonly char delimiter = ';';
      private string _recipients;
      /// <summary>
      /// id в базе данных
      /// </summary>
      public int Id { get; set; }
      /// <summary>
      /// Заголов письма
      /// </summary>
      public string Subject { get; set; }
      /// <summary>
      /// Тело письма
      /// </summary>
      public string Body { get; set; }
      /// <summary>
      /// Дата создания письма
      /// </summary>
      public DateTime Datecreate { get; set; }
      /// <summary>
      /// Результат отправки сообщения
      /// </summary>
      public string Result { get; set; }
      /// <summary>
      /// Сообщение об ошибки
      /// </summary>
      public string Failedmessage { get; set; }

      /// <summary>
      /// Список получателей письма
      /// </summary>
      [NotMapped]
      public string[] Recipients
      {
         get => _recipients?.Split(delimiter);
         set => _recipients = value != null ? string.Join($"{delimiter}", value) : null;
      }
   }
}
