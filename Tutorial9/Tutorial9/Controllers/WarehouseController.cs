using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Tutorial9.Model.DTO;

namespace Tutorial9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=apbd;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    [HttpPost("add")]
    public async Task<IActionResult> AddProductToWarehouse([FromBody] ProductWarehouseDto request)
    {
        if (request.Amount <= 0)
            return BadRequest("Amount must be greater than 0");

        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        using var transaction = connection.BeginTransaction();

        try
        {
            var productExistsCmd = new SqlCommand("SELECT 1 FROM Product WHERE IdProduct = @IdProduct", connection, transaction);
            productExistsCmd.Parameters.AddWithValue("@IdProduct", request.IdProduct);
            var productExists = await productExistsCmd.ExecuteScalarAsync();

            if (productExists == null)
                return NotFound("Product not found");
            
            var warehouseExistsCmd = new SqlCommand("SELECT 1 FROM Warehouse WHERE IdWarehouse = @IdWarehouse", connection, transaction);
            warehouseExistsCmd.Parameters.AddWithValue("@IdWarehouse", request.IdWarehouse);
            var warehouseExists = await warehouseExistsCmd.ExecuteScalarAsync();

            if (warehouseExists == null)
                return NotFound("Warehouse not found");
            
            var orderCmd = new SqlCommand(@"
                SELECT TOP 1 * FROM [Order]
                WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt < @CreatedAt
            ", connection, transaction);

            orderCmd.Parameters.AddWithValue("@IdProduct", request.IdProduct);
            orderCmd.Parameters.AddWithValue("@Amount", request.Amount);
            orderCmd.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);

            Order? order = null;
            using (var reader = await orderCmd.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    order = new Order
                    {
                        IdOrder = reader.GetInt32(reader.GetOrdinal("IdOrder")),
                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                        Amount = reader.GetInt32(reader.GetOrdinal("Amount")),
                        IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct"))
                    };
                }
            }

            if (order == null)
                return NotFound("Order not found**-");
            
            var existsInProductWarehouseCmd = new SqlCommand(@"
                SELECT 1 FROM Product_Warehouse WHERE IdOrder = @IdOrder
            ", connection, transaction);
            existsInProductWarehouseCmd.Parameters.AddWithValue("@IdOrder", order.IdOrder);
            var alreadyProcessed = await existsInProductWarehouseCmd.ExecuteScalarAsync();
            if (alreadyProcessed != null)
                return BadRequest("Order already fulfilled");
            
            var updateOrderCmd = new SqlCommand(@"
                UPDATE [Order]
                SET FulfilledAt = @Now
                WHERE IdOrder = @IdOrder
            ", connection, transaction);
            updateOrderCmd.Parameters.AddWithValue("@Now", DateTime.Now);
            updateOrderCmd.Parameters.AddWithValue("@IdOrder", order.IdOrder);
            await updateOrderCmd.ExecuteNonQueryAsync();
            
            var priceCmd = new SqlCommand("SELECT Price FROM Product WHERE IdProduct = @IdProduct", connection, transaction);
            priceCmd.Parameters.AddWithValue("@IdProduct", request.IdProduct);
            var price = (decimal?)await priceCmd.ExecuteScalarAsync() ?? 0;

            var totalPrice = price * request.Amount;
            
            var insertCmd = new SqlCommand(@"
                INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)
                OUTPUT INSERTED.IdProductWarehouse
                VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt)
            ", connection, transaction);
            insertCmd.Parameters.AddWithValue("@IdWarehouse", request.IdWarehouse);
            insertCmd.Parameters.AddWithValue("@IdProduct", request.IdProduct);
            insertCmd.Parameters.AddWithValue("@IdOrder", order.IdOrder);
            insertCmd.Parameters.AddWithValue("@Amount", request.Amount);
            insertCmd.Parameters.AddWithValue("@Price", totalPrice);
            insertCmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

            var insertedId = (int)await insertCmd.ExecuteScalarAsync();

            transaction.Commit();
            return Ok(new { Id = insertedId });
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return StatusCode(500, ex.Message);
        }
    }
}