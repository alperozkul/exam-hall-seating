using Microsoft.AspNetCore.Identity;

namespace exam_hall_seating.Models
{
    public class AppRole : IdentityRole
    {
        public const string Admin = "admin";
        public const string Instructor = "instructor";
    }
}
