using BcbCrawler.Interfaces;
using HtmlAgilityPack;

namespace BcbCrawler.Relatorios
{
    public class DLO : IRelatorio
    {
        #region Fields

        private const string nome = "DLO";

        private const string url = "https://www.bcb.gov.br/estabilidadefinanceira/leiautedoc2061e2071";

        private const string xPath = "//table[@id='tableatual_{0}']//tbody";

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

        public HtmlNodeCollection Html { get; set; }
    }
}
