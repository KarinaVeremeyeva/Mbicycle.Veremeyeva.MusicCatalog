using MusicCatalog.DataAccess.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MusicCatalog.DataAccess.Repositories
{
    /// <summary>
    /// A concrete repository for performers
    /// </summary>
    public class PerformerRepository : Repository<Performer>
    {
        /// <summary>
        /// Performers repository
        /// </summary>
        /// <param name="connectionStr">Connection string</param>
        public PerformerRepository(string connectionStr) : base(connectionStr)
        {
        }

        /// <summary>
        /// Creates performer
        /// </summary>
        /// <param name="performer">New performer</param>
        public override void Create(Performer performer)
        {
            var sqlExpression = string.Format($"INSERT INTO Performers (Name) VALUES (@Name)");
            
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(sqlExpression, connection);
                var nameParam = new SqlParameter("@Name", performer.Name);

                command.Parameters.Add(nameParam);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates performer
        /// </summary>
        /// <param name="performer">Performer to update</param>
        public override void Update(Performer performer)
        {
            var sqlExpression = string.Format($"UPDATE Performers SET Name=@Name WHERE PerformerId=@PerformerId");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(sqlExpression, connection);
                var idParam = new SqlParameter("@PerformerId", performer.PerformerId);
                var nameParam = new SqlParameter("@Name", performer.Name);

                command.Parameters.Add(idParam);
                command.Parameters.Add(nameParam);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes performer by id
        /// </summary>
        /// <param name="id">Performer id to delete</param>
        public override void Delete(int id)
        {
            var sqlExpression = string.Format($"DELETE FROM Performers WHERE PerformerId='{id}'");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Returns all performers
        /// </summary>
        /// <returns>Performers</returns>
        public override IEnumerable<Performer> GetAll()
        {
            var performers = new List<Performer>();
            var sqlExpression = string.Format($"SELECT * FROM Performers");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);
                var reader = command.ExecuteReader();

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

        /// <summary>
        /// Returns performer by id
        /// </summary>
        /// <param name="id">Performer id</param>
        /// <returns>Performer with expecting id</returns>
        public override Performer GetById(int id)
        {
            var performer = new Performer();
            var sqlExpression = string.Format($"SELECT * FROM Performers WHERE PerformerId='{id}'");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(sqlExpression, connection);
                var reader = command.ExecuteReader();

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
