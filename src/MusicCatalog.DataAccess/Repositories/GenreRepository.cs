using MusicCatalog.DataAccess.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MusicCatalog.DataAccess.Repositories
{
    /// <summary>
    /// A concrete repository for genres
    /// </summary>
    public class GenreRepository : Repository<Genre>
    {
        public GenreRepository(string connectionStr) : base(connectionStr)
        {
        }

        /// <summary>
        /// Creates genre
        /// </summary>
        /// <param name="genre">New genre</param>
        public override void Create(Genre genre)
        {
            string sqlExpression = string.Format($"INSERT INTO Genres (Name) VALUES (@Name)");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter nameParam = new SqlParameter("@Name", genre.Name);
                command.Parameters.Add(nameParam);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates genre
        /// </summary>
        /// <param name="genre">Genre to update</param>
        public override void Update(Genre genre)
        {
            string sqlExpression = string.Format($"UPDATE Genres SET Name=@Name WHERE GenreId=@GenreId");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@GenreId", genre.GenreId);
                SqlParameter nameParam = new SqlParameter("@Name", genre.Name);
                command.Parameters.Add(idParam);
                command.Parameters.Add(nameParam);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes genre by id
        /// </summary>
        /// <param name="id">Genre id to update</param>
        public override void Delete(int id)
        {
            string sqlExpression = string.Format($"DELETE FROM Genres WHERE GenreId='{id}'");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Returns all genres
        /// </summary>
        /// <returns>Genres</returns>
        public override IEnumerable<Genre> GetAll()
        {
            var genres = new List<Genre>();
            string sqlExpression = string.Format($"SELECT * FROM Genres");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var genre = new Genre();
                        genre.GenreId = reader.GetInt32(0);
                        genre.Name = reader.GetString(1);
                        genres.Add(genre);
                    }
                }

                reader.Close();
            }

            return genres;
        }

        /// <summary>
        /// Returns genre by id
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>Genre with expecting id</returns>
        public override Genre GetById(int id)
        {
            var genre = new Genre();
            string sqlExpression = string.Format($"SELECT * FROM Genres WHERE GenreId='{id}'");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        genre.GenreId = reader.GetInt32(0);
                        genre.Name = reader.GetString(1);
                    }
                }
            }

            return genre;
        }
    }
}