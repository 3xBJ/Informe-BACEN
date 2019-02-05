using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace BcbCrawler
{
    public class Crawler
    {
       private const string leiauteDRM = "https://www.bcb.gov.br/fis/pstaw10/leiauteDRM.asp?frame=1";
       private const string leiauteDDR = "https://www.bcb.gov.br/fis/pstaw10/leiauteDDR.asp?frame=1";
       private const string leiauteDLO = "https://www.bcb.gov.br/fis/pstaw10/leiaute_limitesdlo.asp?frame=1";
       private const string xPath = "//body[1]/div[1]/table[{0}]/tbody[1]/tr";

        public static IEnumerable<HtmlNodeCollection> RetornaNodeHTML(ETipoDocumento tipoDocumento, int numeroTabelas)
        {
            string paginaRenderizada = string.Empty;
            ChromeOptions opcoes = new ChromeOptions();
            opcoes.AddArgument("--headless");

            using (ChromeDriver driver = new ChromeDriver(opcoes))
            {
                string url = tipoDocumento == ETipoDocumento.DRM ? leiauteDRM : leiauteDDR;

                if (tipoDocumento == ETipoDocumento.DLO)
                {
                    url = leiauteDLO;
                }

                driver.Navigate().GoToUrl(url);

                try
                {
                    paginaRenderizada = driver.PageSource;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(paginaRenderizada);


            List<HtmlNodeCollection> noTabelas = new List<HtmlNodeCollection>();
            for (int i = 1; i < numeroTabelas + 1; i++)
            {
                string path = string.Format(xPath, i);
                yield return html.DocumentNode.SelectNodes(path);
            }
        }
    }
}
