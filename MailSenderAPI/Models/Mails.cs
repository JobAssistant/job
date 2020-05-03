using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MailSenderAPI.Models
{
   public partial class Mails
   {
      private static readonly char delimiter = ';';
      private string _recipients;
      public int Id { get; set; }
      public string Subject { get; set; }
      public string Body { get; set; }
      
      public DateTime Datecreate { get; set; }
      public string Result { get; set; }

      [NotMapped]
      public string[] Recipients
      {
         get => _recipients?.Split(delimiter);
         set => _recipients = value != null ? string.Join($"{delimiter}", value) : null;
      }
   }
}
