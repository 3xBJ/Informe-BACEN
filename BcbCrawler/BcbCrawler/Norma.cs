using BcbCrawler.Util;
using System;

namespace BcbCrawler
{
    [Serializable]
    public class Norma
    {
        public string Comunicado { get; private set; }
        public string DataHora { get; private set; }
        public string Assunto { get; private set; }
        public string Responsavel { get; private set; }

        public Norma(string comunicado, string dataHora, string assunto, string responsavel)
        {
            Comunicado = comunicado;
            DataHora = dataHora;
            Assunto = assunto;
            Responsavel = responsavel;
        }

        public string MontaTexto()
        {
            return Formatador.PrintaNormas(this);
        }
    }
}
