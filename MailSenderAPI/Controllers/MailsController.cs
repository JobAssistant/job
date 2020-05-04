using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MailSenderAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using SmtpStatusCode = MailKit.Net.Smtp.SmtpStatusCode;

namespace MailSenderAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class MailsController : ControllerBase
   {
      private readonly MailsContext _context;
      private Config config { get; set; }

      public MailsController(MailsContext context, IOptions<Config> settings)
      {
         _context = context;
         config = settings.Value;
      }

      

      // GET: api/Mails1
      [HttpGet]
      public async Task<ActionResult<IEnumerable<Mails>>> GetMails()
      {
         return await _context.Mails.ToListAsync();
      }

      // GET: api/Mails1/5
      [HttpGet("{id}")]
      public async Task<ActionResult<Mails>> GetMails(int id)
      {
         var mails = await _context.Mails.FindAsync(id);

         if (mails == null)
         {
            return NotFound();
         }

         return mails;
      }

      // PUT: api/Mails1/5
      // To protect from overposting attacks, enable the specific properties you want to bind to, for
      // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
      [HttpPut("{id}")]
      public async Task<IActionResult> PutMails(int id, Mails mails)
      {
         if (id != mails.Id)
         {
            return BadRequest();
         }

         _context.Entry(mails).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!MailsExists(id))
            {
               return NotFound();
            } else
            {
               throw;
            }
         }

         return NoContent();
      }

      /*
       * Data Type	            Maximum Length	.NET Type
         CHAR	                  255	         string
         BINARY	               255	         byte[]
         VARCHAR, VARBINARY	   65,535	      string, byte[]
         TINYBLOB, TINYTEXT	   255	         byte[]
         BLOB, TEXT	            65,535	      byte[]
         MEDIUMBLOB, MEDIUMTEXT	16,777,215	   byte[]
         LONGBLOB, LONGTEXT	   4,294,967,295	byte[]
         ENUM	                  65,535	      string
         SET	                  65,535	      string
       */

      // POST: api/Mails1
      // To protect from overposting attacks, enable the specific properties you want to bind to, for
      // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
      [HttpPost]
      public async Task<ActionResult<Mails>> PostMails(Mails mails)
      {
         var mail = new Mails
         {
            Subject = mails.Subject,
            Body = mails.Body,
            Recipients = mails.Recipients,
            Datecreate = DateTime.Now
         };
         // Отправка почты
         try
         {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(config.MailAddress));
            // Добавление адресатов
            foreach (var address in mail.Recipients)
            {
               emailMessage.To.Add(new MailboxAddress(address));
            }

            emailMessage.Subject = mail.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
               Text = mail.Body
            };

            var client = new SmtpClient();
            client.Connect(config.Host, config.Port, config.UseSsl);
            client.Authenticate(config.UserName, config.Password);
            client.MessageSent += (sender, args) => mail.Result = "OK";
            client.Send(emailMessage);
            client.Disconnect(true);

            
         }
         // При отсутствии хоста или не верном порте
         catch (System.Net.Sockets.SocketException e)
         {
            mail.Result = "Failed";
            mail.Failedmessage = e.Message;
         }
         catch (SmtpCommandException ex)
         {
            mail.Result = "Failed";
            mail.Failedmessage = $"Error trying to connect: {ex.Message} StatusCode: {ex.StatusCode}";
         }
         catch (SmtpProtocolException ex)
         {
            mail.Result = "Failed";
            mail.Failedmessage = $"Protocol error while trying to connect: {ex.Message}";
         }
         // При проблеме с ssl сертификатом
         catch (MailKit.Security.SslHandshakeException e)
         {
            mail.Result = "Failed";
            mail.Failedmessage = "Не верный ssl сертификат или не правильный порт";
         }
         // Проблема с логином или паролем
         catch (MailKit.Security.AuthenticationException e)
         {
            mail.Result = "Failed";
            mail.Failedmessage = "Invalid user name or password.";
         }
         catch (Exception e)
         {
            mail.Result = "Failed";
            mail.Failedmessage = e.Message;
         }

         _context.Mails.Add(mail);
         await _context.SaveChangesAsync();

         return CreatedAtAction(nameof(GetMails), new { id = mails.Id }, mail);
      }

      // DELETE: api/Mails1/5
      [HttpDelete("{id}")]
      public async Task<ActionResult<Mails>> DeleteMails(int id)
      {
         var mails = await _context.Mails.FindAsync(id);
         if (mails == null)
         {
            return NotFound();
         }

         _context.Mails.Remove(mails);
         await _context.SaveChangesAsync();

         return mails;
      }

      private bool MailsExists(int id)
      {
         return _context.Mails.Any(e => e.Id == id);
      }
   }
}
