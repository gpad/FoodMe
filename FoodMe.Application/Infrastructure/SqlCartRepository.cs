using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace FoodMe.Core
{
    public class SqlCartRepository : ICartRepository
    {
        private string connectionString;

        public SqlCartRepository(string dbConnectionString)
        {
            this.connectionString = dbConnectionString;
        }
        public async Task SaveAsync(Cart cart)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.OpenAsync();
                using (var transaction = sqlConnection.BeginTransaction(IsolationLevel.Serializable))
                {
                    await sqlConnection.ExecuteAsync(
                        "insert carts (id) values (@Id)", new{ Id = cart.Id},
                        transaction: transaction);
                    await sqlConnection.ExecuteAsync(
                        "insert cart_items (id, product_id, description, qty) values (@Id, @ProductId, @Description, @Quantity)",
                        cart.Items,
                        transaction: transaction);
                    await transaction.CommitAsync();
                }
            }
        }
    }
}
