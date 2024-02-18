using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Biblioteka.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=DESKTOP-V55J9BD;Initial Catalog=biblioteka;Integrated Security=True;";
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
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.title = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.returnn = reader.GetString(4);
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
            clientInfo.id = Request.Form["id"];
            clientInfo.name = Request.Form["name"];
            clientInfo.title = Request.Form["title"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.returnn= Request.Form["returnn"];

            if (clientInfo.id.Length == 0 || clientInfo.name.Length == 0 || clientInfo.title.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.returnn.Length == 0)
            {
                errorMessage = "Musisz uzupe³niæ wszystkie luki";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-V55J9BD;Initial Catalog=biblioteka;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients " + "SET name=@name, title=@title, phone=@phone, returnn=@returnn WHERE id=@id";

                    using (SqlCommand command = new SqlCommand (sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
						command.Parameters.AddWithValue("@title", clientInfo.title);
						command.Parameters.AddWithValue("@phone", clientInfo.phone);
						command.Parameters.AddWithValue("@returnn", clientInfo.returnn);
						command.Parameters.AddWithValue("@id", clientInfo.id);

                        command.ExecuteNonQuery();
					}
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clients/Index");
        }
    }
}
