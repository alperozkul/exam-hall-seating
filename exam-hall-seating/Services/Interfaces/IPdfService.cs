using exam_hall_seating.ViewModels.ExamVM;

namespace exam_hall_seating.Services.Interfaces
{
    public interface IPdfService
    {
        byte[] GeneratePdfAsync(ArrangementViewModel arrangementVM);
        
    }
}
