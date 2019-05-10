using BcbCrawler.Interfaces;
using BcbCrawler.Relatorios;
using BcbCrawler.Util;
using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace BcbCrawler
{
    public class Crawler
    {
        private string paginaRenderizada = string.Empty;
        private string paginaNormas = string.Empty;

        private string RetornaPaginaRenderizada(string url)
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
                    //se não esperar ele recupera o HTML errado.
                    System.Threading.Thread.Sleep(1000);
                    paginaRenderizada = driver.PageSource;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return paginaRenderizada;
        }

        public IEnumerable<HtmlNodeCollection> RetornaNodeRelatorio(IRelatorio relatorio)
        {
            if (string.IsNullOrEmpty(paginaRenderizada))
            {
                paginaRenderizada = RetornaPaginaRenderizada(relatorio.Url);
            }

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(paginaRenderizada);

            List<HtmlNodeCollection> noTabelas = new List<HtmlNodeCollection>();
            for (int i = 1; i < relatorio.NumeroTabelas + 1; i++)
            {
                string path = string.Format(relatorio.XPath, relatorio is DLO ? i - 1 : i);

                yield return html.DocumentNode.SelectNodes(path);
            }
        }

        public HtmlNodeCollection RetornaNodeNormas(out string url)
        {
            DateTime hoje = DateTime.Now;
            hoje = hoje.AddDays(-2);
            DateTime diaInicial = hoje.DayOfWeek.Equals(DayOfWeek.Monday) ? hoje.AddDays(-3) : hoje.AddDays(-1);

            url = string.Format(ConstNormas.url, diaInicial.Day, diaInicial.Month, diaInicial.Year, hoje.Day, hoje.Month, hoje.Year);

            paginaNormas = RetornaPaginaRenderizada(url);

            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(paginaNormas);

            List<HtmlNodeCollection> noTabelas = new List<HtmlNodeCollection>();

            return html.DocumentNode.SelectNodes(ConstNormas.xPathDoc);
        }
    }
}
