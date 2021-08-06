using MusicCatalog.DataAccess.Entities;
using System.Data.SqlClient;

namespace MusicCatalog.DataAccess.Repositories
{
    public class PerformerRepository : Repository<Performer>
    {
        public PerformerRepository(string connectionStr) : base(connectionStr)
        {
        }

        public new void Create(Performer performer)
        {
            string sqlExpression = string.Format($"INSERT INTO Performers (Name) VALUES ('{performer.Name}')");
            
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
