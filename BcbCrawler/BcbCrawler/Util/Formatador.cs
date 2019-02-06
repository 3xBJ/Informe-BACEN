namespace BcbCrawler.Util
{
    public static class Formatador
    {
        public static string PrintaCabecalho(string campo1, string campo2, string campo3)
        {
            return ConstStringHtml.abreTH + campo1 + ConstStringHtml.espaco + "</th>" +
                   ConstStringHtml.abreTH + campo2 + ConstStringHtml.espaco + "</th>" +
                   ConstStringHtml.abreTH + campo3 + ConstStringHtml.espaco + "</th>" ;
        }

        public static string PrintaCabecalho(string campo1, string campo2, string campo3, string campo4)
        {
            return  ConstStringHtml.abreTH + campo1 + ConstStringHtml.espaco + "</th>" +
                    ConstStringHtml.abreTH + campo2 + ConstStringHtml.espaco + "</th>" +
                    ConstStringHtml.abreTH + campo3 + ConstStringHtml.espaco + "</th>" +
                    ConstStringHtml.abreTH + campo4 + ConstStringHtml.espaco + "</th>" ;
        }

        public static string PrintaLinha(string campo1, string campo2, string campo3)
        {
            return ConstStringHtml.abreTDEsquerda + campo1 + ConstStringHtml.espaco + "</td>" +
                   ConstStringHtml.abreTD         + campo2 + ConstStringHtml.espaco + "</td>" +
                   ConstStringHtml.abreTD         + campo3 + ConstStringHtml.espaco + "</td>" ;
        }

        public static string PrintaLinha(string campo1, string campo2, string campo3, string campo4)
        {
            return ConstStringHtml.abreTDEsquerda + campo1 + ConstStringHtml.espaco + "</td>" +
                   ConstStringHtml.abreTD         + campo2 + ConstStringHtml.espaco + "</td>" +
                   ConstStringHtml.abreTD         + campo3 + ConstStringHtml.espaco + "</td>" +
                   ConstStringHtml.abreTD         + campo4 + ConstStringHtml.espaco + "</td>" ;
        }
    }
}

