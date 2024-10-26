USE [WellMeetDb]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 10/26/2024 11:20:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookingDate] [datetime2](7) NOT NULL,
	[AppointmentDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[PsychiatristId] [int] NOT NULL,
	[PaymentId] [int] NULL,
	[TimeSlotId] [int] NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 10/26/2024 11:20:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](18, 2) NULL,
	[PaymentDate] [datetime] NULL,
	[PaymentStatus] [nvarchar](50) NULL,
	[AppointmentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Psychiatrists]    Script Date: 10/26/2024 11:20:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Psychiatrists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Specialization] [nvarchar](max) NOT NULL,
	[UserId] [int] NULL,
	[Bio] [nvarchar](max) NULL,
	[Experience] [nvarchar](max) NULL,
	[Location] [varchar](max) NULL,
	[ConsultationFee] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Psychiatrist] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeSlots]    Script Date: 10/26/2024 11:20:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSlots](
	[TimeSlotId] [int] IDENTITY(1,1) NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[PsychiatristId] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[SlotDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[TimeSlotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tokens]    Script Date: 10/26/2024 11:20:01 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 10/26/2024 11:20:01 PM ******/
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
	[Role] [nvarchar](50) NULL,
	[UserImage] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Psychiatrists] ON 

INSERT [dbo].[Psychiatrists] ([Id], [Specialization], [UserId], [Bio], [Experience], [Location], [ConsultationFee]) VALUES (2, N'bezt doctor', 8, N'hello im trong', N'20 years', N'7/20 385 Street', CAST(300000.00 AS Decimal(10, 2)))
INSERT [dbo].[Psychiatrists] ([Id], [Specialization], [UserId], [Bio], [Experience], [Location], [ConsultationFee]) VALUES (3, N'bezt doom', 9, N'hello world', N'18years ', N'183 tran nhat duat', CAST(300000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Psychiatrists] OFF
GO
SET IDENTITY_INSERT [dbo].[TimeSlots] ON 

INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (2, CAST(N'09:00:00' AS Time), CAST(N'09:30:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-26' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (4, CAST(N'10:00:00' AS Time), CAST(N'10:30:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-26' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (11, CAST(N'10:30:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-26' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (12, CAST(N'11:00:00' AS Time), CAST(N'11:30:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-26' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (13, CAST(N'11:30:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-26' AS Date))
SET IDENTITY_INSERT [dbo].[TimeSlots] OFF
GO
SET IDENTITY_INSERT [dbo].[Tokens] ON 

INSERT [dbo].[Tokens] ([Id], [PasswordResetToken], [PasswordResetTokenExpiration], [UserId]) VALUES (13, N'uIztmy7Gn6P5dxi3FIU7o4nesLLL7xBmxGUA0BCAIMvpXcFEURBrEAmLZeWh9CRL5cATdqqhkpxUlG9TCAV3Rw==', CAST(N'2024-08-30T06:13:10.1187995' AS DateTime2), 6)
INSERT [dbo].[Tokens] ([Id], [PasswordResetToken], [PasswordResetTokenExpiration], [UserId]) VALUES (16, N'q+BE6xcen9YgnVLxopaneTMMGGFyjHxkEUbXKf4+gFoDu8NrFluPI9wdXQnOPE+OItkxbVxfCBrtGBCQxL9+Yg==', CAST(N'2024-08-30T06:22:28.9706021' AS DateTime2), 6)
SET IDENTITY_INSERT [dbo].[Tokens] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Fullname], [Password], [Email], [DateOfBirth], [Phonenumber], [Address], [Gender], [Role], [UserImage]) VALUES (6, N'string', N'0Goa9ixVLUa+kAUF4cpHrB+T//pvleQDU0b1COX421hVQjn9', N'example@gmail.com', CAST(N'2024-08-30T05:53:35.4710000' AS DateTime2), N'string', N'string', N'Male', N'USER', N'string')
INSERT [dbo].[Users] ([Id], [Fullname], [Password], [Email], [DateOfBirth], [Phonenumber], [Address], [Gender], [Role], [UserImage]) VALUES (8, N'Le Quang Trong', N'T4O3RwcobwV0j+XuFNdR+sRJVFW/nsyzltT93ekpbSMHSqQ2', N'trongle278@gmail.com', CAST(N'2002-09-24T16:02:43.3210000' AS DateTime2), N'01887052354', N'7/20 385 Street', N'Female', N'PSYCHIATRIST', N'string')
INSERT [dbo].[Users] ([Id], [Fullname], [Password], [Email], [DateOfBirth], [Phonenumber], [Address], [Gender], [Role], [UserImage]) VALUES (9, N'Xion von Doom', N'0CjHA1XhK7ZM7+CrcbGPIiGb9f+Bb6zj+Zy0l5jS4QC+823s', N'xion278@gmail.com', CAST(N'2001-09-26T15:41:53.9640000' AS DateTime2), N'0321654987', N'12 nguyen xien', N'Male', N'PSYCHIATRIST', N'string')
INSERT [dbo].[Users] ([Id], [Fullname], [Password], [Email], [DateOfBirth], [Phonenumber], [Address], [Gender], [Role], [UserImage]) VALUES (11, N'string', N'J13Q16LR0md7mEv0GZCr3OrtXQKnv3pW4NIHxqyMh16USGq7', N'string', CAST(N'2024-10-19T11:45:56.8090000' AS DateTime2), N'string', N'string', N'Male', N'USER', N'string')
INSERT [dbo].[Users] ([Id], [Fullname], [Password], [Email], [DateOfBirth], [Phonenumber], [Address], [Gender], [Role], [UserImage]) VALUES (12, N'string', N'SJSoUCDQsmLB1r2S7eOS5OoS1JWLHzafcuXEVQjF+doyQ4vZ', N'string1', CAST(N'2024-10-24T14:38:58.3110000' AS DateTime2), N'string', N'string', N'string', N'USER', N'string')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Payment] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payment] ([Id])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Payment]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_TimeSlot] FOREIGN KEY([TimeSlotId])
REFERENCES [dbo].[TimeSlots] ([TimeSlotId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_TimeSlot]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Psychiatrist_PsychiatristId] FOREIGN KEY([PsychiatristId])
REFERENCES [dbo].[Psychiatrists] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Booking_Psychiatrist_PsychiatristId]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Booking_Users_UserId]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Appointment] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Appointment]
GO
ALTER TABLE [dbo].[Psychiatrists]  WITH CHECK ADD  CONSTRAINT [FK_Psychiatrists_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Psychiatrists] CHECK CONSTRAINT [FK_Psychiatrists_Users_UserId]
GO
ALTER TABLE [dbo].[TimeSlots]  WITH CHECK ADD FOREIGN KEY([PsychiatristId])
REFERENCES [dbo].[Psychiatrists] ([Id])
GO
ALTER TABLE [dbo].[Tokens]  WITH CHECK ADD  CONSTRAINT [FK_Tokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tokens] CHECK CONSTRAINT [FK_Tokens_Users_UserId]
GO
