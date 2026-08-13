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
                throw new ArgumentException("Senha é obrigatória.");

            if (password.Length < 6)
                throw new ArgumentException("Senha deve ter pelo menos 6 caracteres.");
        }
    }
}
