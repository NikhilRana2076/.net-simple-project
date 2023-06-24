using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Windows.Input;

namespace FPL.Pages.Players
{
    public class CreateModel : PageModel
    {
        public PlayerInfo playerInfo = new PlayerInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            playerInfo.name = Request.Form["name"];
            playerInfo.email = Request.Form["email"];
            playerInfo.phone = Request.Form["phone"];
            playerInfo.fpl_id = Request.Form["fpl_id"];
            playerInfo.semester = Request.Form["semester"];

            if (playerInfo.name.Length == 0 || playerInfo.email.Length == 0 ||
                playerInfo.phone.Length == 0 || playerInfo.fpl_id.Length == 0 ||
                playerInfo.semester.Length == 0)
            {
                errorMessage = "All the fields are required to be filled";
                return;
            }

            //save the new player info into the database

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=fpl;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                  {
                    connection.Open();
                    String sql = "INSERT INTO clients " +
                                    "(name, email, phone, fpl_id, semester) VALUES " +
                                    "(@name, @email, @phone, @fpl_id, @semester);";

                    using (SqlCommand command = new SqlCommand(sql, connection) )
                    {
                        command.Parameters.AddWithValue("@name", playerInfo.name);
                        command.Parameters.AddWithValue("@email", playerInfo.email);
                        command.Parameters.AddWithValue("@phone", playerInfo.phone);
                        command.Parameters.AddWithValue("@fpl_id", playerInfo.fpl_id);
                        command.Parameters.AddWithValue("@semester", playerInfo.semester);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            playerInfo.name = ""; playerInfo.email = ""; playerInfo.phone = "";
            playerInfo.fpl_id = ""; playerInfo.semester = "";
            successMessage = "New Player Added Correctly";

            Response.Redirect("/Players/Index");

        }
    }
}
