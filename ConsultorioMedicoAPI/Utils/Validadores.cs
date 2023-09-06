namespace ConsultorioMedicoAPI.Utils
{
    public class Validadores
    {
        public static bool VerificarZeroNaData(int numero)
        {
            string numeroStr = numero.ToString();
            return numeroStr[0] == '0' ? true : false;
        }
    }
}
