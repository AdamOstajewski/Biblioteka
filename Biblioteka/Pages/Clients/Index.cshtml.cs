using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Biblioteka.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-V55J9BD;Initial Catalog=biblioteka;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Zmodyfikowane zapytanie SQL z klauzul¹ ORDER BY dla sortowania po returnn
                    String sql = "SELECT * FROM clients ORDER BY CONVERT(datetime, returnn, 103)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.title = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.returnn = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();

                                listClients.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public String id;
        public String name;
        public String title;
        public String phone;
        public String returnn;
        public String created_at;
    }
}
