using Azure.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Helper
{
     public  class MailSender
    {
       
        private readonly IConfiguration _configuration;

        public MailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Mail(string sender, string sub, string body)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");

            SmtpClient smtp = new SmtpClient(smtpSettings["Server"])
            {
                EnableSsl = bool.Parse(smtpSettings["EnableSsl"]),
                Port = int.Parse(smtpSettings["Port"]),
                Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"])
            };

            smtp.Send(smtpSettings["Username"], sender, sub, body);
        }

    }
}
