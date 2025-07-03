
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/17/2025 12:13:26
-- Generated from EDMX file: D:\Users\rober\Documents\MAD\practica-mad-segundo-cuatrimestre-mad201\mad201\Model\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Mad201];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProductCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductSet] DROP CONSTRAINT [FK_ProductCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_CartlineProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartlineSet] DROP CONSTRAINT [FK_CartlineProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderCartline]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartlineSet] DROP CONSTRAINT [FK_OrderCartline];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientBankcard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BankcardSet] DROP CONSTRAINT [FK_ClientBankcard];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderSet] DROP CONSTRAINT [FK_ClientOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductRestaurant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductSet] DROP CONSTRAINT [FK_ProductRestaurant];
GO
IF OBJECT_ID(N'[dbo].[FK_RestaurantComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentSet] DROP CONSTRAINT [FK_RestaurantComment];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CommentSet] DROP CONSTRAINT [FK_ClientComment];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategorySet] DROP CONSTRAINT [FK_CategoryCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductProductPorperty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductPropertySet] DROP CONSTRAINT [FK_ProductProductPorperty];
GO
IF OBJECT_ID(N'[dbo].[FK_PropertyProductPorperty]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductPropertySet] DROP CONSTRAINT [FK_PropertyProductPorperty];
GO
IF OBJECT_ID(N'[dbo].[FK_Client_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet_Client] DROP CONSTRAINT [FK_Client_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Restaurant_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSet_Restaurant] DROP CONSTRAINT [FK_Restaurant_inherits_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CategorySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategorySet];
GO
IF OBJECT_ID(N'[dbo].[CartlineSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartlineSet];
GO
IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[BankcardSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BankcardSet];
GO
IF OBJECT_ID(N'[dbo].[OrderSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderSet];
GO
IF OBJECT_ID(N'[dbo].[ProductSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSet];
GO
IF OBJECT_ID(N'[dbo].[PropertySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PropertySet];
GO
IF OBJECT_ID(N'[dbo].[CommentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CommentSet];
GO
IF OBJECT_ID(N'[dbo].[ProductPropertySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductPropertySet];
GO
IF OBJECT_ID(N'[dbo].[UserSet_Client]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet_Client];
GO
IF OBJECT_ID(N'[dbo].[UserSet_Restaurant]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet_Restaurant];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CategorySet'
CREATE TABLE [dbo].[CategorySet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [categoryName] nvarchar(max)  NOT NULL,
    [Category2_Id] bigint  NULL
);
GO

-- Creating table 'CartlineSet'
CREATE TABLE [dbo].[CartlineSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [uds] int  NOT NULL,
    [price] float  NOT NULL,
    [productName] nvarchar(max)  NOT NULL,
    [Order_Id] bigint  NOT NULL
);
GO

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [login] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [address] nvarchar(max)  NOT NULL,
    [email] nvarchar(max)  NOT NULL,
    [language] nvarchar(max)  NOT NULL,
    [country] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BankcardSet'
CREATE TABLE [dbo].[BankcardSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [number] bigint  NOT NULL,
    [cardtype] nvarchar(max)  NOT NULL,
    [cvv] int  NOT NULL,
    [expirationdate] datetime  NOT NULL,
    [default] bit  NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [Client_Id] bigint  NOT NULL
);
GO

-- Creating table 'OrderSet'
CREATE TABLE [dbo].[OrderSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [orderDate] datetime  NOT NULL,
    [orderAddress] nvarchar(max)  NOT NULL,
    [bankcardNumber] bigint  NOT NULL,
    [Client_Id] bigint  NOT NULL
);
GO

-- Creating table 'ProductSet'
CREATE TABLE [dbo].[ProductSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [price] float  NOT NULL,
    [creationDate] datetime  NOT NULL,
    [stock] int  NOT NULL,
    [Category_Id] bigint  NOT NULL,
    [Restaurant_Id] bigint  NOT NULL
);
GO

-- Creating table 'PropertySet'
CREATE TABLE [dbo].[PropertySet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CommentSet'
CREATE TABLE [dbo].[CommentSet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [content] nvarchar(max)  NOT NULL,
    [rate] int  NOT NULL,
    [date] datetime  NOT NULL,
    [Restaurant_Id] bigint  NOT NULL,
    [Client_Id] bigint  NOT NULL
);
GO

-- Creating table 'ProductPropertySet'
CREATE TABLE [dbo].[ProductPropertySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [value] nvarchar(max)  NOT NULL,
    [Product_Id] bigint  NOT NULL,
    [Property_Id] bigint  NOT NULL
);
GO

-- Creating table 'UserSet_Client'
CREATE TABLE [dbo].[UserSet_Client] (
    [surname] nvarchar(max)  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- Creating table 'UserSet_Restaurant'
CREATE TABLE [dbo].[UserSet_Restaurant] (
    [schedule] nvarchar(max)  NOT NULL,
    [type] nvarchar(max)  NOT NULL,
    [Id] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CategorySet'
ALTER TABLE [dbo].[CategorySet]
ADD CONSTRAINT [PK_CategorySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CartlineSet'
ALTER TABLE [dbo].[CartlineSet]
ADD CONSTRAINT [PK_CartlineSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BankcardSet'
ALTER TABLE [dbo].[BankcardSet]
ADD CONSTRAINT [PK_BankcardSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [PK_OrderSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [PK_ProductSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PropertySet'
ALTER TABLE [dbo].[PropertySet]
ADD CONSTRAINT [PK_PropertySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [PK_CommentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductPropertySet'
ALTER TABLE [dbo].[ProductPropertySet]
ADD CONSTRAINT [PK_ProductPropertySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet_Client'
ALTER TABLE [dbo].[UserSet_Client]
ADD CONSTRAINT [PK_UserSet_Client]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet_Restaurant'
ALTER TABLE [dbo].[UserSet_Restaurant]
ADD CONSTRAINT [PK_UserSet_Restaurant]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Category_Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [FK_ProductCategory]
    FOREIGN KEY ([Category_Id])
    REFERENCES [dbo].[CategorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategory'
CREATE INDEX [IX_FK_ProductCategory]
ON [dbo].[ProductSet]
    ([Category_Id]);
GO

-- Creating foreign key on [Order_Id] in table 'CartlineSet'
ALTER TABLE [dbo].[CartlineSet]
ADD CONSTRAINT [FK_OrderCartline]
    FOREIGN KEY ([Order_Id])
    REFERENCES [dbo].[OrderSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderCartline'
CREATE INDEX [IX_FK_OrderCartline]
ON [dbo].[CartlineSet]
    ([Order_Id]);
GO

-- Creating foreign key on [Client_Id] in table 'BankcardSet'
ALTER TABLE [dbo].[BankcardSet]
ADD CONSTRAINT [FK_ClientBankcard]
    FOREIGN KEY ([Client_Id])
    REFERENCES [dbo].[UserSet_Client]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientBankcard'
CREATE INDEX [IX_FK_ClientBankcard]
ON [dbo].[BankcardSet]
    ([Client_Id]);
GO

-- Creating foreign key on [Client_Id] in table 'OrderSet'
ALTER TABLE [dbo].[OrderSet]
ADD CONSTRAINT [FK_ClientOrder]
    FOREIGN KEY ([Client_Id])
    REFERENCES [dbo].[UserSet_Client]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientOrder'
CREATE INDEX [IX_FK_ClientOrder]
ON [dbo].[OrderSet]
    ([Client_Id]);
GO

-- Creating foreign key on [Restaurant_Id] in table 'ProductSet'
ALTER TABLE [dbo].[ProductSet]
ADD CONSTRAINT [FK_ProductRestaurant]
    FOREIGN KEY ([Restaurant_Id])
    REFERENCES [dbo].[UserSet_Restaurant]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductRestaurant'
CREATE INDEX [IX_FK_ProductRestaurant]
ON [dbo].[ProductSet]
    ([Restaurant_Id]);
GO

-- Creating foreign key on [Restaurant_Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [FK_RestaurantComment]
    FOREIGN KEY ([Restaurant_Id])
    REFERENCES [dbo].[UserSet_Restaurant]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RestaurantComment'
CREATE INDEX [IX_FK_RestaurantComment]
ON [dbo].[CommentSet]
    ([Restaurant_Id]);
GO

-- Creating foreign key on [Client_Id] in table 'CommentSet'
ALTER TABLE [dbo].[CommentSet]
ADD CONSTRAINT [FK_ClientComment]
    FOREIGN KEY ([Client_Id])
    REFERENCES [dbo].[UserSet_Client]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientComment'
CREATE INDEX [IX_FK_ClientComment]
ON [dbo].[CommentSet]
    ([Client_Id]);
GO

-- Creating foreign key on [Category2_Id] in table 'CategorySet'
ALTER TABLE [dbo].[CategorySet]
ADD CONSTRAINT [FK_CategoryCategory]
    FOREIGN KEY ([Category2_Id])
    REFERENCES [dbo].[CategorySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryCategory'
CREATE INDEX [IX_FK_CategoryCategory]
ON [dbo].[CategorySet]
    ([Category2_Id]);
GO

-- Creating foreign key on [Product_Id] in table 'ProductPropertySet'
ALTER TABLE [dbo].[ProductPropertySet]
ADD CONSTRAINT [FK_ProductProductPorperty]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[ProductSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductPorperty'
CREATE INDEX [IX_FK_ProductProductPorperty]
ON [dbo].[ProductPropertySet]
    ([Product_Id]);
GO

-- Creating foreign key on [Property_Id] in table 'ProductPropertySet'
ALTER TABLE [dbo].[ProductPropertySet]
ADD CONSTRAINT [FK_PropertyProductPorperty]
    FOREIGN KEY ([Property_Id])
    REFERENCES [dbo].[PropertySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PropertyProductPorperty'
CREATE INDEX [IX_FK_PropertyProductPorperty]
ON [dbo].[ProductPropertySet]
    ([Property_Id]);
GO

-- Creating foreign key on [Id] in table 'UserSet_Client'
ALTER TABLE [dbo].[UserSet_Client]
ADD CONSTRAINT [FK_Client_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'UserSet_Restaurant'
ALTER TABLE [dbo].[UserSet_Restaurant]
ADD CONSTRAINT [FK_Restaurant_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------