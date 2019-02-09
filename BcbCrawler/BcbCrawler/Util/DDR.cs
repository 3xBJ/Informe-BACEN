using BcbCrawler.Interfaces;
using HtmlAgilityPack;

namespace BcbCrawler.Util
{
    class DDR : IRelatorio
    {
        #region Fields

        private const string nome = "DDR";

        public const string url = "https://www.bcb.gov.br/fis/pstaw10/leiauteDDR.asp?frame=1";

        public const string xPath = "//body[1]/div[1]/table[{0}]/tbody[1]/tr";

        public static readonly string[] nomeTabelas = new string[1] { "Informações técnicas do documento 2011:" };

        public static readonly string[] cabecalho = new string[3] { "Título", "Data do Arquivo", "Arquivo" };

        public const int numeroTabelas = 1;

        #endregion

        public string Nome => nome;

        public string Url => url;

        public string XPath => xPath;

        public string[] NomeTabelas => nomeTabelas;

        public string[] Cabecalho => cabecalho;

        public int NumeroTabelas => numeroTabelas;

        public string[] Classe => null;

        public HtmlNodeCollection Html { get; set; }
    }
}
