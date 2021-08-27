using MusicCatalog.DataAccess.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MusicCatalog.DataAccess.Repositories
{
    /// <summary>
    /// A concrete repository for albums
    /// </summary>
    public class AlbumRepository : Repository<Album>
    {
        /// <summary>
        /// Albums repository
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public AlbumRepository(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Creates album
        /// </summary>
        /// <param name="album">New album</param>
        public override void Create(Album album)
        {
            var sqlExpression = string.Format($"INSERT INTO Albums (Name, ReleaseDate) VALUES (@Name, @ReleaseDate)");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(sqlExpression, connection);
                var nameParam = new SqlParameter("@Name", album.Name);
                var dateParam = new SqlParameter("@ReleaseDate", album.ReleaseDate);

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
            var sqlExpression = string.Format($"DELETE FROM Albums WHERE AlbumId='{id}'");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);

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
            var sqlExpression = string.Format($"SELECT * FROM Albums");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);
                var reader = command.ExecuteReader();

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
            var sqlExpression = string.Format($"SELECT * FROM Albums WHERE AlbumId='{id}'");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);
                var reader = command.ExecuteReader();

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
            var sqlExpression = string.Format($"UPDATE Albums SET Name=@Name, ReleaseDate=@ReleaseDate WHERE AlbumId=@AlbumId");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(sqlExpression, connection);
                var idParam = new SqlParameter("@AlbumId", album.AlbumId);
                var nameParam = new SqlParameter("@Name", album.Name);
                var dateParam = new SqlParameter("@ReleaseDate", album.ReleaseDate);

                command.Parameters.Add(idParam);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(dateParam);
                command.ExecuteNonQuery();
            }
        }
    }
}
