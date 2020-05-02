using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MailSenderAPI.Controllers
{
   [Route("api/[controller]")]
   public class MailsController : Controller
   {
      

      // GET: api/<controller>
      [HttpGet]
      public IEnumerable<Mails> Get()
      {
         return Enumerable.Range(1, 1).Select(i => new Mails
         {
            subject = "testsubject",
            body = "testbody",
            recipients = new []{"test1","test2"}
         });
      }

      // GET api/<controller>/5
      [HttpGet("{id}")]
      public string Get(int id)
      {
         return "value";
      }

      // Данные для теста
      // {"subject":"testsubject","body":"testbody","recipients":["test1","test2"]}
      // POST api/<controller>
      [HttpPost]
      public void Post([FromBody]Mails value)
      {
         Mails t = value;
         // Сформировать email сообщение
         // выполнить его отправку
         // Добавить запись в БД
         // поля Result: (значения Ok, Failed), а также поле FailedMessage
      }

      // PUT api/<controller>/5
      [HttpPut("{id}")]
      public void Put(int id, [FromBody]string value)
      {
      }

      // DELETE api/<controller>/5
      [HttpDelete("{id}")]
      public void Delete(int id)
      {
      }
   }
}
