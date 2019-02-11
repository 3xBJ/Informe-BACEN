using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        public static string PrintaNormas(Norma norma)
        {
            return string.Format(ConstStringHtml.textoNorma, norma.Comunicado, norma.DataHora, norma.Assunto, norma.Responsavel);
        }

        public static string RetornaTextoIntervalo(string palavraInicio, string palavraFim, string texto)
        {
            string rgxTxt = string.Format(".*{0} (.*) {1}.*", palavraInicio, palavraFim);
            Regex regex = new Regex(rgxTxt);

            return regex.Match(texto).Groups[1].Value;
        }

        public static string RetornaTextoIntervalo(string palavra, string texto)
        {
            string rgxTxt = string.Format(".*{0} (.*)", palavra);
            Regex regex = new Regex(rgxTxt);

            return regex.Match(texto).Groups[1].Value;
        }

        public static string RetornaTextoTabela(ref int semMudanca, DadosBCB dadosBCB, DadosBCB dadoRecuperadosBCB)
        {
            string texto = string.Empty;
            List<LinhaDadosBCB> listaMudancas = new List<LinhaDadosBCB>();

            listaMudancas = dadoRecuperadosBCB != null ? dadoRecuperadosBCB.RetonaListaDiferentes(dadosBCB) : dadosBCB.Linhas.ToList();

            foreach (LinhaDadosBCB linhaMudada in listaMudancas)
            {
                texto += "<tr>" + Formatador.PrintaLinha(linhaMudada) + "</tr>";
            }

            if ((listaMudancas.Count > 0))
            {
                semMudanca++;
            }

            return texto;
        }
    }
}