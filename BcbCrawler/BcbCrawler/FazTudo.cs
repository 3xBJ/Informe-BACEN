using System.IO;

namespace BcbCrawler
{
    class FazTudo
    {
        static readonly string arquivoSaida = "EmailBasileia.html";

        static void Main(string[] args)
        {
            string textoArquivo = MontadorDeTextos.MontaTextoTabelas();

            textoArquivo += MontadorDeTextos.MontaTexoNormas();

            using (StreamWriter arquivo = new StreamWriter(arquivoSaida))
            {
                arquivo.Write(textoArquivo);
            }
        }
    }
}
