using BcbCrawler.Interfaces;
using BcbCrawler.Util;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace BcbCrawler
{
    public class Crawler
    {
        private static string RetornaPaginaRenderizada(string url)
        {
            string paginaRenderizada = string.Empty;

            ChromeOptions opcoes = new ChromeOptions();
            opcoes.AddArgument("--headless");

            using (ChromeDriver driver = new ChromeDriver(opcoes))
            {
                driver.Navigate().GoToUrl(url);
                
                try
                {
                    //A pagina das normas carrega uma pag antes da final
                    //precia esperar se não ele não recupera o html correto
                    System.Threading.Thread.Sleep(1);
                    paginaRenderizada = driver.PageSource;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return paginaRenderizada;
        }

        public static IEnumerable<HtmlNodeCollection> RetornaNodeRelatorio(IRelatorio relatorio)
        {
            string paginaRenderizada = RetornaPaginaRenderizada(relatorio.Url);

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(paginaRenderizada);

            List<HtmlNodeCollection> noTabelas = new List<HtmlNodeCollection>();
            for (int i = 1; i < relatorio.NumeroTabelas + 1; i++)
            {
                string path = string.Format(relatorio.XPath, relatorio is DLO ? relatorio.Classe[i - 1] : i.ToString());
                yield return html.DocumentNode.SelectNodes(path);
            }
        }

        public static HtmlNodeCollection RetornaNodeNormas(out string url)
        {
            DateTime hoje = DateTime.Now;
            hoje = hoje.AddDays(-2);
            DateTime diaInicial = hoje.DayOfWeek.Equals(DayOfWeek.Monday) ? hoje.AddDays(-3) : hoje.AddDays(-1);

            url = string.Format(ConstNormas.url, diaInicial.Day, diaInicial.Month, diaInicial.Year, hoje.Day, hoje.Month, hoje.Year);
            string paginaRenderizada = RetornaPaginaRenderizada(url);

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(paginaRenderizada);

            List<HtmlNodeCollection> noTabelas = new List<HtmlNodeCollection>();

            return html.DocumentNode.SelectNodes(ConstNormas.xPathDoc);
        }
    }
}
