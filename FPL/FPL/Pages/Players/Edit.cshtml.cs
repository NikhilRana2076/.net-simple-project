using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace FPL.Pages.Players
{
    public class EditModel : PageModel
    {
        public PlayerInfo playerInfo = new PlayerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=fpl;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                        
                                playerInfo.id = "" + reader.GetInt32(0);
                                playerInfo.name = reader.GetString(1);
                                playerInfo.email = reader.GetString(2);
                                playerInfo.phone = reader.GetString(3);
                                playerInfo.fpl_id = reader.GetString(4);
                                playerInfo.semester = reader.GetString(5);
                                playerInfo.created_at = reader.GetDateTime(6).ToString();
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            playerInfo.id = Request.Form["id"];
            playerInfo.name = Request.Form["name"];
            playerInfo.email = Request.Form["email"];
            playerInfo.phone = Request.Form["phone"];
            playerInfo.fpl_id = Request.Form["fpl_id"];
            playerInfo.semester = Request.Form["semester"];


            if (playerInfo.name.Length == 0 || playerInfo.name.Length == 0 ||
                playerInfo.email.Length == 0 ||  playerInfo.phone.Length == 0 || 
                playerInfo.fpl_id.Length == 0 || playerInfo.semester.Length == 0)
            {
                errorMessage = "All the fields are required to be filled";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=fpl;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients " +
                        "SET name=@name, email=@email, phone=@phone, fpl_id=@fpl_id, semester=@semester" +
                        "WHERE id = @id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", playerInfo.name);
                        command.Parameters.AddWithValue("@email", playerInfo.email);
                        command.Parameters.AddWithValue("@phone", playerInfo.phone);
                        command.Parameters.AddWithValue("@fpl_id", playerInfo.fpl_id);
                        command.Parameters.AddWithValue("@semester", playerInfo.semester);
                        command.Parameters.AddWithValue("@id", playerInfo.id);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Players/Index");
        }
    }
}
