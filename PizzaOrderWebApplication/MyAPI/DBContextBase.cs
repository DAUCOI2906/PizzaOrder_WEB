namespace PizzaOrderWebApplication.MyAPI
{
    public class DBContextBase
    {
        public static SqlConnection GetConnection()
        {
            string strConnection = ConfigurationManager.ConnectionStrings["PizzaDB"].ToString();
            SqlConnection conn = new SqlConnection(strConnection);
            return conn;
        }
    }
}