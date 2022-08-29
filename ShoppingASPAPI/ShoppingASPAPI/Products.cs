using System.Data.SqlClient;

namespace ShoppingASPAPI
{
    public class Products
    {
        public string GameName { get; set; }
        public string GameGenre { get; set; }

        SqlConnection con = new SqlConnection("Server=tcp:Nikhils-p2.database.windows.net,1433;Initial Catalog=retroStoreDB;Persist Security Info=False;User ID=trainer;Password=Password@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public List<Products> GameSearchByName(string gameName)
        {
            SqlCommand searchGame = new SqlCommand("select * from tbl_ProductInfo where productName = @productName", con);
            searchGame.Parameters.AddWithValue("@productName", gameName);
            con.Open();
            List<Products> gameList = new List<Products>();
            SqlDataReader readGame = searchGame.ExecuteReader();
            while (readGame.Read())
            {
                gameList.Add(new Products()
                {
                    GameName = readGame[0].ToString(),
                    GameGenre = readGame[1].ToString(),
                });
            }
            readGame.Close();
            con.Close();
            return gameList;
        }
    }
}
