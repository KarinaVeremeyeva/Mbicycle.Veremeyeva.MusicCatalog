using MusicCatalog.DataAccess.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MusicCatalog.DataAccess.Repositories
{
    public class PerformerRepository : Repository<Performer>
    {
        public PerformerRepository(string connectionStr) : base(connectionStr)
        {
        }

        public override void Create(Performer performer)
        {
            string sqlExpression = string.Format($"INSERT INTO Performers (Name) VALUES (@Name)");
            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter nameParam = new SqlParameter("@Name", performer.Name);
                command.Parameters.Add(nameParam);
                command.ExecuteNonQuery();
            }
        }

        public override void Update(Performer performer)
        {
            string sqlExpression = string.Format($"UPDATE Performers SET Name=@Name WHERE PerformerId=@PerformerId");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlParameter idParam = new SqlParameter("@PerformerId", performer.PerformerId);
                SqlParameter nameParam = new SqlParameter("@Name", performer.Name);
                command.Parameters.Add(idParam);
                command.Parameters.Add(nameParam);
                command.ExecuteNonQuery();
            }
        }

        public override void Delete(int id)
        {
            string sqlExpression = string.Format($"DELETE FROM Performers WHERE PerformerId='{id}'");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }
        }

        public override IEnumerable<Performer> GetAll()
        {
            var performers = new List<Performer>();
            string sqlExpression = string.Format($"SELECT * FROM Performers");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var performer = new Performer();
                        performer.PerformerId = reader.GetInt32(0);
                        performer.Name = reader.GetString(1);
                        performers.Add(performer);
                    }
                }

                reader.Close();
            }

            return performers;
        }

        public override Performer GetById(int id)
        {
            var performer = new Performer();
            string sqlExpression = string.Format($"SELECT * FROM Performers WHERE PerformerId='{id}'");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        performer.PerformerId = reader.GetInt32(0);
                        performer.Name = reader.GetString(1);
                    }
                }
            }

            return performer;
        }
    }
}
