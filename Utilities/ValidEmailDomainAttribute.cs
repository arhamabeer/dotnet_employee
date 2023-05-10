namespace dotnet_mvc.Utilities
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        private readonly string _format;

        public ValidEmailDomainAttribute(string format) {
            _format = format;
        }
        public override bool IsValid(object? value)
        {
            string domain = value.ToString().Split("@")[1];
            return domain.ToUpper() == _format.ToUpper();
        }
    }
}
