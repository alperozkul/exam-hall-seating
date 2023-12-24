using exam_hall_seating.Services.Interfaces;
using exam_hall_seating.ViewModels.ExamVM;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace exam_hall_seating.Services
{
    public class MailService : IMailService
    {
        public void SendMail(ArrangementViewModel arrangementVM, byte[] pdf)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("sinavoturmaduzeni@gmail.com", "");
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("sinavoturmaduzeni@gmail.com");
                mailMessage.Subject = arrangementVM.LectureName + " Sınavı Oturma Düzeni";
                mailMessage.Body = arrangementVM.Date.ToShortDateString() + " tarihli " + arrangementVM.LectureName + " dersinin oturma düzeni ektedir. Lütfen kürsü ve sınıf krokisini dikkate alarak geçerli yerinize oturunuz.";

                //foreach(var  student in arrangementVM.Students)
                //{
                //    mailMessage.To.Add(student.Mail);
                //}

                mailMessage.To.Add(arrangementVM.Students[0].Mail);
                //mailMessage.To.Add(arrangementVM.Students[1].Mail);


                // Ek dosya eklemek için
                MemoryStream stream = new MemoryStream(pdf);
                Attachment attachment = new Attachment(stream, arrangementVM.Date.ToString("dd/MM/yyyy") + "_" + arrangementVM.LectureName + "_" + arrangementVM.Students[0].ClassName + ".pdf");
                mailMessage.Attachments.Add(attachment);

                smtpClient.Send(mailMessage);
            }
        }
    }
}
