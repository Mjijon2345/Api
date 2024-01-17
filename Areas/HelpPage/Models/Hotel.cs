using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace Api.Areas.HelpPage.Models
{
    public class Hotel
    {
        public int IdHotel { get; set; }
        public string TitleHotel { get; set; }
        public string DescriptionHotel { get; set; }
        public decimal? Price { get; set; }

        // Método para guardar un hotel en la base de datos
        public void GuardarEnBaseDeDatos()
        {
            // Configuración de la cadena de conexión
            string connectionString = "Data Source=MATEO;Initial Catalog=API;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Insertar datos en la base de datos
                using (SqlCommand cmd = new SqlCommand("INSERT INTO hoteles (IdHotel, TitleHotel, DescriptionHotel, Price) VALUES (@IdHotel, @TitleHotel, @DescriptionHotel, @Price)", conn))
                {
                    cmd.Parameters.AddWithValue("@IdHotel", IdHotel);
                    cmd.Parameters.AddWithValue("@TitleHotel", TitleHotel);
                    cmd.Parameters.AddWithValue("@DescriptionHotel", DescriptionHotel);
                    cmd.Parameters.AddWithValue("@Price", Price ?? (object)DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
