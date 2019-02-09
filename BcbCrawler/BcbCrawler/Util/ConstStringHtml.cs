namespace BcbCrawler.Util
{
    public class ConstStringHtml
    {
        #region CriadorTabelas

        public static readonly string tituloEUrl       = "<h3> {0} </h3> <a href='{1}'>{1}</a>";
        public static readonly string nomeBin          = "RelatorioBCB_{0}_tabela{1}.bin";
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

        #endregion
    }
}
