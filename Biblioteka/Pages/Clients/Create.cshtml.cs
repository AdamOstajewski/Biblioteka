using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Biblioteka.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.title = Request.Form["title"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.returnn = Request.Form["returnn"];

            if (clientInfo.name.Length == 0 || clientInfo.title.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.returnn.Length == 0)
            {
                errorMessage = "Musisz uzupe³niæ wszystkie luki";
                return;
            }

            //zapis nowego klienta w bazie danych
            try
            {
                String connectionString = "Data Source=DESKTOP-V55J9BD;Initial Catalog=biblioteka;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients " + "(name, title, phone, returnn) VALUES " + "(@name, @title, @phone, @returnn);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
						command.Parameters.AddWithValue("@title", clientInfo.title);
						command.Parameters.AddWithValue("@phone", clientInfo.phone);
						command.Parameters.AddWithValue("@returnn", clientInfo.returnn);

                        command.ExecuteNonQuery();
					}
                                    
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;

                
            }

            clientInfo.name = ""; clientInfo.title = ""; clientInfo.phone = ""; clientInfo.returnn = "";
            successMessage = "Nowy Klient Zosta³ Dodany Pomyœlnie";


        }
    }
}
