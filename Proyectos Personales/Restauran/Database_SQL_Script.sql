--Important Note: Before running this command, please create a database as "Restaurant" then run this command.
--Important Note: Before running this command, please create a database as "Restaurant" then run this command.
--Important Note: Before running this command, please create a database as "Restaurant" then run this command.
--Important Note: Before running this command, please create a database as "Restaurant" then run this command.

USE [Restaurant]
GO
/****** Object:  Table [dbo].[Units]    Script Date: 01/23/2012 12:11:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Units](
	[UnitName] [nvarchar](50) NOT NULL,
	[ShortName] [nvarchar](10) NULL,
 CONSTRAINT [PK_Units_1] PRIMARY KEY CLUSTERED 
(
	[UnitName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Report_ProductPopularity]    Script Date: 01/23/2012 12:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns Products list by their popularity
-- =============================================
CREATE PROCEDURE [dbo].[Report_ProductPopularity]
	@FromDate DATETIME,
	@ToDate DATETIME 
AS
BEGIN
	SELECT     Products.ProductName, Products.UnitName, COUNT(OrderDetail.ProductID) AS OrderTime
FROM         OrderHeader INNER JOIN
                      OrderDetail ON OrderHeader.OrderNo = OrderDetail.OrderNo INNER JOIN
                      Products ON OrderDetail.ProductID = Products.ProductID
	WHERE ( CONVERT ( DATETIME , FLOOR ( CONVERT ( FLOAT , OrderHeader.CreationDatetime ) ) )  BETWEEN @FromDate AND @ToDate ) 
	GROUP BY OrderDetail.ProductID, Products.ProductName, Products.UnitName
	ORDER BY OrderTime DESC	
END
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 01/23/2012 12:11:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Settings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RestaurantName] [nvarchar](60) NOT NULL,
	[Address] [nvarchar](150) NULL,
	[PhoneNumber] [char](12) NULL,
	[WebsiteURL] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Logo] [image] NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[Get_Last_OrderNo]    Script Date: 01/23/2012 12:10:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Last_OrderNo]

AS
BEGIN
	SELECT MAX(OrderNo) AS LastOrderNo FROM OrderHeader
END
GO
/****** Object:  StoredProcedure [dbo].[Make_OrderDetail]    Script Date: 01/23/2012 12:10:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure creates order detail for the specified OrderNo
-- =============================================
CREATE PROCEDURE [dbo].[Make_OrderDetail]
	@OrderNo BIGINT,
	@ProductID INT,
	@Amount INT
AS
BEGIN
	DECLARE @Availability BIT
	SET @Availability=0
	-- Check the availability of the product again to become sure that this product is still available
	SELECT @Availability=Availability FROM Products WHERE ProductID=@ProductID

	IF @Availability=1
	BEGIN
		INSERT INTO OrderDetail (OrderNo,ProductID,Amount,EditState,NotEditable)
		VALUES(@OrderNo,@ProductID,@Amount,0,0)
		
		SELECT 1 AS Result
	END
	ELSE
	BEGIN
		-- Because this product is not available any more
		SELECT -1 AS Result
	END
END
GO
/****** Object:  StoredProcedure [dbo].[Get_Available_Products]    Script Date: 01/23/2012 12:10:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure will return all available products 
-- =============================================
CREATE PROCEDURE [dbo].[Get_Available_Products]
 AS
	SELECT     ProductID, ProductName, UnitName,GroupID, Price
	FROM         Products 
	WHERE Availability=1
ORDER BY Products.ProductName
GO
/****** Object:  StoredProcedure [dbo].[Get_Orders_List_For_Kitchen]    Script Date: 01/23/2012 12:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure returns all orders which are new or has edited or canceled
-- =============================================
CREATE PROCEDURE [dbo].[Get_Orders_List_For_Kitchen]
AS
BEGIN
	SELECT    OrderHeader.TableNo, OrderHeader.OrderNo, Users.FullName AS Waiter, OrderHeader.State,
	CASE WHEN  OrderHeader.State=1 THEN  CAST(DATEPART(hour, LastEditionDatetime)AS char(2))+':'+CAST(DATEPART(Minute, LastEditionDatetime)AS char(2))
		 ELSE  CAST(DATEPART(hour, CreationDatetime)AS char(2))+':'+CAST(DATEPART(Minute, CreationDatetime)AS char(2)) END AS ActionTime,OrderHeader.PrintState
	FROM         OrderHeader INNER JOIN
						  Users ON OrderHeader.CreatorUserID = Users.UserID
	WHERE OrderHeader.State=0 OR OrderHeader.State=1 OR OrderHeader.State=2 OR OrderHeader.State=3
	ORDER BY Case [State] WHEN 2 THEN 0 WHEN 1 THEN 1 WHEN 0 THEN 2 ELSE 3 END,ActionTime, OrderHeader.TableNo
END
GO
/****** Object:  StoredProcedure [dbo].[Get_Orders_List_For_Cashier]    Script Date: 01/23/2012 12:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure returns all orders which are new or has edited or canceled
-- =============================================
CREATE PROCEDURE [dbo].[Get_Orders_List_For_Cashier]
AS
BEGIN
	SELECT    OrderHeader.TableNo, OrderHeader.OrderNo, Users.FullName AS Waiter, OrderHeader.State,
	CASE   WHEN  OrderHeader.State=0 THEN CAST(DATEPART(hour, CreationDatetime)AS char(2))+':'+CAST(DATEPART(Minute, CreationDatetime)AS char(2))
		 ELSE CAST(DATEPART(hour, LastEditionDatetime)AS char(2))+':'+CAST(DATEPART(Minute, LastEditionDatetime)AS char(2))  END AS ActionTime
	FROM         OrderHeader INNER JOIN
						  Users ON OrderHeader.CreatorUserID = Users.UserID
	WHERE OrderHeader.State=0 OR OrderHeader.State=1 OR OrderHeader.State=3 OR OrderHeader.State=4
	ORDER BY [State] Desc,ActionTime, OrderHeader.TableNo
END
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 01/23/2012 12:10:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Groups] UNIQUE NONCLUSTERED 
(
	[GroupName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Get_Order_Detail]    Script Date: 01/23/2012 12:10:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure will return the order detail of a specific order
-- =============================================
CREATE PROCEDURE [dbo].[Get_Order_Detail]
	@OrderNo BIGINT
AS
BEGIN
	SELECT     OrderDetail.OrderDetailID, OrderDetail.ProductID, Products.ProductName, Products.UnitName, OrderDetail.Amount, OrderDetail.EditState, 
					CASE OrderDetail.EditState WHEN 0 THEN N''
							WHEN 1 THEN N'+'+CAST(OrderDetail.EditAmount AS VARCHAR(3))
							WHEN 2 THEN N'-'+CAST(OrderDetail.EditAmount AS VARCHAR(3))	
							WHEN 3 THEN N'' END AS EditAmount
						  ,OrderDetail.NotEditable
	FROM         OrderDetail INNER JOIN
                      Products ON OrderDetail.ProductID = Products.ProductID
	WHERE     (OrderDetail.OrderNo = @OrderNo)
	ORDER BY Products.GroupID,Products.ProductName
END
GO
/****** Object:  StoredProcedure [dbo].[Update_NotEditable_State]    Script Date: 01/23/2012 12:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure updates the NotEditable state to disable the editing of the order
-- =============================================
CREATE PROCEDURE [dbo].[Update_NotEditable_State]
	@OrderDetailID BIGINT
AS
BEGIN
	UPDATE OrderDetail SET NotEditable=1 WHERE OrderDetailID=@OrderDetailID
END
GO
/****** Object:  StoredProcedure [dbo].[Lock_Order]    Script Date: 01/23/2012 12:10:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure Locks the order for editing or changing state
-- =============================================
CREATE PROCEDURE [dbo].[Lock_Order]
	@OrderNo BIGINT,
	@LockKeeperUserID INT
AS
BEGIN
	UPDATE OrderHeader SET LockState=1, LockKeeperUserID=@LockKeeperUserID WHERE (OrderNo=@OrderNo AND LockState=0) 
									OR (OrderNo=@OrderNo AND LockState=1 AND LockKeeperUserID=@LockKeeperUserID)
END
GO
/****** Object:  StoredProcedure [dbo].[Unlock_Order]    Script Date: 01/23/2012 12:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure Unlocks the order after editing or changing state
-- =============================================
CREATE PROCEDURE [dbo].[Unlock_Order]
	@OrderNo BIGINT,	
	@LockKeeperUserID INT
AS
BEGIN
	UPDATE OrderHeader SET LockState=0,LockKeeperUserID=NULL WHERE OrderNo=@OrderNo AND LockKeeperUserID=@LockKeeperUserID
END
GO
/****** Object:  Table [dbo].[DataHasChanged]    Script Date: 01/23/2012 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataHasChanged](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupsLastChangedDate] [datetime] NOT NULL CONSTRAINT [DF_DataHasChanged_GroupsLastChangedDate]  DEFAULT (getdate()),
	[ProductsLastChangedDate] [datetime] NOT NULL CONSTRAINT [DF_DataHasChanged_ProductsLastChangedDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_DataHasChanged] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Set_Order_ReadyToServe]    Script Date: 01/23/2012 12:10:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	This procedure Updates the state of the order into Ready to Serve
-- =============================================
CREATE PROCEDURE [dbo].[Set_Order_ReadyToServe]
	@OrderNo BIGINT
AS
BEGIN
	UPDATE OrderHeader SET [State]=3, PrintState='1' WHERE OrderNo=@OrderNo 
END
GO
/****** Object:  Table [dbo].[RestaurantTables]    Script Date: 01/23/2012 12:11:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RestaurantTables](
	[TableNo] [varchar](10) NOT NULL,
	[Capacity] [tinyint] NOT NULL,
	[Description] [nvarchar](100) NULL,
	[State] [bit] NOT NULL CONSTRAINT [DF_RestaurantTables_State]  DEFAULT ((0)),
 CONSTRAINT [PK_RestaurantTables] PRIMARY KEY CLUSTERED 
(
	[TableNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Free=false; Busy=true' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RestaurantTables', @level2type=N'COLUMN',@level2name=N'State'
GO
/****** Object:  StoredProcedure [dbo].[Get_Orders_List_For_Waiter]    Script Date: 01/23/2012 12:10:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure returns all orders which are not served yet
-- =============================================
CREATE PROCEDURE [dbo].[Get_Orders_List_For_Waiter]
AS
BEGIN
	SELECT    TableNo, OrderNo, [State]
		FROM         OrderHeader 
	WHERE [State]=0 OR [State]=1 OR [State]=3
	ORDER BY [State] DESC,TableNo,OrderNo
	
	SELECT COUNT(*) FROM OrderHeader WHERE [State]=3
END
GO
/****** Object:  StoredProcedure [dbo].[Set_Order_Served]    Script Date: 01/23/2012 12:10:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure Updates the state of the order into Served
-- =============================================
CREATE PROCEDURE [dbo].[Set_Order_Served]
	@OrderNo BIGINT
AS
BEGIN
	UPDATE OrderHeader SET [State]=4 WHERE OrderNo=@OrderNo 
END
GO
/****** Object:  StoredProcedure [dbo].[Update_DataHasChanged]    Script Date: 01/23/2012 12:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_DataHasChanged]
	@Type VARCHAR(8)
AS

	IF (SELECT COUNT(*) FROM DataHasChanged)=0
	BEGIN
		INSERT INTO DataHasChanged (GroupsLastChangedDate,ProductsLastChangedDate)
		VALUES (GETDATE(),GETDATE())
	END

	IF @Type='Groups'
	BEGIN
	UPDATE DataHasChanged SET GroupsLastChangedDate=GETDATE()
	END

	IF @Type='Products'
	BEGIN
	UPDATE DataHasChanged SET ProductsLastChangedDate=GETDATE()
	END
GO
/****** Object:  StoredProcedure [dbo].[PPC_Get_Order_Detail]    Script Date: 01/23/2012 12:10:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure returns the order detail of a specific order To be used in editing an order in Waiter Application
-- =============================================
CREATE PROCEDURE [dbo].[PPC_Get_Order_Detail]
	@OrderNo BIGINT
AS
BEGIN
	SELECT     OrderDetail.ProductID, Products.ProductName, Products.UnitName, OrderDetail.Amount, OrderDetail.NotEditable
	FROM         OrderDetail INNER JOIN
						  Products ON OrderDetail.ProductID = Products.ProductID INNER JOIN
						  OrderHeader ON OrderDetail.OrderNo = OrderHeader.OrderNo
	WHERE     (OrderDetail.OrderNo = @OrderNo) AND (OrderHeader.State = 0 OR OrderHeader.State = 1) AND (OrderDetail.EditState=0 OR OrderDetail.EditState=1 OR OrderDetail.EditState=2)
END
GO
/****** Object:  StoredProcedure [dbo].[Update_OrderDetail]    Script Date: 01/23/2012 12:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure updates order detail for the specified OrderNo
-- =============================================
CREATE PROCEDURE [dbo].[Update_OrderDetail]
	@OrderNo BIGINT,
	@ProductID INT,
	@Amount INT,
	@ChangeState TINYINT,
	@EditAmount INT
AS
BEGIN
	IF @ChangeState=3 -- the amount value of a product has increased.
	BEGIN
		DECLARE @Availability BIT
		SET @Availability=0
		-- Check the availability of the product again to become sure that this product is still available
		SELECT @Availability=Availability FROM Products WHERE ProductID=@ProductID

		IF @Availability=1
		BEGIN
			UPDATE OrderHeader SET LastEditionDatetime=GETDATE() 
			WHERE OrderNo=@OrderNo

			UPDATE OrderDetail SET Amount=@Amount,EditState=1,EditAmount=@EditAmount
			WHERE OrderNo=@OrderNo AND ProductID=@ProductID 
			
			SELECT 1 AS Result
		END
		ELSE
		BEGIN
			-- Because this product is not available any more
			SELECT -1 AS Result
		END
	END
	ELSE IF @ChangeState=4 -- the amount value of a product has decreased.
	BEGIN
		UPDATE OrderHeader SET LastEditionDatetime=GETDATE() 
		WHERE OrderNo=@OrderNo

		UPDATE OrderDetail SET Amount=@Amount,EditState=2,EditAmount=@EditAmount
		WHERE OrderNo=@OrderNo AND ProductID=@ProductID 
	END
	ELSE IF @ChangeState=2 -- a product has removed from the order.
	BEGIN
		UPDATE OrderHeader SET LastEditionDatetime=GETDATE() 
		WHERE OrderNo=@OrderNo

		UPDATE OrderDetail SET EditState=3
		WHERE OrderNo=@OrderNo AND ProductID=@ProductID 
	END
END
GO
/****** Object:  StoredProcedure [dbo].[Delete_User]    Script Date: 01/23/2012 12:10:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Delete_User]

	@UserID INT
AS
	DELETE Users 
	WHERE UserID=@UserID
GO
/****** Object:  StoredProcedure [dbo].[Reset_User_Password]    Script Date: 01/23/2012 12:10:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Reset_User_Password]

	@UserID INT
AS
	UPDATE Users SET Password='202CB962AC59075B964B07152D234B70' 
	WHERE UserID=@UserID
GO
/****** Object:  StoredProcedure [dbo].[Get_Order_Detail_For_Cashier]    Script Date: 01/23/2012 12:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure returns the order detail of a specific order
-- =============================================
CREATE PROCEDURE [dbo].[Get_Order_Detail_For_Cashier]
	@OrderNo BIGINT
AS
BEGIN
	SELECT     OrderDetail.ProductID, Products.ProductName, Products.UnitName, OrderDetail.Amount, Products.Price, OrderDetail.Amount * Products.Price AS RowPrice
FROM         OrderDetail INNER JOIN
					  OrderHeader ON OrderHeader.OrderNo=OrderDetail.OrderNo INNER JOIN
                      Products ON OrderDetail.ProductID = Products.ProductID
	WHERE     (OrderDetail.OrderNo = @OrderNo) AND (OrderDetail.EditState<>3) AND (OrderHeader.State <> 5)
	ORDER BY Products.GroupID,Products.ProductName
END
GO
/****** Object:  StoredProcedure [dbo].[Update_User]    Script Date: 01/23/2012 12:10:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_User]

	@UserID INT,
	@FullName NVARCHAR(50),
	@UserName NVARCHAR(32),
	@PermissionID INT
 AS
	UPDATE Users SET FullName=@FullName , UserName=@UserName ,PermissionID=@PermissionID
	WHERE UserID=@UserID
GO
/****** Object:  StoredProcedure [dbo].[Get_Orders_Of_A_Table]    Script Date: 01/23/2012 12:10:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure returns all orders for an specified table
-- =============================================
CREATE PROCEDURE [dbo].[Get_Orders_Of_A_Table]
	@TableNo VARCHAR(10)
AS
BEGIN
	SELECT     OrderNo, State
	FROM         OrderHeader
	WHERE     (TableNo = @TableNo) AND (State <> 2) AND (State <> 5) AND (State <> 6)
END
GO
/****** Object:  StoredProcedure [dbo].[Update_User_Pass]    Script Date: 01/23/2012 12:10:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_User_Pass]

	@UserID INT,
	@Password VARCHAR(32)
 AS
	UPDATE Users SET Password=@Password
	WHERE UserID=@UserID
GO
/****** Object:  StoredProcedure [dbo].[Lock_Order_New]    Script Date: 01/23/2012 12:10:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure Locks the order
-- =============================================
CREATE PROCEDURE [dbo].[Lock_Order_New]
	@OrderNo BIGINT,
	@LockKeeperUserID INT
AS
BEGIN
	UPDATE OrderHeader SET LockState=1, LockKeeperUserID=@LockKeeperUserID WHERE (OrderNo=@OrderNo AND LockState=0) 
									OR (OrderNo=@OrderNo AND LockState=1 AND LockKeeperUserID=@LockKeeperUserID)
	IF (SELECT @@RowCount)>0
	BEGIN	
		SELECT N''
	END
	ELSE
	BEGIN	
		SELECT FullName FROM Users WHERE UserID=@LockKeeperUserID
	END
END
GO
/****** Object:  StoredProcedure [dbo].[Check_User]    Script Date: 01/23/2012 12:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Check_User]

	@UserName VARCHAR (50),
	@Password VARCHAR (32)
 AS
	SELECT UserID,FullName,UserName,Password,PermissionID, GETDATE() AS Today FROM [dbo].[Users]
	WHERE (UserName=@UserName AND Password=@Password)
GO
/****** Object:  StoredProcedure [dbo].[Create_User]    Script Date: 01/23/2012 12:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Create_User]

	@FullName NVARCHAR(50),
	@UserName NVARCHAR(32),
	@Password VARCHAR(32),
	@PermissionID INT
AS
	INSERT INTO Users (FullName,UserName,[Password],PermissionID)
	VALUES (@FullName,@UserName,@Password,@PermissionID)
GO
/****** Object:  StoredProcedure [dbo].[Get_Order_State]    Script Date: 01/23/2012 12:10:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure returns the Order State
-- =============================================
CREATE PROCEDURE [dbo].[Get_Order_State]
	@OrderNo BIGINT
AS
BEGIN
	SELECT [State] FROM OrderHeader WHERE OrderNo=@OrderNo
END
GO
/****** Object:  StoredProcedure [dbo].[Set_Order_Edited]    Script Date: 01/23/2012 12:10:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure Updates the state of the order into Edited
-- =============================================
CREATE PROCEDURE [dbo].[Set_Order_Edited]
	@OrderNo BIGINT
AS
BEGIN
	UPDATE OrderHeader SET [State]=1,PrintState=0,LastEditionDatetime=GETDATE()  WHERE OrderNo=@OrderNo
END
GO
/****** Object:  StoredProcedure [dbo].[Get_Orders_To_Auto_Print]    Script Date: 01/23/2012 12:10:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure returns all orders which should be printed
-- =============================================
CREATE PROCEDURE [dbo].[Get_Orders_To_Auto_Print]
AS
BEGIN
	SELECT     OrderNo, TableNo,[State]
	FROM         OrderHeader
	WHERE     (PrintState = 0) AND ([State] = 0 OR [State] = 1 OR [State] = 2)
	ORDER BY LastEditionDateTime,CreationDateTime
END
GO
/****** Object:  StoredProcedure [dbo].[Get_Order_Detail_For_Auto_Print]    Script Date: 01/23/2012 12:10:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure will return the order detail of a specific order
-- =============================================
CREATE PROCEDURE [dbo].[Get_Order_Detail_For_Auto_Print]
	@OrderNo BIGINT
AS
BEGIN
	SELECT      Products.ProductName, Products.UnitName, OrderDetail.Amount, OrderDetail.EditState, 
					CASE OrderDetail.EditState WHEN 0 THEN N'New'
							WHEN 1 THEN N'+'+CAST(OrderDetail.EditAmount AS VARCHAR(3))
							WHEN 2 THEN N'-'+CAST(OrderDetail.EditAmount AS VARCHAR(3))	
							WHEN 3 THEN N'Deleted' END AS EditAmount
	FROM         OrderDetail INNER JOIN
                      Products ON OrderDetail.ProductID = Products.ProductID
	WHERE     (OrderDetail.OrderNo = @OrderNo)
	ORDER BY Products.GroupID,Products.ProductName
END
GO
/****** Object:  StoredProcedure [dbo].[Get_All_Products]    Script Date: 01/23/2012 12:10:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_All_Products]

 AS
	SELECT     Products.ProductID, Products.ProductName, Products.UnitName,Products.GroupID, Groups.GroupName, Products.Price
FROM         Products INNER JOIN
                      Groups ON Products.GroupID = Groups.GroupID
ORDER BY Products.ProductName
GO
/****** Object:  StoredProcedure [dbo].[Update_Settings]    Script Date: 01/23/2012 12:10:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Settings]
	@RestaurantName NVARCHAR(60),
	@Address NVARCHAR(150),
	@PhoneNumber NVARCHAR(12),
	@WebsiteURL NVARCHAR(50),
	@Email NVARCHAR(50),
	@Logo IMAGE
 AS
	IF (SELECT COUNT(*) FROM Settings)>0
	BEGIN
		IF @Logo IS NULL
		BEGIN
			UPDATE Settings SET RestaurantName=@RestaurantName,[Address]=@Address,
				PhoneNumber=@PhoneNumber,WebsiteURL=@WebsiteURL,Email=@Email
		END
		ELSE
		BEGIN
			UPDATE Settings SET RestaurantName=@RestaurantName,[Address]=@Address,
				PhoneNumber=@PhoneNumber,WebsiteURL=@WebsiteURL,Email=@Email,Logo=@Logo
		END
	END
	ELSE
	BEGIN
		IF @Logo IS NULL
		BEGIN
			INSERT INTO Settings(RestaurantName,[Address],PhoneNumber,WebsiteURL,Email)
			VALUES(@RestaurantName,@Address,@PhoneNumber,@WebsiteURL,@Email)
		END
		ELSE
		BEGIN
			INSERT INTO Settings(RestaurantName,[Address],PhoneNumber,WebsiteURL,Email,Logo)
			VALUES(@RestaurantName,@Address,@PhoneNumber,@WebsiteURL,@Email,@Logo)
		END
	END
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 01/23/2012 12:11:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[PermissionID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Update_OrderPrintState]    Script Date: 01/23/2012 12:10:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	This procedure updates order PrintState to true
-- =============================================
CREATE PROCEDURE [dbo].[Update_OrderPrintState]
	@OrderNo BIGINT
AS
BEGIN
	UPDATE OrderHeader SET PrintState='1'
			WHERE OrderNo=@OrderNo
END
GO
/****** Object:  StoredProcedure [dbo].[Report_GetOrdersList]    Script Date: 01/23/2012 12:10:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns the Orders list in a specific duration for sell report
-- =============================================
CREATE PROCEDURE [dbo].[Report_GetOrdersList]
	@FromDate DATETIME,
	@ToDate DATETIME 
AS
BEGIN
	SELECT     OrderNo, TableNo, CreationDatetime, CustomerName, TotalPrice
	FROM         OrderHeader
	WHERE [State]=5 AND ( CONVERT ( DATETIME , FLOOR ( CONVERT ( FLOAT , CreationDatetime ) ) )  BETWEEN @FromDate AND @ToDate ) 
END
GO
/****** Object:  StoredProcedure [dbo].[Get_All_Products_By_GroupID]    Script Date: 01/23/2012 12:10:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	This procedure returns all the products in a specific group
-- =============================================
CREATE PROCEDURE [dbo].[Get_All_Products_By_GroupID]
	@GroupID INT
 AS
	SELECT     Products.ProductID, Products.ProductName, Products.UnitName, Products.Price
	FROM         Products 
	WHERE Products.GroupID=@GroupID
	ORDER BY Products.ProductName
GO
/****** Object:  StoredProcedure [dbo].[Get_All_Groups]    Script Date: 01/23/2012 12:10:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_All_Groups]

 AS
	SELECT GroupID,GroupName FROM Groups 
	ORDER BY GroupName
GO
/****** Object:  Table [dbo].[Products]    Script Date: 01/23/2012 12:11:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[UnitName] [nvarchar](50) NOT NULL,
	[GroupID] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Availability] [bit] NOT NULL CONSTRAINT [DF_Products_Availabilty]  DEFAULT ((0)),
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Products] UNIQUE NONCLUSTERED 
(
	[ProductName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 01/23/2012 12:10:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderDetailID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderNo] [bigint] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[EditState] [tinyint] NOT NULL CONSTRAINT [DF_OrderDetail_EditState]  DEFAULT ((0)),
	[EditAmount] [int] NULL,
	[NotEditable] [bit] NOT NULL CONSTRAINT [DF_OrderDetail_PrepareState]  DEFAULT ((0)),
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0=New; 1=Add; 2= Subtract; 3=Deleted' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderDetail', @level2type=N'COLUMN',@level2name=N'EditState'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'editable=false; not editable=true' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderDetail', @level2type=N'COLUMN',@level2name=N'NotEditable'
GO
/****** Object:  Table [dbo].[OrderHeader]    Script Date: 01/23/2012 12:11:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OrderHeader](
	[OrderNo] [bigint] IDENTITY(1,1) NOT NULL,
	[TableNo] [varchar](10) NOT NULL,
	[CreatorUserID] [int] NOT NULL,
	[CreationDatetime] [datetime] NOT NULL CONSTRAINT [DF_OrderHeader_CreationDatetime]  DEFAULT (getdate()),
	[LastEditionDatetime] [datetime] NULL,
	[CustomerName] [nvarchar](50) NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL CONSTRAINT [DF_OrderHeader_PriceSum]  DEFAULT ((0)),
	[LockState] [bit] NOT NULL CONSTRAINT [DF_OrderHeader_LockState]  DEFAULT ((0)),
	[LockKeeperUserID] [int] NULL,
	[PrintState] [bit] NOT NULL CONSTRAINT [DF_OrderHeader_PrintState]  DEFAULT ((0)),
	[CancelCause] [nvarchar](255) NULL,
	[State] [tinyint] NOT NULL,
 CONSTRAINT [PK_OrderHeader] PRIMARY KEY CLUSTERED 
(
	[OrderNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0=New; 1=Edited; 2=Canceled; 3=Ready To Serve; 4=Served; 5=Finished; 6= Cancel Finished' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderHeader', @level2type=N'COLUMN',@level2name=N'State'
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01/23/2012 12:11:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [varchar](32) NOT NULL,
	[PermissionID] [tinyint] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[Get_All_Units]    Script Date: 01/23/2012 12:10:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_All_Units]

 AS
	SELECT UnitName,ShortName FROM Units
	ORDER BY UnitName
GO
/****** Object:  StoredProcedure [dbo].[Update_Unit]    Script Date: 01/23/2012 12:10:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Unit]

	@UnitName NVARCHAR(50),
	@ShortName NVARCHAR(10)
 AS
	UPDATE Units SET ShortName=@ShortName
	WHERE UnitName=@UnitName
GO
/****** Object:  StoredProcedure [dbo].[Create_Unit]    Script Date: 01/23/2012 12:10:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Create_Unit]

	@UnitName NVARCHAR(50),
	@ShortName NVARCHAR(10)
AS
	INSERT INTO Units (UnitName,ShortName)
	VALUES (@UnitName,@ShortName)
GO
/****** Object:  StoredProcedure [dbo].[Delete_Unit]    Script Date: 01/23/2012 12:10:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Delete_Unit]

	@UnitName NVARCHAR(50)
AS
	DELETE Units 
	WHERE UnitName=@UnitName
GO
/****** Object:  StoredProcedure [dbo].[Create_Product]    Script Date: 01/23/2012 12:10:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Create_Product]

	@ProductName NVARCHAR(100),
	@GroupID INT,
	@UnitName NVARCHAR(50),
	@Price DECIMAL(18,2)
AS
	EXEC Update_DataHasChanged 'Products'

	INSERT INTO Products (ProductName,GroupID,UnitName,Price,Availability)
	VALUES (@ProductName,@GroupID,@UnitName,@Price,'1')
GO
/****** Object:  StoredProcedure [dbo].[Update_Table_State]    Script Date: 01/23/2012 12:10:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure updates the state of the specified restaurant table to Occupied or Empty
-- =============================================
CREATE PROCEDURE [dbo].[Update_Table_State]
	@TableNo VARCHAR(10),
	@State BIT
AS
BEGIN
	UPDATE RestaurantTables SET [State]=@State WHERE TableNo=@TableNo
END
GO
/****** Object:  StoredProcedure [dbo].[Get_All_Tables]    Script Date: 01/23/2012 12:10:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_All_Tables]

 AS
	SELECT TableNo,Capacity,[Description],[State] FROM RestaurantTables
	ORDER BY [State],TableNo,Capacity
GO
/****** Object:  StoredProcedure [dbo].[Delete_Table]    Script Date: 01/23/2012 12:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Delete_Table]

	@TableNo VARCHAR(10)
AS
	DELETE RestaurantTables 
	WHERE TableNo=@TableNo
GO
/****** Object:  StoredProcedure [dbo].[Update_Table]    Script Date: 01/23/2012 12:10:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Table]

	@TableNo VARCHAR(10),
	@Capacity TINYINT,
	@Description NVARCHAR(100)
 AS
	UPDATE RestaurantTables SET Capacity=@Capacity , [Description]=@Description 
	WHERE TableNo=@TableNo
GO
/****** Object:  StoredProcedure [dbo].[Create_Table]    Script Date: 01/23/2012 12:10:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Create_Table]

	@TableNo VARCHAR(10),
	@Capacity TINYINT,
	@Description NVARCHAR(100)
AS
	INSERT INTO RestaurantTables (TableNo,Capacity,[Description])
	VALUES (@TableNo,@Capacity,@Description)
GO
/****** Object:  StoredProcedure [dbo].[Create_Group]    Script Date: 01/23/2012 12:10:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Create_Group]

	@GroupName NVARCHAR(50)
AS
	EXEC Update_DataHasChanged 'Groups'

	INSERT INTO Groups (GroupName)
	VALUES (@GroupName)
GO
/****** Object:  StoredProcedure [dbo].[Delete_Group]    Script Date: 01/23/2012 12:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Delete_Group]

	@GroupID INT
AS
	EXEC Update_DataHasChanged 'Groups'

	DELETE Groups 
	WHERE GroupID=@GroupID
GO
/****** Object:  StoredProcedure [dbo].[Update_Group]    Script Date: 01/23/2012 12:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Group]

	@GroupID INT,
	@GroupName NVARCHAR(50)
 AS
	EXEC Update_DataHasChanged 'Groups'

	UPDATE Groups SET GroupName=@GroupName
	WHERE GroupID=@GroupID
GO
/****** Object:  StoredProcedure [dbo].[Update_Product_Availability]    Script Date: 01/23/2012 12:10:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Product_Availability]
	@ProductID INT,
	@Availability BIT
AS
BEGIN
	EXEC Update_DataHasChanged 'Products'

	UPDATE Products SET Availability=@Availability
	WHERE ProductID=@ProductID
END
GO
/****** Object:  StoredProcedure [dbo].[Delete_Product]    Script Date: 01/23/2012 12:10:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Delete_Product]

	@ProductID INT
AS
	EXEC Update_DataHasChanged 'Products'

	DELETE Products 
	WHERE ProductID=@ProductID
GO
/****** Object:  StoredProcedure [dbo].[Update_Product]    Script Date: 01/23/2012 12:10:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Update_Product]

	@ProductID INT,
	@ProductName NVARCHAR(100),
	@GroupID INT,
	@UnitName NVARCHAR(50),
	@Price DECIMAL(18,2)
 AS
	EXEC Update_DataHasChanged 'Products'

	UPDATE Products SET ProductName=@ProductName , UnitName=@UnitName ,GroupID=@GroupID, Price=@Price
	WHERE ProductID=@ProductID
GO
/****** Object:  StoredProcedure [dbo].[Get_All_Users]    Script Date: 01/23/2012 12:10:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_All_Users]

 AS
	SELECT     [dbo].[Users].UserID, [Permissions].Description, [dbo].[Users].FullName, Users.UserName, Users.PermissionID
	FROM         Users INNER JOIN
                      [Permissions] ON Users.PermissionID = [Permissions].PermissionID
GO
/****** Object:  StoredProcedure [dbo].[Cancel_Order]    Script Date: 01/23/2012 12:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure will do the process of checking out of an order
-- =============================================
CREATE PROCEDURE [dbo].[Cancel_Order]
	@OrderNo BIGINT,
	@CancelCause NVARCHAR(255)
AS
BEGIN
	DECLARE @NoOFActiveOrders INT, @TableNo VARCHAR(10)
	SET @NoOFActiveOrders=-1
	BEGIN TRAN T1;
	BEGIN TRY
		-- Update the OrderHeader
		UPDATE OrderHeader SET [State]=2, CancelCause=@CancelCause WHERE OrderNo=@OrderNo

		--Retreive the TableNo of the order
		SELECT @TableNo=TableNo FROM OrderHeader WHERE OrderNo=@OrderNo
		
		-- Get the no of active orders for an specified tableNo
		SELECT @NoOFActiveOrders=COUNT(*) FROM OrderHeader WHERE ([State]=0 OR [State]=1 OR [State]=3 OR [State]=4) AND TableNo=@TableNo
		IF @NoOFActiveOrders=0
		BEGIN
			--Frees the table
			EXEC Update_Table_State @TableNo,0
		END
		COMMIT TRAN T1;
		SELECT 1 AS RowsAffected
	END TRY
	BEGIN CATCH
		-- returns 0
		SELECT 0 AS RowsAffected
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Check_Out_Order]    Script Date: 01/23/2012 12:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure will do the process of checking out of an order
-- =============================================
CREATE PROCEDURE [dbo].[Check_Out_Order]
	@OrderNo BIGINT,
	@TotalPrice DECIMAL(18,2),
	@CustomerName NVARCHAR(50)
AS
BEGIN
	DECLARE @NoOFActiveOrders INT, @TableNo VARCHAR(10)
	SET @NoOFActiveOrders=-1
	BEGIN TRAN T1;
	BEGIN TRY
		-- Update the OrderHeader
		UPDATE OrderHeader SET TotalPrice=@TotalPrice,CustomerName=@CustomerName,[State]=5 WHERE OrderNo=@OrderNo

		--Retreive the TableNo of the order
		SELECT @TableNo=TableNo FROM OrderHeader WHERE OrderNo=@OrderNo
		
		-- Get the no of active orders for an specified tableNo
		SELECT @NoOFActiveOrders=COUNT(*) FROM OrderHeader WHERE ([State]=0 OR [State]=1 OR [State]=3 OR [State]=4) AND TableNo=@TableNo
		IF @NoOFActiveOrders=0
		BEGIN
			--Frees the table
			EXEC Update_Table_State @TableNo,0
		END
		COMMIT TRAN T1;
		SELECT 1 AS RowsAffected
	END TRY
	BEGIN CATCH
		-- returns 0
		SELECT 0 AS RowsAffected
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Make_OrderHeader]    Script Date: 01/23/2012 12:10:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure creates a new OrderHeader and returns the created OrderNo
-- =============================================
CREATE PROCEDURE [dbo].[Make_OrderHeader]
	@TableNo VARCHAR(10),
	@CreatorUserID INT
AS
BEGIN
	BEGIN TRAN T1;
	BEGIN TRY
		--occupies the table
		EXEC Update_Table_State @TableNo,1

		INSERT INTO OrderHeader(TableNo,CreatorUserID,CreationDatetime,PrintState,[State])
		VALUES(@TableNo,@CreatorUserID,GETDATE(),0,0)

		COMMIT TRAN T1;

		-- returns the saved OrderNo to be used in order detail
		SELECT @@Identity AS OrderNo
	END TRY
	BEGIN CATCH
		-- returns 0
		SELECT 0 AS OrderNo
	END CATCH;
	
END
GO
/****** Object:  StoredProcedure [dbo].[Update_Order_TableNo]    Script Date: 01/23/2012 12:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ali Daneshmandi
-- Description:	This procedure updates the TableNo of an order
-- =============================================
CREATE PROCEDURE [dbo].[Update_Order_TableNo]
	@OrderNo BIGINT,
	@CurrentTableNo VARCHAR(10),
	@NewTableNo VARCHAR(10)
AS
BEGIN	
	DECLARE @NoOfActiveOrders INT
	SET @NoOfActiveOrders=-1
	BEGIN TRAN T1;
		-- Get the no of active orders for an specified tableNo
		SELECT @NoOfActiveOrders=COUNT(*) FROM OrderHeader WHERE ([State]=0 OR [State]=1 OR [State]=3 OR [State]=4) AND TableNo=@CurrentTableNo
		IF @NoOfActiveOrders=0
		BEGIN
			--Frees the table
			EXEC Update_Table_State @CurrentTableNo,0
		END

	--occupies the table
	EXEC Update_Table_State @NewTableNo,1

	UPDATE OrderHeader SET TableNo=@NewTableNo,LastEditionDatetime=GETDATE() 
	WHERE OrderNo=@OrderNo
	COMMIT TRAN T1;
END
GO
/****** Object:  ForeignKey [FK_OrderDetail_OrderHeader]    Script Date: 01/23/2012 12:10:57 ******/
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_OrderHeader] FOREIGN KEY([OrderNo])
REFERENCES [dbo].[OrderHeader] ([OrderNo])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_OrderHeader]
GO
/****** Object:  ForeignKey [FK_OrderDetail_Products]    Script Date: 01/23/2012 12:10:58 ******/
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Products]
GO
/****** Object:  ForeignKey [FK_OrderHeader_RestaurantTables]    Script Date: 01/23/2012 12:11:05 ******/
ALTER TABLE [dbo].[OrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_OrderHeader_RestaurantTables] FOREIGN KEY([TableNo])
REFERENCES [dbo].[RestaurantTables] ([TableNo])
GO
ALTER TABLE [dbo].[OrderHeader] CHECK CONSTRAINT [FK_OrderHeader_RestaurantTables]
GO
/****** Object:  ForeignKey [FK_Products_Groups]    Script Date: 01/23/2012 12:11:10 ******/
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Groups] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([GroupID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Groups]
GO
/****** Object:  ForeignKey [FK_Products_Units]    Script Date: 01/23/2012 12:11:11 ******/
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Units] FOREIGN KEY([UnitName])
REFERENCES [dbo].[Units] ([UnitName])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Units]
GO
/****** Object:  ForeignKey [FK_Users_Permission]    Script Date: 01/23/2012 12:11:21 ******/
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Permission] FOREIGN KEY([PermissionID])
REFERENCES [dbo].[Permissions] ([PermissionID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Permission]
GO


INSERT INTO [dbo].[Permissions] ([Description]) VALUES('Manager')
GO
INSERT INTO [dbo].[Permissions] ([Description]) VALUES('Cashier')
GO
INSERT INTO [dbo].[Permissions] ([Description]) VALUES('Kitchen User')
GO
INSERT INTO [dbo].[Permissions] ([Description]) VALUES('Waiter')
GO

INSERT INTO [dbo].[Users] ([FullName],[UserName],[Password],[PermissionID]) VALUES('Manager','Manager','AE94BE3CD532CE4A025884819EB08C98',1)
GO

