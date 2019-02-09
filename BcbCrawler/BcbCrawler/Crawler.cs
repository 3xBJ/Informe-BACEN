using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using BcbCrawler.Util;
using BcbCrawler.Interfaces;

namespace BcbCrawler
{
    public class Crawler
    {
        public static IEnumerable<HtmlNodeCollection> RetornaNodeHTML(IRelatorio relatorio)
        {
            string paginaRenderizada = string.Empty;
            
            ChromeOptions opcoes = new ChromeOptions();
            opcoes.AddArgument("--headless");

            using (ChromeDriver driver = new ChromeDriver(opcoes))
            {
                driver.Navigate().GoToUrl(relatorio.Url);

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
            for (int i = 1; i < relatorio.NumeroTabelas + 1; i++)
            {
                string path = string.Format(relatorio.XPath, relatorio is DLO ? relatorio.Classe[i - 1] : i.ToString());
                yield return html.DocumentNode.SelectNodes(path);
            }
        }
    }
}
