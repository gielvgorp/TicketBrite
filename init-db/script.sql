USE [TicketBrite]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/11/2024 9:15:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 11/11/2024 9:15:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[eventID] [uniqueidentifier] NOT NULL,
	[organizationID] [uniqueidentifier] NOT NULL,
	[eventName] [nvarchar](max) NOT NULL,
	[eventDescription] [nvarchar](max) NOT NULL,
	[eventDateTime] [datetime2](7) NOT NULL,
	[eventLocation] [nvarchar](max) NOT NULL,
	[eventAge] [int] NOT NULL,
	[eventCategory] [nvarchar](max) NOT NULL,
	[eventImage] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[eventID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Guests]    Script Date: 11/11/2024 9:15:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guests](
	[guestID] [uniqueidentifier] NOT NULL,
	[guestName] [nvarchar](max) NOT NULL,
	[guestEmail] [nvarchar](max) NOT NULL,
	[verificationCode] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Guests] PRIMARY KEY CLUSTERED 
(
	[guestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organizations]    Script Date: 11/11/2024 9:15:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organizations](
	[organizationID] [uniqueidentifier] NOT NULL,
	[organizationName] [nvarchar](max) NOT NULL,
	[organizationEmail] [nvarchar](max) NOT NULL,
	[organizationPhone] [nvarchar](max) NOT NULL,
	[organizationAddress] [nvarchar](max) NULL,
	[organizationWebsite] [nvarchar](max) NULL,
 CONSTRAINT [PK_Organizations] PRIMARY KEY CLUSTERED 
(
	[organizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservedTickets]    Script Date: 11/11/2024 9:15:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservedTickets](
	[ticketID] [uniqueidentifier] NOT NULL,
	[userID] [uniqueidentifier] NOT NULL,
	[reservedAt] [datetime2](7) NOT NULL,
	[reservedID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11/11/2024 9:15:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[roleID] [uniqueidentifier] NOT NULL,
	[roleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[roleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SoldTickets]    Script Date: 11/11/2024 9:15:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SoldTickets](
	[purchaseID] [uniqueidentifier] NOT NULL,
	[ticketID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 11/11/2024 9:15:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[ticketID] [uniqueidentifier] NOT NULL,
	[eventID] [uniqueidentifier] NOT NULL,
	[ticketName] [nvarchar](max) NOT NULL,
	[ticketPrice] [decimal](18, 2) NOT NULL,
	[ticketMaxAvailable] [int] NOT NULL,
	[ticketStatus] [bit] NOT NULL,
 CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED 
(
	[ticketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPurchases]    Script Date: 11/11/2024 9:15:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPurchases](
	[purchaseID] [uniqueidentifier] NOT NULL,
	[userID] [uniqueidentifier] NULL,
	[guestID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_UserPurchases] PRIMARY KEY CLUSTERED 
(
	[purchaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/11/2024 9:15:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[userID] [uniqueidentifier] NOT NULL,
	[roleID] [uniqueidentifier] NOT NULL,
	[userName] [nvarchar](max) NOT NULL,
	[userEmail] [nvarchar](max) NOT NULL,
	[userPasswordHash] [nvarchar](max) NOT NULL,
	[organizationID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Guests] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [verificationCode]
GO
ALTER TABLE [dbo].[ReservedTickets] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [reservedID]
GO
