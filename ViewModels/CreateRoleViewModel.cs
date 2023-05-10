namespace dotnet_mvc.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role Name")]
        public string role { get; set; }
    }
}
