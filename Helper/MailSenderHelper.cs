using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Helper
{
    public static class MailSenderHelper
    {
        public static bool SendMail(string emailAddress,string callBackUrl,string linktext)
        {
			try
			{

				SmtpClient smtpClient = new SmtpClient();
				smtpClient.Host = "smtp.live.com";
				smtpClient.Port = 587;
				smtpClient.EnableSsl = true;

				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("<h2>Please click Accept</h2>");
				string link=$"<a href='{callBackUrl}' style='color: white;text-decoration: none;display: inline-block; padding: 14px 25px; background-color: #f44336; text-align:center;'>{linktext}</a>";
				stringBuilder.AppendLine(link);
				MailAddress mailAddress = new MailAddress("heybeat@hotmail.com", "Confirmation EMail");

				MailMessage mailMessage = new MailMessage();
				mailMessage.To.Add(emailAddress);
				mailMessage.From = mailAddress;
				mailMessage.Subject = "Please Confirm ";
				mailMessage.Body = stringBuilder.ToString();
				mailMessage.IsBodyHtml = true;
			

				smtpClient.Credentials = new NetworkCredential("heybeat@hotmail.com", "Ea.eypio1903");
				smtpClient.Send(mailMessage);
				return true;
			
			}
			catch 
			{

				return false;
			}
        }
    }
}
