using System.ComponentModel.DataAnnotations;

namespace exam_hall_seating.ViewModels.LectureVM
{
    public class CreateLectureViewModel
    {
        [Display(Name = "Ders Kodu")]
        [Required(ErrorMessage = "Lütfen ders kodunu giriniz.")]
        public string Code { get; set; }
        [Display(Name = "Ders Adı")]
        [Required(ErrorMessage = "Lütfen ders adını giriniz.")]
        public string Name { get; set; }
        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Lütfen açıklama giriniz.")]
        public string Description { get; set; }
        [Display(Name = "Yıl")]
        [Required(ErrorMessage = "Lütfen yıl giriniz.")]
        public int Year { get; set; }
        [Display(Name = "Dönem")]
        [Required(ErrorMessage = "Lütfen dönem seçiniz.")]
        public int Period { get; set; }
        [Display(Name = "Departman")]
        [Required(ErrorMessage = "Lütfen departman seçiniz.")]
        public int DepartmentId { get; set; }
    }
}
