using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace FPL.Pages.Players
{
    public class IndexModel : PageModel
    {
        public List<PlayerInfo> listPlayers = new list<PlayerInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=fpl;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PlayerInfo playerInfo = new PlayerInfo();
                                playerInfo.id = "" + reader.GetInt32(0);
                                playerInfo.name = reader.GetString(1);
                                playerInfo.email = reader.GetString(2);
                                playerInfo.phone = reader.GetString(3);
                                playerInfo.fpl_id = reader.GetString(4);
                                playerInfo.semester = reader.GetString(5);
                                playerInfo.created_at = reader.GetDateTime(6).ToString();

                                listPlayers.Add(playerInfo);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {

            }
        }

    }
    public class PlayerInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String fpl_id;
        public String semester;
        public String created_at;
    }
}
