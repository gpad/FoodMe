using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace FoodMe.Core
{
    public class SqlOrderRepository : IOrderRepository
    {
        private string connectionString;

        public SqlOrderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task SaveAsync(Order order)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                // await sqlConnection.ExecuteAsync(
                //     "insert into order (Id) values (@Id)",
                //     new { Id = order.Id });
            }
        }
    }
}
