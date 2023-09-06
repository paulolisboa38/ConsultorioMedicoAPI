namespace ConsultorioMedicoAPI.Utils
{
    public static class Validadores
    {
        public static bool VerificarZeroNaData(int numero)
        {
            string numeroStr = numero.ToString();
            return numeroStr[0] == '0' ? true : false;
        }

        public static bool VerificarAnoNascimento(int anoNascimento)
        {
            int anoAtual = DateTime.UtcNow.Year;
            if (anoNascimento > anoAtual || anoNascimento < anoAtual - 120)
            {
                return false;
            }
            return true;
        }
    }
}
