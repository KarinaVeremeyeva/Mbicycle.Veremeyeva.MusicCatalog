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
            string sqlExpression = string.Format($"INSERT INTO Songs (Name, GenreId, PerformerId, AlbumId)" +
                $" VALUES (@Name, @GenreId, @PerformerId, @AlbumId)");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter nameParam = new SqlParameter("@Name", song.Name);
                SqlParameter genreIdParam = new SqlParameter("@GenreId", song.GenreId);
                SqlParameter performerIdParam = new SqlParameter("@PerformerId", song.PerformerId);
                SqlParameter albumIdParam = new SqlParameter("@AlbumId", song.AlbumId);

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
            string sqlExpression = string.Format($"DELETE FROM Songs WHERE SongId='{id}'");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
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
            string sqlExpression = string.Format($"SELECT * FROM Songs");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

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
            string sqlExpression = string.Format($"SELECT * FROM Songs WHERE SongId='{id}'");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

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
            string sqlExpression = string.Format($"UPDATE Songs SET Name=@Name, GenreId=@GenreId," +
                $" PerformerId=@PerformerId, AlbumId=@AlbumId  WHERE SongId=@SongId");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@SongId", song.SongId);
                SqlParameter nameParam = new SqlParameter("@Name", song.Name);
                SqlParameter genreIdParam = new SqlParameter("@GenreId", song.GenreId);
                SqlParameter performerIdParam = new SqlParameter("@PerformerId", song.PerformerId);
                SqlParameter albumIdParam = new SqlParameter("@AlbumId", song.AlbumId);

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
