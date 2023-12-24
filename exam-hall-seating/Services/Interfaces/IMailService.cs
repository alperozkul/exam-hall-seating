using exam_hall_seating.ViewModels.ExamVM;
using Microsoft.AspNetCore.Mvc;

namespace exam_hall_seating.Services.Interfaces
{
    public interface IMailService
    {
        void SendMail(ArrangementViewModel arrangementVM, byte[] pdf); 
    }
}
