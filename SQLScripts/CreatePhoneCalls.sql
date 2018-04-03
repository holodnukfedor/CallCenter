
CREATE TABLE [dbo].[PhoneCalls](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[ConnectionTime] [datetime] NULL,
	[TerminationTime] [datetime] NOT NULL,
	[ParentCallId] [int] NULL,
	[DurationSeconds] [int] NULL,
 CONSTRAINT [PK_dbo.PhoneCalls] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PhoneCalls]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PhoneCalls_dbo.PhoneCalls_ParentCallId] FOREIGN KEY([ParentCallId])
REFERENCES [dbo].[PhoneCalls] ([Id])
GO

ALTER TABLE [dbo].[PhoneCalls] CHECK CONSTRAINT [FK_dbo.PhoneCalls_dbo.PhoneCalls_ParentCallId]
GO