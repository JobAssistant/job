namespace MailSenderAPI
{
   public class Mails
   {
      public string subject { get; set; }
      public string body { get; set; }
      public string[] recipients { get; set; }
   }
}
