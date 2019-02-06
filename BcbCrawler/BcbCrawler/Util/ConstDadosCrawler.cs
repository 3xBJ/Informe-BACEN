using System.Collections.Generic;

namespace BcbCrawler.Util
{
    public class ConstDadosCrawler
    {
        #region url

        public const string urlDDR  = "https://www.bcb.gov.br/fis/pstaw10/leiauteDDR.asp?frame=1";
        public const string urlDRM  = "https://www.bcb.gov.br/fis/pstaw10/leiauteDRM.asp?frame=1";
        public const string urlDLO  = "https://www.bcb.gov.br/fis/pstaw10/leiaute_limitesdlo.asp?frame=1";
        public const string urlDLO2 = "https://www.bcb.gov.br/estabilidadefinanceira/leiautedoc2061e2071";

        #endregion

        #region Nome Tabelas

        public static readonly string[] nomeTabelasDDR = new string[1] { "Informações técnicas do documento 2011:" };

        public static readonly string[] nomeTabelasDRM = new string[3] { "Informações técnicas (2040/2060):",
                                                                          "Esquemas de validação do DRM (2040/2060):",
                                                                          "Anexos e Instruções de Preenchimentos:"
                                                                        };

        public static readonly string[] nomeTabelasDLO = new string[7] { "Instruções de preenchimento:",
                                                                           "Modelos:",
                                                                           "Leiautes:",
                                                                           "Exemplos:",
                                                                           "Esquemas de validação do DLO",
                                                                           "Críticas de processamento:",
                                                                           "RPS - Regime Prudencial Simplificado - Cooperativas"
                                                                        };

        public static readonly string[] nomeTabelasDLO2 = new string[3] { "Informações técnicas:",
                                                                           "Críticas de processamento (2061/2071)",
                                                                           "Esquemas de validação do DLO (2061/2071)"
                                                                         };

        #endregion

        #region Cabeçalhos

        public static readonly string[] cabecalho     = new string[3] { "Título", "Data do Arquivo", "Arquivo" };
        public static readonly string[] cabecalhoDLO  = new string[4] { "Título", "Documento", "Data do Arquivo", "Arquivo" };
        public static readonly string[] cabecalhoDLO2 = new string[4] { "Título", "Data-base início", "Data-base fim", "Data do arquivo" };

        #endregion
    }
}
