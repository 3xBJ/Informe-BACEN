using BcbCrawler.Interfaces;
using BcbCrawler.Util;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace BcbCrawler
{
    class CriadorTabelas
    {
        public static bool VerificaMudanca(IRelatorio relatorio, ref string textoTabelas)
        {
            return MontaTextoESalvaArquivo(relatorio, ref textoTabelas);
        }

        public static bool MontaTextoESalvaArquivo(IRelatorio relatorio, ref string textoTabelas)
        {
            int i = 0;
            int somaMudadas = 0;
            BinaryFormatter binario = new BinaryFormatter();

            textoTabelas += string.Format(ConstStringHtml.tituloEUrl, relatorio.Nome, relatorio.Url);

            foreach (HtmlNodeCollection noTabela in Crawler.RetornaNodeHTML(relatorio))
            {
                int mudadas = 0;

                string textoTabela = string.Format(ConstStringHtml.tituloTabela, relatorio.NomeTabelas[i]);

                //Não mova esse i++ de lugar!!
                i++;

                if (noTabela == null)
                {
                    textoTabela += Formatador.PrintaLinha(new LinhaDadosBCB("Erro na leitura do nó", "Verifique o serviço", "Tabela " + i.ToString()));
                    somaMudadas++;
                    continue;
                }

                relatorio.Html = noTabela;

                string nomeArquivo = string.Format(ConstStringHtml.nomeBin, relatorio.Nome, i);

                DadosBCB dadosBCB = new DadosBCB(relatorio);

                if (!File.Exists(nomeArquivo))
                {
                    Stream tmp = File.Create(nomeArquivo);
                    tmp.Close();
                }

                using (Stream stream = File.OpenRead(nomeArquivo))
                {
                    DadosBCB dadoRecuperadosBCB = null;

                    if (!(stream.Length == 0))
                    {
                        dadoRecuperadosBCB = (DadosBCB)binario.Deserialize(stream);
                    }

                    textoTabela += ConstStringHtml.abreTabela + ConstStringHtml.abreTR;

                    textoTabela += Formatador.PrintaCabecalho(relatorio.Cabecalho);

                    textoTabela += "</tr>";
                    textoTabela += MontaTextoTabela(ref mudadas, dadosBCB, dadoRecuperadosBCB);
                    textoTabela += "</table>";
                }

                using (Stream stream = File.OpenWrite(nomeArquivo))
                {
                    binario.Serialize(stream, dadosBCB);
                }

                somaMudadas += mudadas;
                if (mudadas == 0)
                {
                    textoTabela = string.Empty;

                    if (i == relatorio.NumeroTabelas && somaMudadas == 0)
                    {
                        textoTabela = string.Format(ConstStringHtml.nenuhmaAlteracao, relatorio.Nome);
                    }
                }

                textoTabelas += textoTabela;
            }

            return !(somaMudadas == 0);
        }

        public static string MontaTextoTabela(ref int semMudanca, DadosBCB dadosBCB, DadosBCB dadoRecuperadosBCB)
        {
            string texto = string.Empty;
            List<LinhaDadosBCB> listaMudancas = new List<LinhaDadosBCB>();

            listaMudancas = dadoRecuperadosBCB != null ? dadoRecuperadosBCB.RetonaListaDiferentes(dadosBCB) : dadosBCB.Linhas.ToList();

            foreach (LinhaDadosBCB linhaMudada in listaMudancas)
            {
                texto += "<tr>" + Formatador.PrintaLinha(linhaMudada) + "</tr>";
            }

            if ((listaMudancas.Count() > 0))
            {
                semMudanca++;
            }

            return texto;
        }

    }
}
