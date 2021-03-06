﻿using BcbCrawler.Interfaces;
using HtmlAgilityPack;

namespace BcbCrawler.Util
{
    public class DLO : IRelatorio
    {
        #region Fields

        private const string nome = "DLO";

        private const string url = "https://www.bcb.gov.br/estabilidadefinanceira/leiautedoc2061e2071";

        private const string xPath = "//div[@class='{0}']//tbody//tr";

        private string[] classe = new string[3] { "ExternalClassBB69604781914FED9C6294E519F58EE6" ,
                                                  "ExternalClassFE28F6BFF2C649438133F7343E9969A9",
                                                  "ExternalClass2B89B04033534D30B6311E296228A55E"
                                                };

        private readonly string[] nomeTabelas = new string[4] { "Informações técnicas:",
                                                                "RPS - Regime Prudencial Simplificado - S5",
                                                                "Críticas de processamento (2061/2071)",
                                                                "Esquemas de validação do DLO (2061/2071)"
                                                              };

        private readonly string[] cabecalho = new string[4] { "Título", "Data-base início", "Data-base fim", "Data do arquivo" };

        private const int numeroTabelas = 4;

        #endregion

        public string Nome => nome;

        public string Url => url;

        public string XPath => xPath;

        public string[] NomeTabelas => nomeTabelas;

        public string[] Cabecalho => cabecalho;
       
        public int NumeroTabelas => numeroTabelas;

        public string[] Classe => classe;

        public HtmlNodeCollection Html { get; set; }
    }
}
