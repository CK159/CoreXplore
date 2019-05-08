CREATE TABLE [Store] (
    [StoreId] int NOT NULL IDENTITY,
    [StoreName] nvarchar(128) NOT NULL,
    [ImportantConfigId] int NOT NULL DEFAULT 7,
    [Description] nvarchar(1024) NOT NULL DEFAULT N'',
    [Owner] nvarchar(128) NOT NULL DEFAULT N'',
    [Active] bit NOT NULL DEFAULT 1,
    [DateCreated] datetime2 NOT NULL DEFAULT (getdate()),
    CONSTRAINT [PK_Store] PRIMARY KEY ([StoreId])
);

GO

CREATE TABLE [Catalog] (
    [CatalogId] int NOT NULL IDENTITY,
    [CatalogName] nvarchar(128) NOT NULL,
    [CatalogDesc] nvarchar(1024) NOT NULL DEFAULT N'',
    [InternalName] nvarchar(16) NOT NULL DEFAULT N'',
    [Active] bit NOT NULL,
    [DateCreated] datetime2 NOT NULL DEFAULT (getdate()),
    [StoreId] int NOT NULL,
    CONSTRAINT [PK_Catalog] PRIMARY KEY ([CatalogId]),
    CONSTRAINT [FK_Catalog_Store_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Store] ([StoreId]) ON DELETE CASCADE
);

GO

CREATE TABLE [CatalogProduct] (
    [CatalogProductId] int NOT NULL IDENTITY,
    [SortOrder] int NOT NULL DEFAULT 0,
    [DateCreated] datetime2 NOT NULL DEFAULT (getdate()),
    [CatalogId] int NOT NULL,
    [ProductId] int NOT NULL,
    CONSTRAINT [PK_CatalogProduct] PRIMARY KEY ([CatalogProductId]),
    CONSTRAINT [FK_CatalogProduct_Catalog_CatalogId] FOREIGN KEY ([CatalogId]) REFERENCES [Catalog] ([CatalogId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CatalogProduct_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([ProductId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Catalog_StoreId] ON [Catalog] ([StoreId]);

GO

CREATE INDEX [IX_CatalogProduct_CatalogId] ON [CatalogProduct] ([CatalogId]);

GO

CREATE INDEX [IX_CatalogProduct_ProductId] ON [CatalogProduct] ([ProductId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190507032700_Products', N'2.2.4-servicing-10062');

GO


