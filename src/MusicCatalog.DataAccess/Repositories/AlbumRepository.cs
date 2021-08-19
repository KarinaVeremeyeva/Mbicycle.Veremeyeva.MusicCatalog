using MusicCatalog.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MusicCatalog.DataAccess.Repositories
{
    /// <summary>
    /// A concrete repository for albums
    /// </summary>
    public class AlbumRepository : Repository<Album>
    {
        public AlbumRepository(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Creates album
        /// </summary>
        /// <param name="album">New album</param>
        public override void Create(Album album)
        {
            string sqlExpression = string.Format($"INSERT INTO Albums (Name, ReleaseDate) VALUES (@Name, @ReleaseDate)");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter nameParam = new SqlParameter("@Name", album.Name);
                SqlParameter dateParam = new SqlParameter("@ReleaseDate", album.ReleaseDate);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(dateParam);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes album by id
        /// </summary>
        /// <param name="id">Album with expecting id</param>
        public override void Delete(int id)
        {
            string sqlExpression = string.Format($"DELETE FROM Albums WHERE AlbumId='{id}'");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Returns all albums
        /// </summary>
        /// <returns>Albums</returns>
        public override IEnumerable<Album> GetAll()
        {
            var albums = new List<Album>();
            string sqlExpression = string.Format($"SELECT * FROM Albums");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var album = new Album();
                        album.AlbumId = reader.GetInt32(0);
                        album.Name = reader.GetString(1);
                        album.ReleaseDate = reader.GetDateTime(2);
                        albums.Add(album);
                    }
                }

                reader.Close();
            }

            return albums;
        }

        /// <summary>
        /// Returns album by id
        /// </summary>
        /// <param name="id">Album id</param>
        /// <returns>Album with expecting id</returns>
        public override Album GetById(int id)
        {
            var album = new Album();
            string sqlExpression = string.Format($"SELECT * FROM Albums WHERE AlbumId='{id}'");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        album.AlbumId = reader.GetInt32(0);
                        album.Name = reader.GetString(1);
                        album.ReleaseDate = reader.GetDateTime(2);
                    }
                }
            }

            return album;
        }

        /// <summary>
        /// Updates album
        /// </summary>
        /// <param name="album">Album to update</param>
        public override void Update(Album album)
        {
            string sqlExpression = string.Format($"UPDATE Albums SET Name=@Name, ReleaseDate=@ReleaseDate WHERE AlbumId=@AlbumId");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@GenreId", album.AlbumId);
                SqlParameter nameParam = new SqlParameter("@Name", album.Name);
                SqlParameter dateParam = new SqlParameter("@ReleaseDate", album.ReleaseDate);
                command.Parameters.Add(idParam);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(dateParam);
                command.ExecuteNonQuery();
            }
        }
    }
}
