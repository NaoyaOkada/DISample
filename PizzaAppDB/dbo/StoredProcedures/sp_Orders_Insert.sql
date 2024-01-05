CREATE PROCEDURE [dbo].[sp_Orders_Insert]
	@userId int,
	@pizzaId int,
	@cnt int
AS
BEGIN
	SET NOCOUNT ON;
	insert into dbo.Orders(UserId, PizzaId, Cnt)
	values(@userId, @pizzaId, @cnt)
END

