namespace dotnet_mvc.ViewModels
{
    public class EditRoleViewModel
    {
        public EditRoleViewModel()
        {
            users = new List<ApplicationUser>();
        }
        public string id { get; set; }
        [Required]
        [Display(Name = "Role Name")]
        public string role { get; set; }
        public List<ApplicationUser> users { get; set; }
    }
}
