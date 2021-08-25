using MusicCatalog.DataAccess.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MusicCatalog.DataAccess.Repositories
{
    /// <summary>
    /// A concrete repository for songs
    /// </summary>
    public class SongRepository : Repository<Song>
    {
        public SongRepository(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Creates song
        /// </summary>
        /// <param name="song">New song</param>
        public override void Create(Song song)
        {
            var sqlExpression = string.Format($"INSERT INTO Songs (Name, GenreId, PerformerId, AlbumId)" +
                $" VALUES (@Name, @GenreId, @PerformerId, @AlbumId)");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(sqlExpression, connection);
                var nameParam = new SqlParameter("@Name", song.Name);
                var genreIdParam = new SqlParameter("@GenreId", song.GenreId);
                var performerIdParam = new SqlParameter("@PerformerId", song.PerformerId);
                var albumIdParam = new SqlParameter("@AlbumId", song.AlbumId);

                command.Parameters.Add(nameParam);
                command.Parameters.Add(genreIdParam);
                command.Parameters.Add(performerIdParam);
                command.Parameters.Add(albumIdParam);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes song by id
        /// </summary>
        /// <param name="id">Song with expecting id</param>
        public override void Delete(int id)
        {
            var sqlExpression = string.Format($"DELETE FROM Songs WHERE SongId='{id}'");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Returns all songs
        /// </summary>
        /// <returns>Songs</returns>
        public override IEnumerable<Song> GetAll()
        {
            var songs = new List<Song>();
            var sqlExpression = string.Format($"SELECT * FROM Songs");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var song = new Song();
                        song.SongId = reader.GetInt32(0);
                        song.Name = reader.GetString(1);
                        song.GenreId = reader.GetInt32(2);
                        song.PerformerId = reader.GetInt32(3);
                        song.AlbumId = reader.GetInt32(4);
                        songs.Add(song);
                    }
                }

                reader.Close();
            }

            return songs;
        }

        /// <summary>
        /// Returns song by id
        /// </summary>
        /// <param name="id">Song id</param>
        /// <returns>Song with expecting id</returns>
        public override Song GetById(int id)
        {
            var song = new Song();
            var sqlExpression = string.Format($"SELECT * FROM Songs WHERE SongId='{id}'");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        song.SongId = reader.GetInt32(0);
                        song.Name = reader.GetString(1);
                        song.GenreId = reader.GetInt32(2);
                        song.PerformerId = reader.GetInt32(3);
                        song.AlbumId = reader.GetInt32(4);
                    }
                }
            }

            return song;
        }

        /// <summary>
        /// Updates song
        /// </summary>
        /// <param name="song">Song to update</param>
        public override void Update(Song song)
        {
            var sqlExpression = string.Format($"UPDATE Songs SET Name=@Name, GenreId=@GenreId," +
                $" PerformerId=@PerformerId, AlbumId=@AlbumId  WHERE SongId=@SongId");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(sqlExpression, connection);
                var idParam = new SqlParameter("@SongId", song.SongId);
                var nameParam = new SqlParameter("@Name", song.Name);
                var genreIdParam = new SqlParameter("@GenreId", song.GenreId);
                var performerIdParam = new SqlParameter("@PerformerId", song.PerformerId);
                var albumIdParam = new SqlParameter("@AlbumId", song.AlbumId);

                command.Parameters.Add(idParam);
                command.Parameters.Add(nameParam);
                command.Parameters.Add(genreIdParam);
                command.Parameters.Add(performerIdParam);
                command.Parameters.Add(albumIdParam);
                command.ExecuteNonQuery();
            }
        }
    }
}
