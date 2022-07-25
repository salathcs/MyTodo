USE [MyTodo]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2022. 07. 25. 22:55:55 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[identities]    Script Date: 2022. 07. 25. 22:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[identities](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[userName] [nvarchar](32) NOT NULL,
	[password] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_identities] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[permissions]    Script Date: 2022. 07. 25. 22:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permissions](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[permissionName] [nvarchar](16) NOT NULL,
	[created] [datetime2](7) NOT NULL,
	[createdBy] [nvarchar](32) NOT NULL,
	[updated] [datetime2](7) NOT NULL,
	[updatedBy] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_permissions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[todos]    Script Date: 2022. 07. 25. 22:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[todos](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](32) NOT NULL,
	[description] [nvarchar](max) NULL,
	[expiration] [datetime2](7) NULL,
	[userId] [bigint] NOT NULL,
	[created] [datetime2](7) NOT NULL,
	[createdBy] [nvarchar](32) NOT NULL,
	[updated] [datetime2](7) NOT NULL,
	[updatedBy] [nvarchar](32) NOT NULL,
	[emailSent] [bit] NULL,
 CONSTRAINT [PK_todos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userPermissions]    Script Date: 2022. 07. 25. 22:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userPermissions](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[userId] [bigint] NOT NULL,
	[permissionId] [bigint] NOT NULL,
 CONSTRAINT [PK_userPermissions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 2022. 07. 25. 22:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](32) NOT NULL,
	[email] [nvarchar](32) NOT NULL,
	[identityId] [bigint] NOT NULL,
	[created] [datetime2](7) NOT NULL,
	[createdBy] [nvarchar](32) NOT NULL,
	[updated] [datetime2](7) NOT NULL,
	[updatedBy] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220719200535_User_Identity', N'6.0.7')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220720093437_Basic_Structure', N'6.0.7')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220720183437_Permission_UniqueName', N'6.0.7')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220724120924_Todo_EmailSent', N'6.0.7')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220724133450_EmailWorker_User', N'6.0.7')
GO
SET IDENTITY_INSERT [dbo].[identities] ON 
GO
INSERT [dbo].[identities] ([id], [userName], [password]) VALUES (1, N'admin', N'admin')
GO
INSERT [dbo].[identities] ([id], [userName], [password]) VALUES (2, N'emailWorker', N'r1eH#emE295&')
GO
SET IDENTITY_INSERT [dbo].[identities] OFF
GO
SET IDENTITY_INSERT [dbo].[permissions] ON 
GO
INSERT [dbo].[permissions] ([id], [permissionName], [created], [createdBy], [updated], [updatedBy]) VALUES (1, N'AdminPermission', CAST(N'2022-07-24T13:34:50.1222709' AS DateTime2), N'System', CAST(N'2022-07-24T13:34:50.1222709' AS DateTime2), N'System')
GO
SET IDENTITY_INSERT [dbo].[permissions] OFF
GO
SET IDENTITY_INSERT [dbo].[userPermissions] ON 
GO
INSERT [dbo].[userPermissions] ([id], [userId], [permissionId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[userPermissions] ([id], [userId], [permissionId]) VALUES (2, 2, 1)
GO
SET IDENTITY_INSERT [dbo].[userPermissions] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 
GO
INSERT [dbo].[users] ([id], [name], [email], [identityId], [created], [createdBy], [updated], [updatedBy]) VALUES (1, N'MyAdmin', N'MyAdmin@tmp.com', 1, CAST(N'2022-07-24T13:34:50.1222229' AS DateTime2), N'System', CAST(N'2022-07-24T13:34:50.1222232' AS DateTime2), N'System')
GO
INSERT [dbo].[users] ([id], [name], [email], [identityId], [created], [createdBy], [updated], [updatedBy]) VALUES (2, N'EmailWorker', N'MyAdmin@tmp.com', 2, CAST(N'2022-07-24T13:34:50.1222234' AS DateTime2), N'System', CAST(N'2022-07-24T13:34:50.1222235' AS DateTime2), N'System')
GO
SET IDENTITY_INSERT [dbo].[users] OFF
GO
ALTER TABLE [dbo].[todos]  WITH CHECK ADD  CONSTRAINT [FK_todos_users_userId] FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[todos] CHECK CONSTRAINT [FK_todos_users_userId]
GO
ALTER TABLE [dbo].[userPermissions]  WITH CHECK ADD  CONSTRAINT [FK_userPermissions_permissions_permissionId] FOREIGN KEY([permissionId])
REFERENCES [dbo].[permissions] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[userPermissions] CHECK CONSTRAINT [FK_userPermissions_permissions_permissionId]
GO
ALTER TABLE [dbo].[userPermissions]  WITH CHECK ADD  CONSTRAINT [FK_userPermissions_users_userId] FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[userPermissions] CHECK CONSTRAINT [FK_userPermissions_users_userId]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_identities_identityId] FOREIGN KEY([identityId])
REFERENCES [dbo].[identities] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_identities_identityId]
GO
