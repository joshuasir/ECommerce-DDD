using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Payment.Services
{
    public class MailInvoicer
    {
        public string tocc;
        public string subject;
        public Order order;
        public MailInvoicer(string tocc, string subject, Order order)
        {
            this.tocc = tocc;
            this.subject = subject;
            this.order = order;
        }
        public void SendEmail()
        {
            System.Net.Mail.MailAddressCollection to = new System.Net.Mail.MailAddressCollection();
            System.Net.Mail.MailAddressCollection cc = new System.Net.Mail.MailAddressCollection();
            System.Net.Mail.MailAddressCollection bcc = new System.Net.Mail.MailAddressCollection();


            to.Add(new System.Net.Mail.MailAddress(tocc));

            List<System.Net.Mail.Attachment> attachments = new List<System.Net.Mail.Attachment>();
            
            string body = "" + order.totalPrice;
            try
            {
                MailMessage mailMessage = new MailMessage();
                /*mailMessage.From = "[username]"; */
                //mailMessage.From = from;
                foreach (MailAddress address in to)
                {
                    mailMessage.To.Add(address);
                }

                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = System.Net.Mail.MailPriority.Normal;
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                SmtpClient smtpClient = new SmtpClient()
                {
                    /*
                    Host = "[SMTPHost value]",
                    Port = "[SMTPPort value]", 
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("[username]","[password]" ),
                    */
                    TargetName = tocc,
                    EnableSsl = true
                };
                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (SmtpFailedRecipientException ex) { throw ex; }


            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
