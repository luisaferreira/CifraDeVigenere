namespace CifraVigenere
{
    public static class Program
    {
        public static void Main()
        {
            var chave = "adsifpe";
            var texto = "Seguranca em Sistemas Computacionais 2024.2";
            var resultado = Cifra(texto, chave);
            Console.WriteLine("Texto cifrado: " + resultado);
            var textoDecifrado = Cifra(resultado, chave, Acao.Decifrar);
            Console.WriteLine("Texto decifrado: " + textoDecifrado);
        }

        public static string Cifra(string texto, string chave, Acao acao = Acao.Cifrar)
        {
            var caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ,;._";
            var textoFinal = string.Empty;

            if (!chave.All(c => caracteres.Contains(c)) || !texto.All(c => caracteres.Contains(c)))
                throw new Exception("Chave e/ou texto inválido(s)");

            chave = AjustarChave(chave, texto.Length);

            try
            {
                for (int i = 0; i < texto.Length; i++)
                {
                    var textoLetra = texto[i];
                    var chaveLetra = chave[i];

                    var textoLetraIndex = caracteres.IndexOf(textoLetra);
                    var chaveLetraIndex = caracteres.IndexOf(chaveLetra);

                    int index = 0;

                    switch (acao)
                    {
                        case Acao.Cifrar:
                            index = textoLetraIndex + chaveLetraIndex;
                            if (index >= caracteres.Length)
                                index -= caracteres.Length;
                            break;
                        case Acao.Decifrar:
                            index = textoLetraIndex - chaveLetraIndex;
                            if (index < 0)
                                index += caracteres.Length;
                            break;
                    }

                    textoFinal += caracteres[index];
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cifrar/decifrar: " + ex.Message);
            }

            return textoFinal;
        }
        public static string AjustarChave(string palavra, int tamanhoFinal)
        {
            if (palavra.Length >= tamanhoFinal)
                return palavra.Substring(0, tamanhoFinal);

            string resultado = palavra;

            while (resultado.Length < tamanhoFinal)
                resultado += palavra;

            return resultado.Substring(0, tamanhoFinal);
        }
    }

    public enum Acao
    {
        Cifrar,
        Decifrar
    }

}