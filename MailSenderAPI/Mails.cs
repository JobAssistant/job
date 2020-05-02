using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailSenderAPI
{
   public class Mails
   {
      public string subject { get; set; }
      public string body { get; set; }
      public string[] recipients { get; set; }
   }
}
