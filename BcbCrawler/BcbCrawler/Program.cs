using BcbCrawler;
using BcbCrawler.Util;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

/*  TODO:
 *  - Arrumar as linhas quando envia por e-mail
 *  o Adicionar quarta coluna nas linhas
 *  - Inserir formatação html?
 *  - Passar para o serviço
 *  
 */
namespace WebDriverTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] nomeTabelasDDR = new string[1] { "Informações técnicas do documento 2011:" };
            string[] nomeTabelasDRM = new string[3] { "Informações técnicas (2040/2060):", "Esquemas de validação do DRM (2040/2060):", "Anexos e Instruções de Preenchimentos:" };
            string[] nomeTabelasDLO = new string[7] { "Instruções de preenchimento:", "Modelos:", "Leiautes:", "Exemplos:", "Esquemas de validação do DLO", "Críticas de processamento:",
                                                      "RPS - Regime Prudencial Simplificado - Cooperativas"};

            string textoEmail = string.Empty;
            string assunto = "Houve mudanças nos relatórios!!!";

            bool naoteveMudancas = true;
            foreach (ETipoDocumento tipoDocumento in (ETipoDocumento[])Enum.GetValues(typeof(ETipoDocumento)))
            {
                int numeroTabelas = (int)tipoDocumento;
                string[] nomeTabelas = tipoDocumento == ETipoDocumento.DDR ? nomeTabelasDDR : nomeTabelasDRM;
                string textoTabela = string.Empty;

                if (tipoDocumento == ETipoDocumento.DLO)
                {
                    nomeTabelas = nomeTabelasDLO;
                }

                bool tmp = VerificaMudanca(tipoDocumento, numeroTabelas, nomeTabelas, ref textoTabela);
                naoteveMudancas = naoteveMudancas && !tmp;
                textoEmail += textoTabela;
            }
            
            if (naoteveMudancas)
            {
                assunto = "Nenhuma mudança hoje.";
                textoEmail += "God's in his heaven. All's Right with the world.";
            }

            Email.EnviarEmail(assunto, textoEmail);
            Console.Write(textoEmail); //só para ver saida

            Console.Read(); //só para ver saida
        }

        private static bool VerificaMudanca(ETipoDocumento tipoDocumento, int numeroTabelas, string[] nomeTabelas, ref string textoTabelas)
        {
            Crawler.RetornaNodeHTML(tipoDocumento, numeroTabelas);
            return  MontaTextoESalvaArquivo(tipoDocumento, numeroTabelas, ref textoTabelas, nomeTabelas);
        }

        private static bool MontaTextoESalvaArquivo(ETipoDocumento tipoDocumento, int numeroTabelas, ref string textoTabelas, string[] nomeTabelas)
        {
            int i = 0;
            int somaMudadas = 0;
            BinaryFormatter binario = new BinaryFormatter();

            foreach (HtmlNodeCollection noTabela in Crawler.RetornaNodeHTML(tipoDocumento, numeroTabelas))
            {
                string textoTabela = nomeTabelas[i] + Environment.NewLine + Environment.NewLine;
                bool ehLinhaDeQuatro = nomeTabelas[i].Equals("Leiautes:") || nomeTabelas[i].Equals("Exemplos:");
                int mudadas = 0;

                i++;
                if (noTabela == null)
                {
                    textoTabela += Formatador.PrintaLinha("Erro na leitura do nó", "Verifique o serviço", "Tabela " + i.ToString());
                    break;
                }

                string nomeArquivo = string.Format("RelatorioBCB_{0}_tabela{1}.bin", tipoDocumento, i);

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

                    if (!ehLinhaDeQuatro)
                    {
                        textoTabela += Formatador.PrintaLinha("Titulo", "Data do Arquivo", "Arquivo");
                    }
                    else
                    {
                        textoTabela += Formatador.PrintaLinha("Titulo", "Data" ,"Data do Arquivo", "Arquivo");                        
                    }

                    textoTabela += MontaTextoTabela(ref mudadas, dadosBCB, dadoRecuperadosBCB, ehLinhaDeQuatro);
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
                        textoTabela = string.Format("Nenhuma alteração nas tabelas do {0}.", tipoDocumento) + Environment.NewLine + Environment.NewLine;
                    }                      
                }

                textoTabelas += textoTabela;
            }
            
            return !(somaMudadas == 0);
        }

        private static string MontaTextoTabela(ref int semMudanca, DadosBCB dadosBCB, DadosBCB dadoRecuperadosBCB, bool ehLinhaDeQuatro)
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
                if (!ehLinhaDeQuatro)
                {
                    textoEmail += Formatador.PrintaLinha(linhaMudada.Titulo, linhaMudada.Documento, linhaMudada.DataDoArquivo, linhaMudada.LinkArquivo);
                }
                else
                {
                    textoEmail += Formatador.PrintaLinha(linhaMudada.Titulo, linhaMudada.DataDoArquivo, linhaMudada.LinkArquivo);
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
