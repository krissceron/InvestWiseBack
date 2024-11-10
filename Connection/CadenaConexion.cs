namespace InvestWiseProyecto.Connection
{
    public class CadenaConexion
    {
        //private static string servidor = @"camilacabrera.database.windows.net";
        private static string servidor = @"host.docker.internal\SQLEXPRESS";
        private static string base_tip = "InvestWise";
        private static string usuario = "sa";
        private static string password = "2003";
        //private static string usuario = "sa";
        //private static string password = "sasa";
        public static string RetornaCadenaConexion()
        {

            return "Data Source=" + servidor + ";Initial Catalog=" + base_tip + ";User ID=" + usuario + ";Password=" + password;
        }
    }
}
