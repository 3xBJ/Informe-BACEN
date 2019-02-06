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
        public static bool VerificaMudanca(ETipoDocumento tipoDocumento, int numeroTabelas, string[] nomeTabelas, ref string textoTabelas)
        {
            Crawler.RetornaNodeHTML(tipoDocumento, numeroTabelas);
            return MontaTextoESalvaArquivo(tipoDocumento, numeroTabelas, ref textoTabelas, nomeTabelas);
        }

        public static bool MontaTextoESalvaArquivo(ETipoDocumento tipoDocumento, int numeroTabelas, ref string textoTabelas, string[] nomeTabelas)
        {
            int i = 0;
            int somaMudadas = 0;
            BinaryFormatter binario = new BinaryFormatter();

            string comeco = "<h3> {0} </h3>";
            textoTabelas += string.Format(comeco, tipoDocumento);
            foreach (HtmlNodeCollection noTabela in Crawler.RetornaNodeHTML(tipoDocumento, numeroTabelas))
            {
                int mudadas = 0;

                string textoTabela = string.Format(ConstStringHtml.tituloTabela, nomeTabelas[i]);

                //no objeto TipoDocumento, colocar os valores do i para qual a linha é de quatro!
                bool ehLinhaDeQuatro = nomeTabelas[i].Equals(ConstDadosCrawler.nomeTabelasDLO[2])
                                    || nomeTabelas[i].Equals(ConstDadosCrawler.nomeTabelasDLO[3])
                                    || tipoDocumento == ETipoDocumento.DLO2;

                //Não mova esse i++ de lugar!!
                i++; 

                if (noTabela == null)
                {
                    textoTabela += Formatador.PrintaLinha("Erro na leitura do nó", "Verifique o serviço", "Tabela " + i.ToString());
                    break;
                }

                string nomeArquivo = string.Format(ConstStringHtml.nomeBin, tipoDocumento, i);

                DadosBCB dadosBCB = new DadosBCB(noTabela, tipoDocumento, ehLinhaDeQuatro);

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

                    if (!ehLinhaDeQuatro)
                    {
                        textoTabela += Formatador.PrintaCabecalho(ConstDadosCrawler.cabecalho[0],
                                                                  ConstDadosCrawler.cabecalho[1],
                                                                  ConstDadosCrawler.cabecalho[2]);
                    }
                    else if(tipoDocumento == ETipoDocumento.DLO2)
                    {
                        textoTabela += Formatador.PrintaCabecalho(ConstDadosCrawler.cabecalhoDLO[0],
                                                                  ConstDadosCrawler.cabecalhoDLO[1],
                                                                  ConstDadosCrawler.cabecalhoDLO[2],
                                                                  ConstDadosCrawler.cabecalhoDLO[3]);
                    }
                    else
                    {
                        textoTabela += Formatador.PrintaCabecalho(ConstDadosCrawler.cabecalhoDLO2[0], 
                                                                  ConstDadosCrawler.cabecalhoDLO2[1],
                                                                  ConstDadosCrawler.cabecalhoDLO2[2],
                                                                  ConstDadosCrawler.cabecalhoDLO2[3]);
                    }

                    textoTabela += "</tr>";
                    textoTabela += MontaTextoTabela(ref mudadas, dadosBCB, dadoRecuperadosBCB, ehLinhaDeQuatro);
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

                    if (i == numeroTabelas && somaMudadas == 0)
                    {
                        textoTabela = string.Format(ConstStringHtml.nenuhmaAlteracao, tipoDocumento);
                    }
                }

                textoTabelas += textoTabela;
            }

            return !(somaMudadas == 0);
        }

        public static string MontaTextoTabela(ref int semMudanca, DadosBCB dadosBCB, DadosBCB dadoRecuperadosBCB, bool ehLinhaDeQuatro)
        {
            string textoEmail = string.Empty;
            List<LinhaDadosBCB> listaMudancas = new List<LinhaDadosBCB>();

            if (dadoRecuperadosBCB != null)
            {
                listaMudancas = dadoRecuperadosBCB.RetonaListaDiferentes(dadosBCB);
            }
            else
            {
                listaMudancas = dadosBCB.Linhas.ToList();
            }

            foreach (LinhaDadosBCB linhaMudada in listaMudancas)
            {
                if (ehLinhaDeQuatro)
                {
                    textoEmail += "<tr>" + Formatador.PrintaLinha(linhaMudada.Titulo, linhaMudada.Documento, linhaMudada.DataDoArquivo, linhaMudada.LinkArquivo) + "</tr>";
                }
                else
                {
                    textoEmail += "<tr>" + Formatador.PrintaLinha(linhaMudada.Titulo, linhaMudada.DataDoArquivo, linhaMudada.LinkArquivo) + "</tr>";
                }
            }

            if ((listaMudancas.Count() > 0))
            {
                semMudanca++;
            }

            return textoEmail;
        }

    }
}
