using System.ComponentModel.DataAnnotations;

namespace exam_hall_seating.ViewModels.InstructorVM
{
    public class EditInstructorViewModel
    {  
        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Lütfen ad giriniz.")]
        public string FirstName { get; set; }

        [Display(Name = "Soyad")]
        [Required(ErrorMessage = "Lütfen soyad giriniz.")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Lütfen email giriniz.")]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "Lütfen telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Departman")]
        [Required(ErrorMessage = "Lütfen departman seçiniz.")]
        public int DepartmentId { get; set; }

        //[Display(Name = "Şifre")]
        //[Required(ErrorMessage = "Lütfen şifre giriniz.")]
        //public string Password { get; set; } //Burada bir hata var Update calısmıyor.
    }
}
