namespace dotnet_mvc.ViewModels
{
    public class HomeEditViewModel : HomeCreateViewModel
    {
        public int id{ get; set; }
        public string? existingPhoto { get; set; }
    }
}
