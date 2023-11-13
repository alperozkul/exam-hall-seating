using System.ComponentModel.DataAnnotations;

namespace exam_hall_seating.ViewModels.Account
{
    public class LoginViewModel
    {
        [Display(Name = "Email Adresi")]
        [Required(ErrorMessage = "Lütfen Email adresi giriniz.")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
