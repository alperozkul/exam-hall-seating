using exam_hall_seating.ViewModels.ExamVM;

namespace exam_hall_seating.Services.Interfaces
{
    public interface IExcelService
    {
        Task<List<EnrolledStudentViewModel>> ReadExcelFileAsync(IFormFile file);
    }
}
