using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BcbCrawler.Util
{
    static class ConstNormas
    {
        public const string xPathDoc = "//ol[@start='1']";

        public const string nome     = "Normas:";

        public const string url      = "https://www.bcb.gov.br/pre/normativos/busca/buscaNormativo.asp?tema=&startRow=0&refinadorTipo=&refinadorRevogado=&tipo=P&tipoDocumento=0&numero=&conteudo=&" +
                                       "dataInicioBusca={0}%2F{1}%2F{2}&" +
                                       "dataFimBusca={3}%2F{4}%2F{5}&frame=1";

        public static string[] camposNorma = new string[4] { "Título:", "Data/Hora Documento:", "Assunto:", "Responsável:" };
    }
}
