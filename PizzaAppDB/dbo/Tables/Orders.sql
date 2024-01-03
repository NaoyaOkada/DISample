CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [PizzaId] INT NOT NULL, 
    [Cnt] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_orders_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]), 
    CONSTRAINT [FK_orders_ToPizzas] FOREIGN KEY ([PizzaId]) REFERENCES [Pizzas]([Id])
)
