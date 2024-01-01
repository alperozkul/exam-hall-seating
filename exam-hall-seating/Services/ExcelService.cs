using exam_hall_seating.Services.Interfaces;
using exam_hall_seating.ViewModels.ExamVM;
using ExcelDataReader;

namespace exam_hall_seating.Services
{
    public class ExcelService : IExcelService
    {
        public async Task<List<EnrolledStudentViewModel>> ReadExcelFileAsync(IFormFile file)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var filePath = Path.GetTempFileName();

            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                return ReadExcelData(stream);
            }
        }

        private List<EnrolledStudentViewModel> ReadExcelData(Stream stream)
        {
            var excelData = new List<EnrolledStudentViewModel>();

            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                do
                {
                    reader.Read(); //Başlık satırını atlamak için

                    while (reader.Read())
                    {
                        //int.TryParse(reader.GetValue(0)?.ToString(), out int numara); //Numara int

                        var rowData = new EnrolledStudentViewModel
                        {
                            Number = Convert.ToInt32(reader.GetValue(0)),
                            FirstName = reader.GetValue(1)?.ToString(),
                            LastName = reader.GetValue(2)?.ToString(),
                            Mail = reader.GetValue(3)?.ToString(),
                        };

                        excelData.Add(rowData);
                    }
                } while (reader.NextResult());
            }

            return excelData;
        }
    }
}
