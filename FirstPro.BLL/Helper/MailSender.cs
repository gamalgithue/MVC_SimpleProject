using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Helper
{
     public class MailSender
    {
        public static void Mail(string sender,string sub,string body)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("gemyelbatawy@gmail.com", "dxugvwehyipvljlx");
            smtp.Send("gemyelbatawy@gmail.com", sender, sub, body);
        }
      
    }
}
