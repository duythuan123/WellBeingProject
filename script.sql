USE [master]
GO
/****** Object:  Database [WellMeetDb]    Script Date: 9/1/2024 7:14:37 AM ******/
CREATE DATABASE [WellMeetDb]
GO

USE [WellMeetDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/1/2024 7:14:37 AM ******/
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
/****** Object:  Table [dbo].[Bookings]    Script Date: 9/1/2024 7:14:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookingDate] [datetime2](7) NOT NULL,
	[AppointmentDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[PsychiatristId] [int] NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Psychiatrists]    Script Date: 9/1/2024 7:14:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Psychiatrists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fullname] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Phonenumber] [nvarchar](max) NOT NULL,
	[Specialization] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Psychiatrist] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tokens]    Script Date: 9/1/2024 7:14:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PasswordResetToken] [nvarchar](max) NOT NULL,
	[PasswordResetTokenExpiration] [datetime2](7) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Tokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/1/2024 7:14:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fullname] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[Phonenumber] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Gender] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240830022950_InitialCreate', N'6.0.33')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240830053536_AddTokenEntity', N'6.0.33')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240830062642_AddBookingAndPsychiatristEntities', N'6.0.33')
GO
SET IDENTITY_INSERT [dbo].[Bookings] ON 

INSERT [dbo].[Bookings] ([Id], [BookingDate], [AppointmentDate], [Status], [UserId], [PsychiatristId]) VALUES (1, CAST(N'2024-08-30T07:03:40.3867836' AS DateTime2), CAST(N'2024-08-30T07:03:35.5720000' AS DateTime2), N'Pending', 7, 1)
INSERT [dbo].[Bookings] ([Id], [BookingDate], [AppointmentDate], [Status], [UserId], [PsychiatristId]) VALUES (2, CAST(N'2024-08-30T07:10:13.5623115' AS DateTime2), CAST(N'2024-08-30T07:10:11.1140000' AS DateTime2), N'Pending', 7, 1)
SET IDENTITY_INSERT [dbo].[Bookings] OFF
GO
SET IDENTITY_INSERT [dbo].[Psychiatrists] ON 

INSERT [dbo].[Psychiatrists] ([Id], [Fullname], [Email], [Phonenumber], [Specialization]) VALUES (1, N'Dr. John Doe', N'johndoe@example.com', N'123-456-7890', N'Clinical Psychology')
SET IDENTITY_INSERT [dbo].[Psychiatrists] OFF
GO
SET IDENTITY_INSERT [dbo].[Tokens] ON 

INSERT [dbo].[Tokens] ([Id], [PasswordResetToken], [PasswordResetTokenExpiration], [UserId]) VALUES (13, N'uIztmy7Gn6P5dxi3FIU7o4nesLLL7xBmxGUA0BCAIMvpXcFEURBrEAmLZeWh9CRL5cATdqqhkpxUlG9TCAV3Rw==', CAST(N'2024-08-30T06:13:10.1187995' AS DateTime2), 6)
INSERT [dbo].[Tokens] ([Id], [PasswordResetToken], [PasswordResetTokenExpiration], [UserId]) VALUES (16, N'q+BE6xcen9YgnVLxopaneTMMGGFyjHxkEUbXKf4+gFoDu8NrFluPI9wdXQnOPE+OItkxbVxfCBrtGBCQxL9+Yg==', CAST(N'2024-08-30T06:22:28.9706021' AS DateTime2), 6)
SET IDENTITY_INSERT [dbo].[Tokens] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Fullname], [Password], [Email], [DateOfBirth], [Phonenumber], [Address], [Gender]) VALUES (6, N'string', N'0Goa9ixVLUa+kAUF4cpHrB+T//pvleQDU0b1COX421hVQjn9', N'example@gmail.com', CAST(N'2024-08-30T05:53:35.4710000' AS DateTime2), N'string', N'string', N'string')
INSERT [dbo].[Users] ([Id], [Fullname], [Password], [Email], [DateOfBirth], [Phonenumber], [Address], [Gender]) VALUES (7, N'string', N'AFXcSBD1kewievrUYQf9pm00ZwZN5xTPOAI3m8m6godh9DY7', N'string1', CAST(N'2024-08-30T05:39:15.2890000' AS DateTime2), N'string', N'string', N'string')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_Booking_PsychiatristId]    Script Date: 9/1/2024 7:14:38 AM ******/
CREATE NONCLUSTERED INDEX [IX_Booking_PsychiatristId] ON [dbo].[Bookings]
(
	[PsychiatristId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Booking_UserId]    Script Date: 9/1/2024 7:14:38 AM ******/
CREATE NONCLUSTERED INDEX [IX_Booking_UserId] ON [dbo].[Bookings]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tokens_UserId]    Script Date: 9/1/2024 7:14:38 AM ******/
CREATE NONCLUSTERED INDEX [IX_Tokens_UserId] ON [dbo].[Tokens]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Psychiatrist_PsychiatristId] FOREIGN KEY([PsychiatristId])
REFERENCES [dbo].[Psychiatrists] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Booking_Psychiatrist_PsychiatristId]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Booking_Users_UserId]
GO
ALTER TABLE [dbo].[Tokens]  WITH CHECK ADD  CONSTRAINT [FK_Tokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tokens] CHECK CONSTRAINT [FK_Tokens_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [WellMeetDb] SET  READ_WRITE 
GO
