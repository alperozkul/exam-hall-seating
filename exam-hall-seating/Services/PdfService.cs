using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using exam_hall_seating.Services.Interfaces;
using exam_hall_seating.ViewModels.ExamVM;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;
using Document = iTextSharp.text.Document;
using Rectangle = iTextSharp.text.Rectangle;

namespace exam_hall_seating.Services
{
    public class PdfService : IPdfService
    {
        private readonly IClassroomDetailRepository _classroomDetailRepository;
        private readonly IClassroomRepository _classroomRepository;

        public PdfService(IClassroomDetailRepository classroomDetailRepository, IClassroomRepository classroomRepository)
        {
            _classroomDetailRepository = classroomDetailRepository;
            _classroomRepository = classroomRepository;
        }

        public byte[] GeneratePdfAsync(ArrangementViewModel arrangementVM)
        {
            //Sınıf krokisi için gerekli bilgilerin alınması
            int classroomId = _classroomRepository.GetByName(arrangementVM.Students[0].ClassName).Id;
            List<ClassroomDetail> classroomDetails = _classroomDetailRepository.GetAllDetailById(classroomId);
            int totalBlock = classroomDetails.Count;
            int totalColumn = 0;
            int row = classroomDetails[0].Row;
            foreach (var blok in classroomDetails)
            {
                totalColumn += blok.Column;
            }


            using(MemoryStream memoryStream = new MemoryStream())
            {
                //Dökümanın açılması
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                //Fotoğrafın alınması ve pdf'e eklenmesi.
                string imagePath = Path.Combine("wwwroot", "omü.png");
                var image = Image.GetInstance(imagePath);
                image.ScaleAbsolute(150f, 150f);
                image.Alignment = Element.ALIGN_CENTER;
                document.Add(image);

                //Fontlar
                BaseFont STF_Helvetica_Turkish = BaseFont.CreateFont("Helvetica", "CP1254", BaseFont.NOT_EMBEDDED);
                Font fontHeader = new Font(STF_Helvetica_Turkish, 26, Font.NORMAL);
                Font fontNormal = new Font(STF_Helvetica_Turkish, 16, Font.NORMAL);
                Font fontTable = new Font(STF_Helvetica_Turkish, 10, Font.NORMAL);
                Font fontInfo = new Font(STF_Helvetica_Turkish, 12, Font.NORMAL);

                //Başlık
                Paragraph title = new Paragraph("Ondokuz Mayıs Üniversitesi", fontHeader);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                //Sınav Bilgileri
                Paragraph exam = new Paragraph(arrangementVM.LectureName + " Sınavı Oturma Düzeni", fontHeader);
                exam.Alignment = Element.ALIGN_CENTER;
                document.Add(exam);

                //Sınav tarih, başlangıç ve bitiş saat bilgileri
                Paragraph examDetails = new Paragraph($"Sınav Tarihi: {arrangementVM.Date.ToShortDateString()}", fontInfo);
                examDetails.Alignment = Element.ALIGN_CENTER;
                document.Add(examDetails);
                Paragraph startTime = new Paragraph($"Başlangıç Saati: {arrangementVM.StartTime.ToString(@"hh\:mm")}", fontInfo);
                startTime.Alignment = Element.ALIGN_CENTER;
                document.Add(startTime);
                Paragraph endTime = new Paragraph($"Bitiş Saati: {arrangementVM.EndTime.ToString(@"hh\:mm")}", fontInfo);
                endTime.Alignment = Element.ALIGN_CENTER;
                document.Add(endTime);

                //Sınıf Bilgileri
                Paragraph classroom = new Paragraph(arrangementVM.Students[0].ClassName + " Sınıf Krokisi", fontNormal);
                classroom.Alignment = Element.ALIGN_CENTER;
                classroom.SpacingBefore = 30f;
                document.Add(classroom);

                //Çizgi
                Paragraph p1 = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_CENTER, 1)));
                document.Add(p1);

