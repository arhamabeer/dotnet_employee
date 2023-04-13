

namespace dotnet_mvc.Models
{
    public class Employee
    {
         public int id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Name must be from length 5 to 25")]
        [MaxLength(25, ErrorMessage = "Name must be from length 5 to 25")]
        public string name{ get; set; }
        [Required]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Invalid Email")]
        [Display(Name = "Professional Email")]
        public string email { get; set; }
        [Required]
        public Dept? department { get; set; }
        
        public string? photoPath { get; set; }
    }
}
