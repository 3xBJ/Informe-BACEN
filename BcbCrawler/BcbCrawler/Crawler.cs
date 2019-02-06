using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using BcbCrawler.Util;

namespace BcbCrawler
{
    public class Crawler
    {
        /*  TODO:
         *  - passar html no lugar do tipo documento
         *  
         */
        public static IEnumerable<HtmlNodeCollection> RetornaNodeHTML(ETipoDocumento tipoDocumento, int numeroTabelas)
        {
            string paginaRenderizada = string.Empty;
            ChromeOptions opcoes = new ChromeOptions();
            opcoes.AddArgument("--headless");

            using (ChromeDriver driver = new ChromeDriver(opcoes))
            {
                //Isso aqui esta muito ruim, cria um monta obj tipoDocumento?
                string url = string.Empty;

                if (tipoDocumento == ETipoDocumento.DRM)
                {
                    url = ConstDadosCrawler.urlDRM;
                }
                else if (tipoDocumento == ETipoDocumento.DDR)
                {
                    url = ConstDadosCrawler.urlDDR;
                }
                else
                {
                    url = tipoDocumento == ETipoDocumento.DLO ? ConstDadosCrawler.urlDLO : ConstDadosCrawler.urlDLO2;
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
                string path = string.Format(ConstStringHtml.xPath, i);
                yield return html.DocumentNode.SelectNodes(path);
            }
        }
    }
}
