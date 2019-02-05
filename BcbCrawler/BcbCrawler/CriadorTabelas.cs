using BcbCrawler.Util;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace BcbCrawler
{
    class CriadorTabelas
    {
        #region Fields

        static readonly string abreTabela = "<table style = 'border-collapse: collapse; text-align: center'> ";
        static readonly string abreTR = "<tr style = 'background-color: #dddddd'>";
        static readonly string tituloTabela = "<h4> {0} </h4></br></br>";
        static readonly string nomeBin = "RelatorioBCB_{0}_tabela{1}.bin";

        #endregion

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
                string textoTabela = string.Format(tituloTabela, nomeTabelas[i]);
                bool ehLinhaDeQuatro = nomeTabelas[i].Equals("Leiautes:") || nomeTabelas[i].Equals("Exemplos:");
                int mudadas = 0;

                i++;
                if (noTabela == null)
                {
                    textoTabela += Formatador.PrintaLinha("Erro na leitura do nó", "Verifique o serviço", "Tabela " + i.ToString());
                    break;
                }

                string nomeArquivo = string.Format(nomeBin, tipoDocumento, i);

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

                    textoTabela += abreTabela + abreTR;
                    if (!ehLinhaDeQuatro)
                    {
                        textoTabela += Formatador.PrintaCabecalho("Titulo", "Data do Arquivo", "Arquivo");
                    }
                    else
                    {
                        textoTabela += Formatador.PrintaCabecalho("Titulo", "Documento", "Data do Arquivo", "Arquivo");
                    }
                    textoTabela += "</tr>";
                    textoTabela += MontaTextoTabela(ref mudadas, dadosBCB, dadoRecuperadosBCB, ehLinhaDeQuatro);
                    textoTabela += "</table>";
                    textoTabela += Environment.NewLine + Environment.NewLine;
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
                        textoTabela = string.Format("Nenhuma alteração nas tabelas do {0}. </br>", tipoDocumento);
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
