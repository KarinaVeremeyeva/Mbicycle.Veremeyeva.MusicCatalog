using MusicCatalog.DataAccess.Entities;
using System.Data.SqlClient;


namespace MusicCatalog.DataAccess.Repositories
{
    public class GenreRepository : Repository<Genre>
    {
        public GenreRepository(string connectionStr) : base(connectionStr)
        {
        }

        public new void Create(Genre genre)
        {
            string sqlExpression = string.Format($"INSERT INTO Genres (Name) VALUES ('{genre.Name}')");

            using (SqlConnection connection = new SqlConnection(ConnectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandText = sqlExpression;
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }
    }
}
