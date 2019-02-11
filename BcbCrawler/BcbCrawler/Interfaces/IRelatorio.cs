using HtmlAgilityPack;

namespace BcbCrawler.Interfaces
{
    public interface IRelatorio
    {
        string Nome { get; }

        string Url { get;}

        string XPath {get; }

        string[] NomeTabelas { get; }

        string[] Cabecalho { get; }

        string[] Classe { get; }

        int NumeroTabelas { get; }

        HtmlNodeCollection Html { get; set; }
    }
}
