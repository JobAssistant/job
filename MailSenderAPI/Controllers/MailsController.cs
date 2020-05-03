using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MailSenderAPI.Models;

namespace MailSenderAPI.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class MailsController : ControllerBase
   {
      private readonly MailsContext _context;

      public MailsController(MailsContext context)
      {
         _context = context;
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
         MailAddress fromAddress = new MailAddress("xoste49@gmail.com", "Павел");
         MailMessage message = new MailMessage();
         message.From = fromAddress;
         // Добавление адресатов
         foreach (var address in mail.Recipients)
         {
            message.To.Add(address);
         }
         message.Subject = mail.Subject;
         message.Body = mail.Body;
         // Создаем подключение smtp клиента
         SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
         {
            Credentials = new NetworkCredential("xoste49@gmail.com", ""),
            EnableSsl = true
         };
         client.Send(message);

         _context.Mails.Add(mail);
         await _context.SaveChangesAsync();

         //return CreatedAtAction(nameof(GetMails), new { id = mails.Id }, Mails(mail));

         return CreatedAtAction(nameof(GetMails), new { id = mails.Id }, mails);
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
