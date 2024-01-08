using Microsoft.Extensions.Configuration;
using PizzaAppDBAccessLib.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace PizzaAppDBAccessLib.Data
{
    public class SqlDataService : IDataService
    {
        private readonly ISQLDataBase _db;

        public SqlDataService(ISQLDataBase db)
        {
            _db = db;
        }

        public List<Pizza> GetAllPizzas()
        {
            var sql = "[dbo].[sp_GetAllPizzas]";
            var rows = _db.Load<Pizza, dynamic>(sql, new { });
            return rows;
        }

        public Pizza? GetPizzaById(int id)
        {
            string sql = @"SELECT TOP (1) [Id],[ProductName] ,[Price]
                          FROM [dbo].[Pizzas]
                          WHERE Id = @id";
            dynamic param = new { id };
            List<Pizza> pizza = _db.Load<Pizza, dynamic>(sql, param, false);
            return pizza.First();
        }

        public void OrderPizza(FullOrder fullOrder)
        {
            string sql = "[dbo].[sp_Users_Insert]";
            dynamic param = new
            {
                firstName = fullOrder.FirstName,
                lastName = fullOrder.LastName,
            };

            List<User> userList = _db.Load<User, dynamic>(sql, param, true);
            User user = userList.First();

            Pizza? pizza = GetPizzaById(fullOrder.PizzaId);
            if (null == pizza) return;

            sql = "[dbo].[sp_Orders_Insert]";
            param = new
            {
                userId = user.Id,
                pizzaId = pizza?.Id,
                cnt = fullOrder.Count,
            };
            _db.Save(sql, param, true);
        }
    }

    public interface ISQLDataBase
    {
        List<T> Load<T, U>(string sql, U param, bool isStoredProcedure = false);

        void Save<T>(string sql, T param, bool isStoredProcedure = false);
    }

    public class SQLDataBase(IConfiguration configuration) : ISQLDataBase
    {
        private readonly string? _connectionString = configuration.GetConnectionString("MsSql");

        private IDbConnection CreateConnection => new SqlConnection(_connectionString);

        public List<T> Load<T, U>(string sql, U param, bool isStoredProcedure = false)
        {
            CommandType commandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;

            using IDbConnection connection = CreateConnection;
            connection.Open();
            List<T> rows = connection.Query<T>(sql, param, commandType: commandType).ToList();
            return rows;
        }

        public void Save<T>(string sql, T param, bool isStoredProcedure = false)
        {
            CommandType commandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;

            using IDbConnection connection = CreateConnection;
            connection.Execute(sql, param, commandType: commandType);
        }
    }
}