                //Kürsü
                Paragraph kürsü = new Paragraph("Kürsü", fontNormal);
                kürsü.Alignment = Element.ALIGN_CENTER;
                kürsü.SpacingBefore = 20f;
                document.Add(kürsü);
                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(2.0F, 50.0F, BaseColor.BLACK, Element.ALIGN_CENTER, 1)));
                document.Add(p);

                //Tablo halinde sınıf krokisinin oluşturulması
                PdfPTable classroomSketch = new PdfPTable(totalColumn + totalBlock - 1);
                classroomSketch.WidthPercentage = 100;
                classroomSketch.SpacingBefore = 35f;

                int currentValidColumns = 1;
                for (int i = 0; i < row; i++)
                {
                    foreach (var blok in classroomDetails)
                    {
                        char[] availableSeat = blok.ValidColumns.ToCharArray();
                        for (int j = 0; j < blok.Column; j++)
                        {
                            if (availableSeat[j] == '1')
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(currentValidColumns.ToString(), new Font(Font.FontFamily.HELVETICA, 10)));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                cell.MinimumHeight = 1f;
                                currentValidColumns++;
                                classroomSketch.AddCell(cell);

                            }
                            else
                            {
                                PdfPCell cell1 = new PdfPCell(new Phrase());
                                cell1.MinimumHeight = 1f;
                                classroomSketch.AddCell(cell1);
                            }
                            if (j == blok.Column - 1 && blok != classroomDetails[classroomDetails.Count - 1])
                            {
                                PdfPCell emptyCell = new PdfPCell(new Phrase(" "));
                                emptyCell.Border = PdfPCell.NO_BORDER; // Sınır olmayacak
                                emptyCell.MinimumHeight = 1f;
                                classroomSketch.AddCell(emptyCell);
                            }
                            classroomDetails.Count();

                        }
                    }
                }
                document.Add(classroomSketch);

                //Çizgi
                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_CENTER, 1)));
                line.SpacingBefore = 35f;
                document.Add(line);

                //Tablo başlığı
                Paragraph studentList = new Paragraph("Öğrenci Listesi", new Font(STF_Helvetica_Turkish, 18, Font.NORMAL));
                studentList.Alignment = Element.ALIGN_CENTER;
                document.Add(studentList);

                //Öğrenci listesinin oluşturulması
                PdfPTable studentsTable = new PdfPTable(3);
                //studentsTable.WidthPercentage = 100;
                studentsTable.SpacingBefore = 10f;

                PdfPCell c_seatNumber = new PdfPCell(new Phrase("Sıra Numarası", fontTable));
                c_seatNumber.BackgroundColor = BaseColor.LIGHT_GRAY;
                c_seatNumber.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                c_seatNumber.BorderWidthBottom = 1f;
                c_seatNumber.BorderWidthTop = 1f;
                c_seatNumber.BorderWidthLeft = 1f;
                c_seatNumber.BorderWidthRight = 1f;
                c_seatNumber.HorizontalAlignment = Element.ALIGN_CENTER;
                c_seatNumber.VerticalAlignment = Element.ALIGN_CENTER;
                c_seatNumber.NoWrap = false;
                studentsTable.AddCell(c_seatNumber);

                PdfPCell c_Number = new PdfPCell(new Phrase("Numara", fontTable));
                c_Number.BackgroundColor = BaseColor.LIGHT_GRAY;
                c_Number.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                c_Number.BorderWidthBottom = 1f;
                c_Number.BorderWidthTop = 1f;
                c_Number.BorderWidthLeft = 1f;
                c_Number.BorderWidthRight = 1f;
                c_Number.HorizontalAlignment = Element.ALIGN_CENTER;
                c_Number.VerticalAlignment = Element.ALIGN_CENTER;
                c_Number.NoWrap = false;
                studentsTable.AddCell(c_Number);

                PdfPCell c_FullName = new PdfPCell(new Phrase("Ad Soyad", fontTable));
                c_FullName.BackgroundColor = BaseColor.LIGHT_GRAY;
                c_FullName.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                c_FullName.BorderWidthBottom = 1f;
                c_FullName.BorderWidthTop = 1f;
                c_FullName.BorderWidthLeft = 1f;
                c_FullName.BorderWidthRight = 1f;
                c_FullName.HorizontalAlignment = Element.ALIGN_CENTER;
                c_FullName.VerticalAlignment = Element.ALIGN_CENTER;
                c_FullName.NoWrap = false;
                studentsTable.AddCell(c_FullName);

                int currentSeat = 1;
                foreach(var student in arrangementVM.Students)
                {
                    string studentFullName = student.FirstName + " " + student.LastName;

                    PdfPCell seatNumber = new PdfPCell(new Phrase(currentSeat.ToString(), fontTable));
                    seatNumber.HorizontalAlignment = Element.ALIGN_CENTER;
                    PdfPCell Number = new PdfPCell(new Phrase(student.Number.ToString(), fontTable));
                    Number.HorizontalAlignment = Element.ALIGN_CENTER;
                    PdfPCell FullName = new PdfPCell(new Phrase(studentFullName, fontTable));
                    FullName.HorizontalAlignment = Element.ALIGN_CENTER;

                    seatNumber.NoWrap = false;
                    Number.NoWrap = false;
                    FullName.NoWrap = false;

                    studentsTable.AddCell(seatNumber);
                    studentsTable.AddCell(Number);
                    studentsTable.AddCell(FullName);
                    currentSeat++;

                }
                
                document.Add(studentsTable);
                document.Close();
                writer.Close();

                var constant = memoryStream.ToArray();
                return constant;
            }

        }
        
    }
}
