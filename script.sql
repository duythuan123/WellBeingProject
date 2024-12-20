USE [master]
GO
CREATE DATABASE [WellMeetDb]
GO
ALTER DATABASE [WellMeetDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WellMeetDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WellMeetDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WellMeetDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WellMeetDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WellMeetDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WellMeetDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [WellMeetDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WellMeetDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WellMeetDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WellMeetDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WellMeetDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WellMeetDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WellMeetDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WellMeetDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WellMeetDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WellMeetDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [WellMeetDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WellMeetDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WellMeetDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WellMeetDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WellMeetDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WellMeetDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WellMeetDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WellMeetDb] SET RECOVERY FULL 
GO
ALTER DATABASE [WellMeetDb] SET  MULTI_USER 
GO
ALTER DATABASE [WellMeetDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WellMeetDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WellMeetDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WellMeetDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WellMeetDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WellMeetDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'WellMeetDb', N'ON'
GO
ALTER DATABASE [WellMeetDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [WellMeetDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [WellMeetDb]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 10/31/2024 7:02:54 PM ******/
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
	[Reason] [nvarchar](255) NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 10/31/2024 7:02:54 PM ******/
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
/****** Object:  Table [dbo].[Psychiatrists]    Script Date: 10/31/2024 7:02:54 PM ******/
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
/****** Object:  Table [dbo].[TimeSlots]    Script Date: 10/31/2024 7:02:54 PM ******/
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
/****** Object:  Table [dbo].[Tokens]    Script Date: 10/31/2024 7:02:54 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 10/31/2024 7:02:54 PM ******/
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
SET IDENTITY_INSERT [dbo].[Appointment] ON 

INSERT [dbo].[Appointment] ([Id], [BookingDate], [AppointmentDate], [Status], [UserId], [PsychiatristId], [PaymentId], [TimeSlotId], [Reason]) VALUES (1, CAST(N'2024-10-31T18:58:29.8158319' AS DateTime2), CAST(N'2024-10-30T17:00:00.0000000' AS DateTime2), N'PAID', 4, 1, 1, 17, N'Tram Cam')
SET IDENTITY_INSERT [dbo].[Appointment] OFF
GO
SET IDENTITY_INSERT [dbo].[Payment] ON 

INSERT [dbo].[Payment] ([Id], [Amount], [PaymentDate], [PaymentStatus], [AppointmentId]) VALUES (1, CAST(300000.00 AS Decimal(18, 2)), CAST(N'2024-10-31T18:59:21.790' AS DateTime), N'SUCCESS', 1)
SET IDENTITY_INSERT [dbo].[Payment] OFF
GO
SET IDENTITY_INSERT [dbo].[Psychiatrists] ON 

INSERT [dbo].[Psychiatrists] ([Id], [Specialization], [UserId], [Bio], [Experience], [Location], [ConsultationFee]) VALUES (1, N'Tâm lý học trẻ em', 4, N'Bác sĩ Jane Doe là một bác sĩ tâm lý được chứng nhận, chuyên về sức khỏe tâm thần trẻ em và thanh thiếu niên với hơn 10 năm kinh nghiệm.', N'10 năm', N'New York, NY', CAST(300000.00 AS Decimal(10, 2)))
INSERT [dbo].[Psychiatrists] ([Id], [Specialization], [UserId], [Bio], [Experience], [Location], [ConsultationFee]) VALUES (2, N'Tâm lý học nghiện', 5, N'Bác sĩ John Smith là một bác sĩ tâm lý chuyên về điều trị nghiện, với đam mê giúp đỡ mọi người vượt qua các vấn đề về lạm dụng chất kích thích.', N'15 năm', N'Los Angeles, CA', CAST(500000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Psychiatrists] OFF
GO
SET IDENTITY_INSERT [dbo].[TimeSlots] ON 

INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (1, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-15' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (2, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-16' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (3, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-17' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (4, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-18' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (5, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-19' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (6, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-20' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (7, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-21' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (8, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-22' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (9, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-23' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (10, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-24' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (11, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-25' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (12, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-26' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (13, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-27' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (14, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-28' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (15, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-29' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (16, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-30' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (17, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'BOOKED', CAST(N'2024-10-31' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (18, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-01' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (19, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-02' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (20, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-03' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (21, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-04' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (22, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-05' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (23, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-06' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (24, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-07' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (25, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-08' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (26, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-09' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (27, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-10' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (28, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-11' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (29, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-12' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (30, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-13' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (31, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-15' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (32, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-16' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (33, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-17' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (34, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-18' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (35, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-19' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (36, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-20' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (37, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-21' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (38, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-22' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (39, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-23' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (40, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-24' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (41, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-25' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (42, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-26' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (43, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-27' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (44, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-28' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (45, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-29' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (46, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-30' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (47, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-31' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (48, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-01' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (49, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-02' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (50, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-03' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (51, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-04' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (52, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-05' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (53, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-06' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (54, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-07' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (55, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-08' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (56, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-09' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (57, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-10' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (58, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-11' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (59, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-12' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (60, CAST(N'09:00:00' AS Time), CAST(N'10:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-13' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (61, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-15' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (62, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-16' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (63, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-17' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (64, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-18' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (65, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-19' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (66, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-20' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (67, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-21' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (68, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-22' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (69, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-23' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (70, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-24' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (71, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-25' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (72, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-26' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (73, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-27' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (74, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-28' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (75, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-29' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (76, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-30' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (77, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-31' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (78, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-01' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (79, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-02' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (80, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-03' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (81, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-04' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (82, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-05' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (83, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-06' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (84, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-07' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (85, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-08' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (86, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-09' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (87, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-10' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (88, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-11' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (89, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-12' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (90, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-13' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (91, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-15' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (92, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-16' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (93, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-17' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (94, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-18' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (95, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-19' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (96, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-20' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (97, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-21' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (98, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-22' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (99, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-23' AS Date))
GO
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (100, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-24' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (101, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-25' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (102, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-26' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (103, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-27' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (104, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-28' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (105, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-29' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (106, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-30' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (107, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-31' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (108, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-01' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (109, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-02' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (110, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-03' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (111, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-04' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (112, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-05' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (113, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-06' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (114, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-07' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (115, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-08' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (116, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-09' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (117, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-10' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (118, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-11' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (119, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-12' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (120, CAST(N'10:00:00' AS Time), CAST(N'11:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-13' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (121, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-15' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (122, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-16' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (123, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-17' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (124, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-18' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (125, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-19' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (126, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-20' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (127, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-21' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (128, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-22' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (129, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-23' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (130, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-24' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (131, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-25' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (132, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-26' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (133, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-27' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (134, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-28' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (135, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-29' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (136, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-30' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (137, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-10-31' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (138, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-01' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (139, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-02' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (140, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-03' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (141, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-04' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (142, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-05' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (143, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-06' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (144, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-07' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (145, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-08' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (146, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-09' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (147, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-10' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (148, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-11' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (149, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-12' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (150, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 2, N'AVAILABLE', CAST(N'2024-11-13' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (151, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-15' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (152, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-16' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (153, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-17' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (154, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-18' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (155, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-19' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (156, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-20' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (157, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-21' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (158, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-22' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (159, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-23' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (160, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-24' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (161, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-25' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (162, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-26' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (163, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-27' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (164, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-28' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (165, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-29' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (166, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-30' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (167, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-10-31' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (168, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-01' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (169, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-02' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (170, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-03' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (171, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-04' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (172, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-05' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (173, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-06' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (174, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-07' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (175, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-08' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (176, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-09' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (177, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-10' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (178, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-11' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (179, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-12' AS Date))
INSERT [dbo].[TimeSlots] ([TimeSlotId], [StartTime], [EndTime], [PsychiatristId], [Status], [SlotDate]) VALUES (180, CAST(N'11:00:00' AS Time), CAST(N'12:00:00' AS Time), 1, N'AVAILABLE', CAST(N'2024-11-13' AS Date))
SET IDENTITY_INSERT [dbo].[TimeSlots] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Fullname], [Password], [Email], [DateOfBirth], [Phonenumber], [Address], [Gender], [Role], [UserImage]) VALUES (1, N'Test', N'qNvVWDaAr0Bk3FXXwrKJ3Bcbu4hAh4fNXqvomI06hUP4qWNp', N'string', CAST(N'2002-10-31T11:14:39.5910000' AS DateTime2), N'1234567890', N'Nowhere', N'Male', N'USER', N'')
INSERT [dbo].[Users] ([Id], [Fullname], [Password], [Email], [DateOfBirth], [Phonenumber], [Address], [Gender], [Role], [UserImage]) VALUES (2, N'Pham Cao Duy Thuan', N'ykDOVydSQpI+GpvHx7nLCOCzqQ6DfpTFCfBDGe9MTXGXndm4', N'phamcaoduythuan@gmail.com', CAST(N'2002-10-26T11:14:39.5910000' AS DateTime2), N'0765701638', N'42 Nguyen Thuong Hien', N'Male', N'USER', N'')
INSERT [dbo].[Users] ([Id], [Fullname], [Password], [Email], [DateOfBirth], [Phonenumber], [Address], [Gender], [Role], [UserImage]) VALUES (3, N'Admin', N'wxjNGAgRu5OoHNEo2zNfEdiQLXgplwFpt01K0AAI4YeL/zBo', N'admin@gmail.com', CAST(N'2002-11-30T11:14:39.5910000' AS DateTime2), N'0768745158', N'Nowhere', N'Male', N'ADMIN', N'')
INSERT [dbo].[Users] ([Id], [Fullname], [Password], [Email], [DateOfBirth], [Phonenumber], [Address], [Gender], [Role], [UserImage]) VALUES (4, N'Bác sĩ Jane Doe', N'wxjNGAgRu5OoHNEo2zNfEdiQLXgplwFpt01K0AAI4YeL/zBo', N'janedoe@example.com', CAST(N'1980-05-15T00:00:00.0000000' AS DateTime2), N'5551234567', N'123 Đường Main, New York, NY 10001', N'Female', N'PSYCHIATRIST', N'')
INSERT [dbo].[Users] ([Id], [Fullname], [Password], [Email], [DateOfBirth], [Phonenumber], [Address], [Gender], [Role], [UserImage]) VALUES (5, N'Bác sĩ John Smith', N'4EaiKctQEVXmjBJtrUbaN9K+gM3BukSuNXdyWIkBNTsMoC3/', N'johnsmith@example.com', CAST(N'1975-11-20T00:00:00.0000000' AS DateTime2), N'5559876543', N'456 Đường Elm, Los Angeles, CA 90001', N'Male', N'PSYCHIATRIST', N'')
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
USE [master]
GO
ALTER DATABASE [WellMeetDb] SET  READ_WRITE 
GO
