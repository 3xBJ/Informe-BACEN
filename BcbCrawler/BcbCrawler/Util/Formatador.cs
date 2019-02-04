using System;

namespace BcbCrawler.Util
{
    public static class Formatador
    {
        static readonly int largura1 = 108;
        static readonly int largura2 = 25;
        static readonly int largura3 = 17;
        static readonly int largura4 = 25;

        public static string PrintaSeparador(int largura)
        {
            return new string('-', largura);
        }

        public static string PrintaLinha(string titulo, string data, string arquivo)
        {
            string linha = "|";

            linha += AlinharCentro(titulo, largura1) + "|";
            linha += AlinharCentro(data, largura2) + "|";
            linha += AlinharCentro(arquivo, largura3) + "|";
            linha += Environment.NewLine;
            linha += PrintaSeparador(largura1 + largura2 + largura3 + 4);
            linha += Environment.NewLine;

            return linha;
        }

        public static string PrintaLinha(string titulo, string documento, string data, string arquivo)
        {
            string linha = "|";

            linha += AlinharCentro(titulo, largura1) + "|";
            linha += AlinharCentro(documento, largura4) + "|";
            linha += AlinharCentro(data, largura2) + "|";
            linha += AlinharCentro(arquivo, largura3) + "|";
            linha += Environment.NewLine;
            linha += PrintaSeparador(largura1 + largura2 + largura3 + largura4 + 4);
            linha += Environment.NewLine;

            return linha;
        }

        public static string AlinharCentro(string texto, int largura)
        {
            texto = texto.Length > largura ? texto.Substring(0, largura - 3) + "..." : texto;

            if (string.IsNullOrEmpty(texto))
            {
                return new string(' ', largura);
            }
            else
            {
                return texto.PadRight(largura - (largura - texto.Length) / 2).PadLeft(largura);
            }
        }
    }
}

