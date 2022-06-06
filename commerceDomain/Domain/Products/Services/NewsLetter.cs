using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Products.Services
{
    public class NewsLetter
    {

        public string tocc;
        public string subject;
        public List<Product> products;
        public NewsLetter(List<Product> products)
        {
            this.products = products;
        }
        public void SendEmail(string tocc, string subject)
        {
            System.Net.Mail.MailAddressCollection to = new System.Net.Mail.MailAddressCollection();
            System.Net.Mail.MailAddressCollection cc = new System.Net.Mail.MailAddressCollection();
            System.Net.Mail.MailAddressCollection bcc = new System.Net.Mail.MailAddressCollection();


            to.Add(new System.Net.Mail.MailAddress(tocc));

            var body = "";
            products.ForEach(a => {
                body += a.productName + "\n" + a.productDescription + "\n" + a.productPrice + "\n\n";
            }); 

            List<System.Net.Mail.Attachment> attachments = new List<System.Net.Mail.Attachment>();

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
