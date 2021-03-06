﻿using BcbCrawler.Interfaces;
using HtmlAgilityPack;

namespace BcbCrawler.Relatorios
{
    public class DRM : IRelatorio
    {
        #region Fields

        private const string nome = "DRM";

        private const string url = "https://www.bcb.gov.br/fis/pstaw10/leiauteDRM.asp?frame=1";

        private const string xPath = "//body[1]/div[1]/table[{0}]/tbody[1]/tr";

        private readonly string[] nomeTabelas = new string[3] { "Informações técnicas (2040/2060):",
                                                                "Esquemas de validação do DRM (2040/2060):",
                                                                "Anexos e Instruções de Preenchimentos:"
                                                              };

        private readonly string[] cabecalho = new string[3] { "Título", "Data do Arquivo", "Arquivo" };

        private const int numeroTabelas = 3;

        #endregion

        public string Nome => nome;

        public string Url => url;

        public string XPath => xPath;

        public string[] NomeTabelas => nomeTabelas;

        public string[] Cabecalho => cabecalho;

        public int NumeroTabelas => numeroTabelas;

        public HtmlNodeCollection Html { get; set; }
    }

}
