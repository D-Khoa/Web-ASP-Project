using System.Net;
using System.Net.Mail;

namespace MES_IFM_Web.Models.MailContact
{
    public class SendMail
    {
        public void SendMailAuto(MailInfo mailInfo)
        {
            SmtpClient smtp = new SmtpClient();

            //ĐỊA CHỈ SMTP Server
            smtp.Host = "smtp.gmail.com";
            //Cổng SMTP
            smtp.Port = 587;
            //SMTP yêu cầu mã hóa dữ liệu theo SSL
            smtp.EnableSsl = true;
            //UserName và Password của mail
            smtp.Credentials = new NetworkCredential("dk.wofts@gmail.com", "4865awds");

            //Tham số lần lượt là địa chỉ người gửi, người nhận, tiêu đề và nội dung thư
            smtp.Send("sendmail.aptech@gmail.com", mailInfo.mailTo, mailInfo.mailSubject, mailInfo.mailMessage);
        }
    }
}
