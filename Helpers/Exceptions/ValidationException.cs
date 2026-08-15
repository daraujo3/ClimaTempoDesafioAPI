namespace ClimaTempoDesafioAPI.Helpers.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; }

        public ValidationException(List<string> errors)
            : base("Foram encontrados erros de validação.")
        {
            Errors = errors;
        }
    }
}
