CREATE TABLE [dbo].[UserInPhoneCalls](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PhoneCallId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UserInPhoneCalls] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserInPhoneCalls]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserInPhoneCalls_dbo.PhoneCalls_PhoneCallId] FOREIGN KEY([PhoneCallId])
REFERENCES [dbo].[PhoneCalls] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserInPhoneCalls] CHECK CONSTRAINT [FK_dbo.UserInPhoneCalls_dbo.PhoneCalls_PhoneCallId]
GO

ALTER TABLE [dbo].[UserInPhoneCalls]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserInPhoneCalls_dbo.Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserInPhoneCalls] CHECK CONSTRAINT [FK_dbo.UserInPhoneCalls_dbo.Users_UserId]
GO