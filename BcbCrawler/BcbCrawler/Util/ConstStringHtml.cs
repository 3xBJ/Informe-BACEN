namespace BcbCrawler.Util
{
    public class ConstStringHtml
    {
        #region CriadorTabelas

        public static readonly string tituloEUrl       = "<h3> {0} </h3>  <br> <a href='{1}'>{1}</a> <br>";
        public static readonly string nomeBinTabelas   = "RelatorioBCB_{0}_tabela{1}.bin";
        public static readonly string nomeBinNormas    = "Normas.bin";
        public static readonly string abreTR           = "<tr style = 'background-color: #dddddd'>";
        public static readonly string abreTabela       = "<table style = 'border-collapse: collapse; text-align: center'> ";
        public static readonly string tituloTabela     = "<h4> {0} </h4></br></br>";
        public static readonly string nenuhmaAlteracao = "Nenhuma alteração nas tabelas do {0}. </br>";

        #endregion

        #region Formatador

        public static readonly string espaco         = "&ensp;";
        public static readonly string abreTH         = "<th style = 'border: 1px solid #dddddd'>";
        public static readonly string abreTD         = "<td style = 'border: 1px solid #dddddd'>";
        public static readonly string abreTDEsquerda = "<td style = 'border: 1px solid #dddddd; text-align: left'>";

        public static readonly string textoNorma     = "<strong>Título:              </strong> {0}" + "<br>" +
                                                       "<strong>Data/Hora Documento: </strong> {1}" + "<br>" +
                                                       "<strong>Assunto:             </strong> {2}" + "<br>" +
                                                       "<strong>Responsável:         </strong>{3}" + "<br><br>";
        #endregion
    }
}
