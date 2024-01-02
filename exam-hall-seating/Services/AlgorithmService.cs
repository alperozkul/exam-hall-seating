using exam_hall_seating.Models;
using exam_hall_seating.Services.Interfaces;
using exam_hall_seating.ViewModels.ExamVM;

namespace exam_hall_seating.Services
{
    public class AlgorithmService : IAlgorithmService
    {
        public List<EnrolledStudentViewModel> SortByAlgorithm(List<EnrolledStudentViewModel> students, string algorithmName)
        {
            switch (algorithmName)
            {
                case "Başarı Sıralamasına Göre":
                    students = SortBySuccess(students);
                    break;

                case "Giriş Yılına Göre":
                    students = SortByEntryYear(students);
                    break;

                case "Giriş Yılı ve Başarı Sıralamasına Göre":
                    students = SortByEntryYearWithSuccess(students);
                    break;
            }
            return students;
        }

        public List<EnrolledStudentViewModel> SortRandom(List<EnrolledStudentViewModel> students)
        {
            Random random = new Random();
            return students = students.OrderBy(student => random.Next()).ToList();
        }

        private List<EnrolledStudentViewModel> SortBySuccess(List<EnrolledStudentViewModel> students)
        {
            return students
            .OrderBy(s => s.Number.ToString().Substring(4, 4))         
            .ToList();
        }

        private  List<EnrolledStudentViewModel> SortByEntryYear(List<EnrolledStudentViewModel> students)
        {
            List<EnrolledStudentViewModel> newStudentList = new List<EnrolledStudentViewModel>();

            for (int i = 0; students.Count > 0; i++)
            {
                //Son iki hanesine göre gruplandırma
                var studentGroups = students.GroupBy(numara => numara.Number.ToString().Substring(0, 2));
                foreach (var group in studentGroups) //Her grubun ilk öğrencisini listeye eklemek ve students listesinden silme
                {                   
                    var selectedStudent = group.First();
                    newStudentList.Add(selectedStudent);
                    students.Remove(selectedStudent);
                }
            }
            return newStudentList;
        }

        private List<EnrolledStudentViewModel> SortByEntryYearWithSuccess(List<EnrolledStudentViewModel> students)
        {
            students = SortBySuccess(students);
            students = SortByEntryYear(students);
            return students;
        }

    }
}


