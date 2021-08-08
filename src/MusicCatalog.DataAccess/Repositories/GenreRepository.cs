using MusicCatalog.DataAccess.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MusicCatalog.DataAccess.Repositories
{
    public class GenreRepository : Repository<Genre>
    {
        public GenreRepository(string connectionStr) : base(connectionStr)
        {
        }

        public override void Create(Genre genre)
        {
            string sqlExpression = string.Format($"INSERT INTO Genres (Name) VALUES ('{genre.Name}')");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

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

        public override void Update(Genre genre)
        {
            string sqlExpression = string.Format($"UPDATE Genres SET Name='{genre.Name}' WHERE GenreId={genre.GenreId}");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
