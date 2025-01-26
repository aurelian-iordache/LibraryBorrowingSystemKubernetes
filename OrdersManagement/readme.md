In order to create the database for the OrderStateDbContext, you need to run the following command in the Visual Studio Developer Power Shell (while the mssql container is running):

PS D:\Development\libraryborrowingsystem\ordersmanagement> dotnet ef database update --context OrderStateDbContext --connection "Server=127.0.0.1,1435;Database=LibraryBorrowingSystem;User Id=sa;Password=yourStrong!Passw0rd;TrustServerCertificate=True"

UPDATE: seems it work fine wihtout the connection parameter (PS D:\Development\libraryborrowingsystem\ordersmanagement> dotnet ef database update --context OrderStateDbContext)

The result should be similar to the following:

Build started...
Build succeeded.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (23ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (22ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT OBJECT_ID(N'[__OrderStateDbContext]');
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (16ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [__OrderStateDbContext] (
          [MigrationId] nvarchar(150) NOT NULL,
          [ProductVersion] nvarchar(32) NOT NULL,
          CONSTRAINT [PK___OrderStateDbContext] PRIMARY KEY ([MigrationId])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT OBJECT_ID(N'[__OrderStateDbContext]');
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (53ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT [MigrationId], [ProductVersion]
      FROM [__OrderStateDbContext]
      ORDER BY [MigrationId];
info: Microsoft.EntityFrameworkCore.Migrations[20402]
      Applying migration '20250107230828_InitialCreate'.
Applying migration '20250107230828_InitialCreate'.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (5ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [OrderSagaState] (
          [CorrelationId] uniqueidentifier NOT NULL,
          [CurrentState] nvarchar(64) NOT NULL,
          [OrderCreatedDate] datetime2 NOT NULL,
          [OrderProcessedDate] datetime2 NOT NULL,
          CONSTRAINT [PK_OrderSagaState] PRIMARY KEY ([CorrelationId])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (5ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      INSERT INTO [__OrderStateDbContext] ([MigrationId], [ProductVersion])
      VALUES (N'20250107230828_InitialCreate', N'8.0.0');
Done.

To check the status of the orders use a command prompt like this (output included):

C:\Users\Work>docker exec -it mssql /opt/mssql-tools18/bin/sqlcmd -S 127.0.0.1,1434 -U sa -P yourStrong!Passw0rd -C
1> USE LibraryBorrowingSystem;
2> GO
Changed database context to 'LibraryBorrowingSystem'.
1> SELECT * FROM OrderSagaState;
2> GO
CorrelationId                        CurrentState                                                     OrderCreatedDate                       OrderProcessedDate
------------------------------------ ---------------------------------------------------------------- -------------------------------------- --------------------------------------
3FA85F64-5717-4562-B3FC-2C963F66AFA6 Final                                                                       2025-01-08 19:11:39.0117850            2025-01-08 19:11:42.4340722

(1 rows affected)