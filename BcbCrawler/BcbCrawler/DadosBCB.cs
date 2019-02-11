using BcbCrawler.Interfaces;
using BcbCrawler.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BcbCrawler
{
    [Serializable]
    public class DadosBCB
    {
        public LinhaDadosBCB[] Linhas { get; private set; }
        public DateTime DataVerificacao;

        public DadosBCB(IRelatorio relatorio)
        {
            MontaLinha(relatorio);
            DataVerificacao = DateTime.Now;
        }

        private void MontaLinha(IRelatorio relatorio)
        {
            int j = relatorio is DLO ? 0 : 1; 

            var linhasHtml = relatorio.Html.Skip(j).Select(tr => tr
                .Elements("td")
                .Select(td => td.InnerText.Trim())
                .ToArray());

            int numeroDeLinhas = linhasHtml.Count();

            Linhas = new LinhaDadosBCB[numeroDeLinhas];

            if (!(relatorio is DLO))
            {
                for (int i = 0; i < numeroDeLinhas; i++)
                {
                    string[] linhaArray = linhasHtml.ElementAt(i);

                    //se for rodapé
                    if (linhaArray.Length == 1)
                    {
                        Linhas[i] = new LinhaDadosBCB(linhaArray[0]);
                        continue;
                    }

                    Linhas[i] = new LinhaDadosBCB(linhaArray[0],
                                                  linhaArray[1],
                                                  linhaArray[2]);
                }
            }
            else
            {
                for (int i = 0; i < numeroDeLinhas; i++)
                {
                    string[] linhaArray = linhasHtml.ElementAt(i);

                    Linhas[i] = new LinhaDadosBCB(linhaArray[0],
                                                  linhaArray[1],
                                                  linhaArray[2],
                                                  linhaArray[3]);
                }
            }

        }

        public List<LinhaDadosBCB> RetonaListaDiferentes(DadosBCB dado)
        {
            List<LinhaDadosBCB> linhasDiferentes = new List<LinhaDadosBCB>();
            int numeroDeLinhas = this.Linhas.Count();
            int numeroDeLDados = dado.Linhas.Count();

            for (int i = 0; i < numeroDeLDados; i++)
            {
                LinhaDadosBCB linhaDado = dado.Linhas[i];
                if (this.Linhas[i].Compara(linhaDado))
                {
                    linhasDiferentes.Add(linhaDado);
                }
            }

            //Ou seja, adicionaram novas linhas na tabela do site
            if (numeroDeLinhas > numeroDeLDados)
            {
                int numeroLinhasNovas = numeroDeLinhas - dado.Linhas.Count();

                for (int i = 1; i < numeroLinhasNovas + 1; i++)
                {
                    linhasDiferentes.Add(dado.Linhas[numeroDeLinhas + i]);
                }
            }

            return linhasDiferentes;
        }
    }

    [Serializable]
    public class LinhaDadosBCB
    {
        public string Titulo { get; private set; }
        public string Coluna2 { get; private set; } = string.Empty;
        public string Coluna3 { get; private set; } = string.Empty;
        public string Coluna4 { get; private set; } = string.Empty;

        #region Construtores

        public LinhaDadosBCB(string titulo)
        {
            Titulo = titulo;
        }

        public LinhaDadosBCB(string titulo, string dataDoArquivo, string linkArquivo)
        {
            Titulo = titulo;
            Coluna2 = dataDoArquivo;
            Coluna3 = linkArquivo;
        }

        public LinhaDadosBCB(string titulo, string DataBaseInicio, string DataBaseFim, string DataDoArquivo)
        {
            Titulo = titulo;
            Coluna2 = DataBaseInicio;
            Coluna3 = DataBaseFim;
            Coluna4 = DataDoArquivo;
        }

        #endregion

        public bool Compara(LinhaDadosBCB linhaDado)
        {
                   
            return this.Titulo  != linhaDado.Titulo  ||
                   this.Coluna2 != linhaDado.Coluna2 ||
                   this.Coluna3 != linhaDado.Coluna3 ||
                   this.Coluna4 != linhaDado.Coluna4;
        }
    }
}
