using BcbCrawler.Interfaces;
using BcbCrawler.Util;
using System.Collections.Generic;
using System.IO;

namespace BcbCrawler
{
    class FazTudo
    {
        static readonly string arquivoSaida = "EmailBasileia.html";

        static void Main(string[] args)
        {           
            string textoArquivo = string.Empty;
            bool naoteveMudancas = true;

            List<IRelatorio> listaRelatorios = new List<IRelatorio>
            {
                new DLO(),
                new DDR(),
                new DRM()
            };

            foreach (IRelatorio relatorio in listaRelatorios)
            {
                string textoTabela = string.Empty;

                bool tabelasMudaram = CriadorTabelas.VerificaMudanca(relatorio, ref textoTabela);
                naoteveMudancas = naoteveMudancas && !tabelasMudaram;
                textoArquivo += textoTabela;
            }

            if (naoteveMudancas)
            {
                textoArquivo = string.Empty;
            }

            using (StreamWriter arquivo = new StreamWriter(arquivoSaida))
            {
                arquivo.Write(textoArquivo);
            }
        }
    }
}
