using BcbCrawler.Util;
using System;
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

            ETipoDocumento[] listaRelatorios = (ETipoDocumento[])Enum.GetValues(typeof(ETipoDocumento));

            foreach (ETipoDocumento tipoDocumento in listaRelatorios)
            {
                int numeroTabelas = (int)tipoDocumento;

                //Aqui esta com o mesmo problema do crawler, tem de ficar fazendo muito if!
                //fazer uma vez e montar objeto é melhor
                string[] nomeTabelas = tipoDocumento == ETipoDocumento.DDR ? ConstDadosCrawler.nomeTabelasDDR : ConstDadosCrawler.nomeTabelasDRM;
                string textoTabela = string.Empty;

                if (tipoDocumento == ETipoDocumento.DLO)
                {
                    nomeTabelas = ConstDadosCrawler.nomeTabelasDLO;
                }

                bool tabelasMudaram = CriadorTabelas.VerificaMudanca(tipoDocumento, numeroTabelas, nomeTabelas, ref textoTabela);
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
