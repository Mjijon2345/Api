using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Areas.HelpPage.Models;

namespace Api.Areas.HelpPage.Controllers
{
    public class HotelsController : ApiController
    {
        private readonly string connectionString = "Data Source=MATEO;Initial Catalog=API;Integrated Security=True";

        // GET: api/Hotels
        public HttpResponseMessage Get()
        {
            try
            {
                // Inicializa una lista para almacenar objetos Hotel
                List<Hotel> hotels = new List<Hotel>();

                // Establece una conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Ejecuta una consulta SQL para recuperar todos los hoteles
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM hoteles", connection))
                    {
                        // Lee datos desde SqlDataReader y popula objetos Hotel
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Hotel hotel = new Hotel
                                {
                                    IdHotel = (int)reader["IdHotel"],
                                    TitleHotel = reader["TitleHotel"].ToString(),
                                    DescriptionHotel = reader["DescriptionHotel"].ToString(),
                                    Price = reader["Price"] is DBNull ? null : (int?)reader["Price"]
                                };

                                // Agrega el objeto Hotel poblado a la lista
                                hotels.Add(hotel);
                            }
                        }
                    }
                }

                // Devuelve una respuesta con la lista de hoteles
                return Request.CreateResponse(HttpStatusCode.OK, hotels);
            }
            catch (Exception ex)
            {
                // Maneja las excepciones y devuelve una respuesta de error interno del servidor
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/Hotels/5
        public IHttpActionResult Get(int id)
        {
            // Inicializa un objeto Hotel
            Hotel hotel = null;

            // Establece una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Ejecuta una consulta SQL parametrizada para recuperar un hotel específico por Id
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM hoteles WHERE IdHotel = @IdHotel", connection))
                {
                    // Agrega un parámetro para el Id del hotel
                    cmd.Parameters.AddWithValue("@IdHotel", id);

                    // Lee datos desde SqlDataReader y popula el objeto Hotel
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hotel = new Hotel
                            {
                                IdHotel = (int)reader["IdHotel"],
                                TitleHotel = reader["TitleHotel"].ToString(),
                                DescriptionHotel = reader["DescriptionHotel"].ToString(),
                                Price = (decimal?)reader["Price"]
                            };
                        }
                    }
                }
            }

            // Verifica si se encontró el hotel y devuelve una respuesta apropiada
            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        // POST: api/Hotels
        public IHttpActionResult Post([FromBody] Hotel hotel)
        {
            // Verifica si el objeto hotel proporcionado es nulo
            if (hotel == null)
            {
                return BadRequest("El hotel no puede ser nulo.");
            }

            // Establece una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Ejecuta una consulta SQL para insertar un nuevo hotel y obtener su Id
                using (SqlCommand cmd = new SqlCommand("INSERT INTO hoteles (TitleHotel, DescriptionHotel, Price) VALUES (@TitleHotel, @DescriptionHotel, @Price); SELECT SCOPE_IDENTITY()", connection))
                {
                    // Agrega parámetros para las propiedades del hotel
                    cmd.Parameters.AddWithValue("@TitleHotel", hotel.TitleHotel);
                    cmd.Parameters.AddWithValue("@DescriptionHotel", hotel.DescriptionHotel);
                    cmd.Parameters.AddWithValue("@Price", hotel.Price ?? (object)DBNull.Value);

                    // Ejecuta la consulta y obtiene el nuevo Id
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());
                    hotel.IdHotel = newId;
                }
            }

            // Devuelve una respuesta con el hotel recién creado y su Id
            return CreatedAtRoute("DefaultApi", new { id = hotel.IdHotel }, hotel);
        }

        // PUT: api/Hotels/5
        public IHttpActionResult Put(int id, [FromBody] Hotel hotel)
        {
            // Verifica si el objeto hotel proporcionado es nulo
            if (hotel == null)
            {
                return BadRequest("El hotel no puede ser nulo.");
            }

            // Establece una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Ejecuta una consulta SQL parametrizada para actualizar un hotel existente por Id
                using (SqlCommand cmd = new SqlCommand("UPDATE hoteles SET TitleHotel = @TitleHotel, DescriptionHotel = @DescriptionHotel, Price = @Price WHERE IdHotel = @IdHotel", connection))
                {
                    // Agrega parámetros para las propiedades del hotel y el Id
                    cmd.Parameters.AddWithValue("@IdHotel", id);
                    cmd.Parameters.AddWithValue("@TitleHotel", hotel.TitleHotel);
                    cmd.Parameters.AddWithValue("@DescriptionHotel", hotel.DescriptionHotel);
                    cmd.Parameters.AddWithValue("@Price", hotel.Price ?? (object)DBNull.Value);

                    // Ejecuta la consulta y verifica el número de filas afectadas
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Si no se afectaron filas, el hotel no se encontró
                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                }
            }

            // Devuelve una respuesta con el hotel actualizado
            return Ok(hotel);
        }

        // DELETE: api/Hotels/5
        public IHttpActionResult Delete(int id)
        {
            // Establece una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Ejecuta una consulta SQL parametrizada para eliminar un hotel por Id
                using (SqlCommand cmd = new SqlCommand("DELETE FROM hoteles WHERE IdHotel = @IdHotel", connection))
                {
                    // Agrega un parámetro para el Id del hotel
                    cmd.Parameters.AddWithValue("@IdHotel", id);

                    // Ejecuta la consulta y verifica el número de filas afectadas
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Si no se afectaron filas, el hotel no se encontró
                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                }
            }

            // Devuelve una respuesta indicando una eliminación exitosa
            return Ok();
        }
    }
}
