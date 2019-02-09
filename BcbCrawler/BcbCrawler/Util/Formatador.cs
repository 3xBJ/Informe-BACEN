namespace BcbCrawler.Util
{
    public static class Formatador
    {
        public static string PrintaCabecalho(string[] campos, int i = 0, string texto = "")
        {
            if (i == campos.Length) return texto;

            texto += ConstStringHtml.abreTH + campos[i] + ConstStringHtml.espaco + "</th>";
            i++;

            return PrintaCabecalho(campos, i, texto);
        }

        public static string PrintaLinha(LinhaDadosBCB campos)
        {
            string texto = ConstStringHtml.abreTDEsquerda + campos.Titulo  + ConstStringHtml.espaco + "</td>" +
                           ConstStringHtml.abreTD         + campos.Coluna2 + ConstStringHtml.espaco + "</td>" +
                           ConstStringHtml.abreTD         + campos.Coluna3 + ConstStringHtml.espaco + "</td>";

            if (campos.Coluna4 != string.Empty)
            {
                texto += ConstStringHtml.abreTD + campos.Coluna4 + ConstStringHtml.espaco + "</td>";
            }

            return texto;
        }
    }
}