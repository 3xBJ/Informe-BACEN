using BcbCrawler.Interfaces;
using BcbCrawler.Util;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace BcbCrawler
{
    class MontadorDeTextos
    {
        public static string MontaTextoTabelas()
        {
            StringBuilder textoArquivo = new StringBuilder();

            List<IRelatorio> listaRelatorios = new List<IRelatorio>
            {
                new DLO(),
                new DDR(),
                new DRM()
            };

            foreach (IRelatorio relatorio in listaRelatorios)
            {
                textoArquivo.Append(MontaTabelasESalvaArquivo(relatorio));
            }

            return textoArquivo.ToString();
        }

        public static string MontaTabelasESalvaArquivo(IRelatorio relatorio)
        {
            int i = 0;
            int somaMudadas = 0;
            StringBuilder textoTabelas = new StringBuilder();
            BinaryFormatter binario = new BinaryFormatter();

            textoTabelas.Append(string.Format(ConstStringHtml.tituloEUrl, relatorio.Nome, relatorio.Url));

            foreach (HtmlNodeCollection noTabela in Crawler.RetornaNodeRelatorio(relatorio))
            {
                int mudadas = 0;

                StringBuilder textoTabela = new StringBuilder(string.Format(ConstStringHtml.tituloTabela, relatorio.NomeTabelas[i]));

                //Não mova esse i++ de lugar!!
                i++;

                if (noTabela == null)
                {
                    textoTabela.Append(Formatador.PrintaLinha(new LinhaDadosBCB("Erro na leitura do nó", "Verifique o serviço", "Tabela " + i.ToString())));
                    somaMudadas++;
                    continue;
                }

                relatorio.Html = noTabela;

                string nomeArquivo = string.Format(ConstStringHtml.nomeBinTabelas, relatorio.Nome, i);

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

                    textoTabela.Append(ConstStringHtml.abreTabela + ConstStringHtml.abreTR);

                    textoTabela.Append(Formatador.PrintaCabecalho(relatorio.Cabecalho) + "</tr>");

                    textoTabela.Append(Formatador.RetornaTextoTabela(ref mudadas, dadosBCB, dadoRecuperadosBCB) + "</table>");
                }

                using (Stream stream = File.OpenWrite(nomeArquivo))
                {
                    binario.Serialize(stream, dadosBCB);
                }

                somaMudadas += mudadas;
                if (mudadas == 0)
                {
                    textoTabela.Clear();

                    if (i == relatorio.NumeroTabelas && somaMudadas == 0)
                    {
                        textoTabela.Append(string.Format(ConstStringHtml.nenuhmaAlteracao, relatorio.Nome));
                    }
                }

                textoTabelas.Append(textoTabela);
            }

            return textoTabelas.ToString();
        }

        public static StringBuilder MontaTexoNormas()
        {
            BinaryFormatter binario = new BinaryFormatter();
            HtmlNodeCollection noNormas = Crawler.RetornaNodeNormas(out string url);
            StringBuilder textoFinal = new StringBuilder(string.Format(ConstStringHtml.tituloEUrl, "Normas", url));
            
            string[] linhasHtml = noNormas.Select(ol => ol
                    .Elements("li")
                    .Select(li => li.InnerText.Trim())
                    .ToArray()).First();

            foreach (string texto in linhasHtml)
            {
                string comunicado = Formatador.RetornaTextoIntervalo(ConstNormas.camposNorma[0], ConstNormas.camposNorma[1], texto);
                string dataHora = Formatador.RetornaTextoIntervalo(ConstNormas.camposNorma[1], ConstNormas.camposNorma[2], texto);
                string assunto = Formatador.RetornaTextoIntervalo(ConstNormas.camposNorma[2], ConstNormas.camposNorma[3], texto);
                string responsavel = Formatador.RetornaTextoIntervalo(ConstNormas.camposNorma[3], texto);

                Norma norma = new Norma(comunicado, dataHora, assunto, responsavel);

                textoFinal.Append(norma.MontaTexto());
            }

            return textoFinal;
        }
    }
}
