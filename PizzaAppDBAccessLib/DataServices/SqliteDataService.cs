using Dapper;
using Microsoft.Extensions.Configuration;
using PizzaAppDBAccessLib.Models;
using System.Data.SQLite;

namespace PizzaAppDBAccessLib.DataServices
{
    public class SqliteDataService : IDataService
    {
        private readonly ISqliteDataBase _db;

        public SqliteDataService(ISqliteDataBase db)
        {
            _db = db;
        }

        public void DeliveryDone(int orderId)
        {
            string sql = @"update orders set isDone = 1 where Id = @id";
            dynamic param = new { id = orderId };
            _db.Save<dynamic>(sql, param);
        }

        public List<FullOrder> GetAllOrdersToDeliver()
        {
            string sql = @"select o.id, o.PizzaId, o.Cnt Count, u.FirstName, u.LastName
                           from dbo.Orders o
                           inner join dbo.Users u on o.UserId = u.Id
                           where o.isDone = @isDone";
            dynamic param = new { isDone = 0 };
            return _db.Load<FullOrder, dynamic>(sql, param);
        }

        public List<Pizza> GetAllPizzas()
        {
            string sql = "SELECT * FROM Pizzas";
            return _db.Load<Pizza, dynamic>(sql, new {});
        }

        public Pizza? GetPizzaById(int id)
        {
            string sql = @"SELECT Id, ProductName ,Price
                          FROM Pizzas
                          WHERE Id = @id LIMIT 1";
            dynamic param = new { id };
            List<Pizza> pizza = _db.Load<Pizza, dynamic>(sql, param);
            return pizza.First();
        }

        public void OrderPizza(FullOrder fullOrder)
        {
            string sql = "SELECT Id FROM Users Where FirstName = @firstName and LastName = @lastName LIMIT 1";
            dynamic param = new
            {
                firstName = fullOrder.FirstName,
                lastName = fullOrder.LastName,
            };

            List<User> userList = _db.Load<User, dynamic>(sql, param);
            if (0 == userList.Count)
            {
                sql = "Insert into Users (FirstName, LastName) values(@firstName, @lastName)";
                _db.Save(sql, param);
            }

            sql = "SELECT Id FROM Users Where FirstName = @firstName and LastName = @lastName LIMIT 1";
            userList = _db.Load<User, dynamic>(sql, param);
            User user = userList.First();

            Pizza? pizza = GetPizzaById(fullOrder.PizzaId);
            if (null == pizza) return;

            sql = "insert into Orders(UserId, PizzaId, Cnt) values(@userId, @pizzaId, @cnt)";
            param = new
            {
                userId = user.Id,
                pizzaId = pizza?.Id,
                cnt = fullOrder.Count,
            };
            _db.Save(sql, param);
        }
    }

    public interface ISqliteDataBase
    {
        List<T> Load<T, U>(string sql, U param);

        void Save<T>(string sql, T param);
    }


    public class SqliteDataBase(IConfiguration config) : ISqliteDataBase
    {
        private readonly string? _connectionString = config.GetConnectionString("Sqlite");

        public List<T> Load<T, U>(string sql, U param)
        {
            using var connection = new SQLiteConnection(_connectionString);
            // データベースを開く
            connection.Open();
            // Dapperを使用してクエリを実行し、結果を取得
            return connection.Query<T>(sql, param).ToList();
        }

        public void Save<T>(string sql, T param)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            int affectedRows = connection.Execute(sql, param);
            return;
        }
    }
}
