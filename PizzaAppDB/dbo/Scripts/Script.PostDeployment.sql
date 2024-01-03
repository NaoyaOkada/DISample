/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
if not exists (select 1 from dbo.Pizzas)
begin
    insert into dbo.Pizzas(ProductName, Price)
    values ('Sea Food Pizza', 10), ('Margherita', 20), ('Meat Pizza', 30)
end

if not exists (select 1 from dbo.Users)
begin
    insert into dbo.Users(FirstName, LastName)
    values ('Taro', 'Yamada'), ('Hanako', 'Suzuki')
end

if not exists (select 1 from dbo.Orders)
begin
    declare @userId1 int;
    declare @userId2 int;
    declare @pizzaId1 int;
    declare @pizzaId2 int;

    select @userId1  = Id from dbo.Users  where FirstName = 'Taro';
    select @userId2  = Id from dbo.Users  where FirstName = 'Hanako';

    select @pizzaId1 = Id from dbo.Pizzas where ProductName = 'Margherita';
    select @pizzaId2 = Id from dbo.Pizzas where ProductName = 'Meat Pizza';

    insert into dbo.Orders(UserId, PizzaId, Cnt)
    values (@userId1, @pizzaId1, 2), (@userId2, @pizzaId2, 1)
end