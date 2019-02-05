using BcbCrawler.Util;
using System;

namespace BcbCrawler
{
    class FazTudo
    {
        #region Fields
        
        private static readonly string[] nomeTabelasDDR = new string[1] { "Informações técnicas do documento 2011:" };
        private static readonly string[] nomeTabelasDRM = new string[3] { "Informações técnicas (2040/2060):", "Esquemas de validação do DRM (2040/2060):", "Anexos e Instruções de Preenchimentos:" };
        private static readonly string[] nomeTabelasDLO = new string[7] { "Instruções de preenchimento:", "Modelos:", "Leiautes:", "Exemplos:", "Esquemas de validação do DLO", "Críticas de processamento:",
                                                      "RPS - Regime Prudencial Simplificado - Cooperativas"};

        #endregion

        static void Main(string[] args)
        {           
            string textoEmail = string.Empty;
            bool naoteveMudancas = true;

            ETipoDocumento[] listaRelatorios = (ETipoDocumento[])Enum.GetValues(typeof(ETipoDocumento));

            foreach (ETipoDocumento tipoDocumento in listaRelatorios)
            {
                int numeroTabelas = (int)tipoDocumento;
                string[] nomeTabelas = tipoDocumento == ETipoDocumento.DDR ? nomeTabelasDDR : nomeTabelasDRM;
                string textoTabela = string.Empty;

                if (tipoDocumento == ETipoDocumento.DLO)
                {
                    nomeTabelas = nomeTabelasDLO;
                }

                bool tabelasMudaram = CriadorTabelas.VerificaMudanca(tipoDocumento, numeroTabelas, nomeTabelas, ref textoTabela);
                naoteveMudancas = naoteveMudancas && !tabelasMudaram;
                textoEmail += textoTabela;
            }

            string assunto = "Houve mudanças nos relatórios!!!";
            if (naoteveMudancas)
            {
                assunto = "Nenhuma mudança hoje.";
                textoEmail += "<br><br><br><br><font color='red'> God's in his heaven. All's Right with the world.</font>";
            }

            Email.EnviarEmail(assunto, textoEmail);
        }
    }
}
