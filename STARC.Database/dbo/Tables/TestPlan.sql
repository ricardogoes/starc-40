
CREATE TABLE [dbo].[TestPlan](
	[TestPlanId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Description] [varchar](1000) NOT NULL,
	[StartDate] [datetime] NULL,
	[FinishDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
	[OwnerId] [bigint] NULL,
	[ProjectId] [bigint] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastUpdatedBy] [bigint] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [pkTestPlan] PRIMARY KEY ([TestPlanId]),
 CONSTRAINT [fkTestPlanCreatedBy] FOREIGN KEY([CreatedBy]) REFERENCES [dbo].[User] ([UserId]),
 CONSTRAINT [fkTestPlanProject] FOREIGN KEY([ProjectId]) REFERENCES [dbo].[Project] ([ProjectId]),
 CONSTRAINT [fkTestPlanLastUpdatedBy] FOREIGN KEY([LastUpdatedBy]) REFERENCES [dbo].[User] ([UserId]),
 CONSTRAINT [fkTestPlanResponsable] FOREIGN KEY([OwnerId]) REFERENCES [dbo].[User] ([UserId])
) ON [PRIMARY]
GO
