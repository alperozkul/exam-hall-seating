using exam_hall_seating.ViewModels.ExamVM;

namespace exam_hall_seating.Services.Interfaces
{
    public interface IAlgorithmService
    {
        List<EnrolledStudentViewModel> SortByAlgorithm(List<EnrolledStudentViewModel> students, string algorithmName);
        List<EnrolledStudentViewModel> SortRandom(List<EnrolledStudentViewModel> students);
    }
}
