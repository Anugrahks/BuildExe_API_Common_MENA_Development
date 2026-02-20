using BuildExeBasic.DBContexts;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BuildExeBasic.Repository
{
    public class MailRepository : IMailRepository
    {
        private readonly BasicContext _dbContext;
        public MailRepository(BasicContext dbContext)
        {
            _dbContext = dbContext;
        }
        public enum Actions
        {
            Insert = 1,
            Update = 2,
            Delete = 3,
            SelectAll = 4,
            Select = 5
        }

        public string SendMailStudentwithCC()
        {
            string recepientEmail = "suhailmusaffi05@gmail.com";
            string subject = "TEST MAIL2"; string body = "HELLO MAHN11";
            string ReplayMail = ""; string CCMail = "";
            using (MailMessage mailMessage = new MailMessage())
            {

                mailMessage.From = new MailAddress("support@buildexe.org", "BuildExe1");
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(recepientEmail));
                if (!string.IsNullOrEmpty(ReplayMail))
                {
                    mailMessage.ReplyToList.Add(ReplayMail);
                }
                string[] CCId = CCMail.Split(',');
                if (CCMail != "")
                {
                    foreach (string CCEmail in CCId)
                    {
                        mailMessage.Bcc.Add(new MailAddress(CCEmail));
                    }
                }
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = "buildexe7@gmail.com";
                NetworkCred.Password = "dwfh npah lpzs kjei";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;

                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }

            return "SUCCESS";
        }


    }
}
