USE [SecondChances]
GO

/****** Object:  Table [dbo].[SecondChances]    Script Date: 2024-01-27 23:36:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SecondChances](
	[CompanyID] [int] NOT NULL,
	[ObjectID] [int] IDENTITY(1,1) NOT NULL,
	[InventoryID] [int] NULL,
	[CustomerID] [int] NULL,
	[CustomerLocationID] [int] NULL,
	[SiteID] [int] NULL,
	[LocationID] [int] NULL,
	[EmployeeID] [int] NULL,
	[OwnerID] [int] NULL,
	[WorkGroupID] [int] NULL,
	[Status] [char](1) NULL,
	[Descr] [nvarchar](256) NULL,
	[ImageUrl] [varchar](255) NULL,
	[Body] [nvarchar](max) NULL,
	[ItemClassID] [int] NULL,
	[ContactID] [int] NULL,
	[ShipDestType] [char](1) NULL,
	[ShipToSiteID] [int] NULL,
	[ShipToBAccountID] [int] NULL,
	[ShipToLocationID] [int] NULL,
	[ShipAddressID] [int] NULL,
	[ShipContactID] [int] NULL,
	[NoteID] [uniqueidentifier] NOT NULL,
	[CreatedByID] [uniqueidentifier] NOT NULL,
	[CreatedByScreenID] [char](8) NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[LastModifiedByID] [uniqueidentifier] NOT NULL,
	[LastModifiedByScreenID] [char](8) NOT NULL,
	[LastModifiedDateTime] [datetime] NOT NULL,
	[tstamp] [timestamp] NOT NULL,
 CONSTRAINT [SecondChances_PK] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC,
	[ObjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[SecondChances] ADD  CONSTRAINT [DF__SecondCha__Compa__5B10E04F]  DEFAULT ((0)) FOR [CompanyID]
GO


