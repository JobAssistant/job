using System;
using System.Collections.Generic;

namespace MailSenderAPI.Models
{
    public partial class Mails
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string[] Recipients { get; set; }
        public DateTime Datecreate { get; set; }
        public string Result { get; set; }
    }
}
