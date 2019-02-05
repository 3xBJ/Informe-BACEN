namespace BcbCrawler.Util
{
    public static class Formatador
    {
        static readonly string espaco = "&ensp;";
        static readonly string abreTDEsquerda = "<td style = 'border: 1px solid #dddddd; text-align: left'>";
        static readonly string abreTD = "<td style = 'border: 1px solid #dddddd'>";
        static readonly string abreTH = "<th style = 'border: 1px solid #dddddd'>";

        public static string PrintaCabecalho(string titulo, string data, string arquivo)
        {
            string linha =  abreTH + titulo  + espaco + "</th>" +
                            abreTH + data    + espaco + "</th>" +
                            abreTH + arquivo + espaco + "</th>";
            return linha;
        }

        public static string PrintaCabecalho(string titulo, string documento, string data, string arquivo)
        {
            string linha =  abreTH + titulo    + espaco + "</th>" +
                            abreTH + documento + espaco + "</th>" +
                            abreTH + data      + espaco + "</th>" +
                            abreTH + arquivo   + espaco + "</th>";
            return linha;
        }

        public static string PrintaLinha(string titulo, string data, string arquivo)
        {
            string linha =  abreTDEsquerda + titulo  + espaco + "</td>" +
                            abreTD         + data    + espaco + "</td>" +
                            abreTD         + arquivo + espaco + "</td>";
            return linha;
        }

        public static string PrintaLinha(string titulo, string documento, string data, string arquivo)
        {
            string linha =  abreTDEsquerda + titulo    + espaco + "</td>" +
                            abreTD         + documento + espaco + "</td>" +
                            abreTD         + data      + espaco + "</td>" +
                            abreTD         + arquivo   + espaco + "</td>";
            return linha;
        }
    }
}

