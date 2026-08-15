using ClimaTempoDesafioAPI.Helpers.Exceptions;

namespace ClimaTempoDesafioAPI.Helpers
{
    public static class TratamentoDados
    {
        public static string NormalizarEmail(string email)
        {
            return email.Trim().ToLowerInvariant();
        }

        internal static void ValidarSenha(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new BusinessException("Senha é obrigatória.");

            var erros = new List<string>();

            if (password.Length < 6)
                erros.Add("Senha deve ter pelo menos 6 caracteres.");

            if (!password.Any(char.IsUpper))
                erros.Add("Senha deve conter pelo menos uma letra maiúscula.");

            if (!password.Any(char.IsLower))
                erros.Add("Senha deve conter pelo menos uma letra minúscula.");

            if (!password.Any(char.IsDigit))
                erros.Add("Senha deve conter pelo menos um número.");

            if (erros.Any())
                throw new ValidationException(erros);
        }
    }
}
