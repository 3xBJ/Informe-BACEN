using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BcbCrawler
{
    [Serializable]
    public class DadosBCB
    {
        public ETipoDocumento TipoDocumento { get; private set; }
        public LinhaDadosBCB[] Linhas { get; private set; }
        public DateTime DataVerificacao;

        public DadosBCB(HtmlNodeCollection html, ETipoDocumento tipoDocumento, bool linhaDeQuatro)
        {
            TipoDocumento = tipoDocumento;
            MontaLinha(html, linhaDeQuatro);
            DataVerificacao = DateTime.Now;
        }

        private void MontaLinha(HtmlNodeCollection html, bool linhaDeQuatro)
        {
            var linhasHtml = html.Skip(1).Select(tr => tr
                .Elements("td")
                .Select(td => td.InnerText.Trim())
                .ToArray());

            int numeroDeLinhas = linhasHtml.Count();

            Linhas = new LinhaDadosBCB[numeroDeLinhas];

            if (!linhaDeQuatro)
            {
                for (int i = 0; i < numeroDeLinhas; i++)
                {
                    string[] linhaArray = linhasHtml.ElementAt(i);

                    //se for rodapé
                    if (linhaArray.Length == 1)
                    {
                        Linhas[i] = new LinhaDadosBCB(linhaArray[0]);
                        break;
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
        public string DataDoArquivo { get; private set; }
        public string LinkArquivo { get; private set; }
        public string Documento { get; private set; }

        #region Construtores

        public LinhaDadosBCB(string titulo)
        {
            Titulo = titulo;
            DataDoArquivo = string.Empty;
            LinkArquivo = string.Empty;
            Documento = string.Empty;
        }

        public LinhaDadosBCB(string titulo, string dataDoArquivo, string linkArquivo)
        {
            Titulo = titulo;
            DataDoArquivo = dataDoArquivo;
            LinkArquivo = linkArquivo;
            Documento = string.Empty;
        }

        public LinhaDadosBCB(string titulo, string documento, string dataDoArquivo, string linkArquivo)
        {
            Titulo = titulo;
            DataDoArquivo = dataDoArquivo;
            LinkArquivo = linkArquivo;
            Documento = documento;
        }

        #endregion

        public bool Compara(LinhaDadosBCB linhaDado)
        {
            return this.Titulo != linhaDado.Titulo ||
                   this.LinkArquivo != linhaDado.LinkArquivo ||
                   this.DataDoArquivo != linhaDado.DataDoArquivo ||
                   this.Documento != linhaDado.Documento;
        }
    }
}
