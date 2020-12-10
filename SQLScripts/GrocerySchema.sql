-- Create a new database called 'GroceryList'
-- Connect to the 'master' database to run this snippet
USE master
GO
IF NOT EXISTS (
   SELECT name
   FROM sys.databases
   WHERE name = N'GroceryList'
)
CREATE DATABASE [GroceryList]
GO

--CREATE GROCERYLIST SCHEMA OBJECTS

USE [GroceryList];
GO 

--CREATE MAIN GROCERYLIST TABLE
IF NOT EXISTS (
   SELECT TABLE_NAME
   FROM INFORMATION_SCHEMA.TABLES
   WHERE TABLE_NAME = N'ShoppingLists')
BEGIN
    CREATE TABLE [dbo].[ShoppingLists]
    (
        uUniqueID UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
        sItemName VARCHAR(30) NOT NULL,
        iItemQuantity INT NOT NULL,
        dItemPrice DECIMAL NOT NULL,
        bIsTaxable BIT NOT NULL,
        sAddedBy VARCHAR(30) NOT NULL,
        tAddedDate DATE NOT NULL,
        sUpdatedBy VARCHAR(30) NULL,
        tUpdatedDate DATE NULL,
        bDeletedIndicator bit NOT NULL default 0 
    )
END 
GO

/*************************************************************************************************/
/*                                  START OF STORED PROCEDURES                                   */
/*************************************************************************************************/
-- INSERT NEW ROW
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertNewRows]') AND type in(N'P', N'PC'))
    DROP PROCEDURE [dbo].[usp_InsertNewRows]
    PRINT 'DROPPING [dbo].[usp_InsertNewRows]'
GO
CREATE PROCEDURE [dbo].[usp_InsertNewRows]
    @itemName VARCHAR(30),
    @itemQuantity INT,
    @itemPrice DECIMAL,
    @isTaxable BIT,
    @addedBy VARCHAR(30),
    @addedDate DATE
AS
BEGIN
    SET NOCOUNT ON 

    DECLARE @sName VARCHAR(30);
    DECLARE @iQuantity INT;
    DECLARE @dPrice DECIMAL;
    DECLARE @bTax BIT;
    DECLARE @sAddedBy VARCHAR(30);
    DECLARE @tAddedDate DATE;
    DECLARE @iInserted INT;

    BEGIN TRY
        SET @sName = @itemName;
        SET @iQuantity = @itemQuantity;
        SET @dPrice = @itemPrice;
        SET @bTax = @isTaxable;
        SET @sAddedBy = @addedBy;
        SET @tAddedDate = @addedDate;

        PRINT 'INSERTING INTO [dbo].[usp_InsertNewRows]'

        INSERT INTO [dbo].[ShoppingLists]
            (sItemName, dItemPrice, iItemQuantity, bIsTaxable, sAddedBy, tAddedDate, bDeletedIndicator)
        VALUES (@sName,@dPrice,@iQuantity,@bTax,@sAddedBy,@tAddedDate,0)
        
        SET @iInserted = @@ROWCOUNT
        PRINT 'Records Inserted'

    END TRY
    BEGIN CATCH
        PRINT 'Problem inserting rows into table'
    END CATCH

    SELECT @iInserted    
END
GO

--SELECT EXISTING ROWS 
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SelectExistingLists]') AND type in(N'P', N'PC'))
    DROP PROCEDURE [dbo].[usp_SelectExistingLists]
    PRINT 'DROPPING [dbo].[usp_SelectExistingLists]'
GO
CREATE PROCEDURE [dbo].[usp_SelectExistingLists]
AS
BEGIN
    SET NOCOUNT ON 

    SELECT COUNT(uUniqueID) uniqueCount, tAddedDate 
        FROM [dbo].[ShoppingLists] WITH (NOLOCK)
    GROUP BY tAddedDate
    ORDER BY tAddedDate DESC
END

/*************************************************************************************************/
/*                                    END OF STORED PROCEDURES                                   */
/*************************************************************************************************/
