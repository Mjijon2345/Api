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
                List<Hotel> hotels = new List<Hotel>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM hoteles", connection))
                    {
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

                                hotels.Add(hotel);
                            }
                        }
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, hotels);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET: api/Hotels/5
        public IHttpActionResult Get(int id)
            {
                Hotel hotel = null;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM hoteles WHERE IdHotel = @IdHotel", connection))
                    {
                        cmd.Parameters.AddWithValue("@IdHotel", id);

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

                if (hotel == null)
                {
                    return NotFound();
                }

                return Ok(hotel);
            }

            // POST: api/Hotels
            public IHttpActionResult Post([FromBody] Hotel hotel)
            {
                if (hotel == null)
                {
                    return BadRequest("El hotel no puede ser nulo.");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO hoteles (TitleHotel, DescriptionHotel, Price) VALUES (@TitleHotel, @DescriptionHotel, @Price); SELECT SCOPE_IDENTITY()", connection))
                    {
                        cmd.Parameters.AddWithValue("@TitleHotel", hotel.TitleHotel);
                        cmd.Parameters.AddWithValue("@DescriptionHotel", hotel.DescriptionHotel);
                        cmd.Parameters.AddWithValue("@Price", hotel.Price ?? (object)DBNull.Value);

                        int newId = Convert.ToInt32(cmd.ExecuteScalar());
                        hotel.IdHotel = newId;
                    }
                }

                return CreatedAtRoute("DefaultApi", new { id = hotel.IdHotel }, hotel);
            }

            // PUT: api/Hotels/5
            public IHttpActionResult Put(int id, [FromBody] Hotel hotel)
            {
                if (hotel == null)
                {
                    return BadRequest("El hotel no puede ser nulo.");
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE hoteles SET TitleHotel = @TitleHotel, DescriptionHotel = @DescriptionHotel, Price = @Price WHERE IdHotel = @IdHotel", connection))
                    {
                        cmd.Parameters.AddWithValue("@IdHotel", id);
                        cmd.Parameters.AddWithValue("@TitleHotel", hotel.TitleHotel);
                        cmd.Parameters.AddWithValue("@DescriptionHotel", hotel.DescriptionHotel);
                        cmd.Parameters.AddWithValue("@Price", hotel.Price ?? (object)DBNull.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            return NotFound();
                        }
                    }
                }

                return Ok(hotel);
            }

            // DELETE: api/Hotels/5
            public IHttpActionResult Delete(int id)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM hoteles WHERE IdHotel = @IdHotel", connection))
                    {
                        cmd.Parameters.AddWithValue("@IdHotel", id);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            return NotFound();
                        }
                    }
                }

                return Ok();
            }
        }
 }

