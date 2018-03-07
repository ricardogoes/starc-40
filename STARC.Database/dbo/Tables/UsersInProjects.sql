CREATE TABLE [dbo].[UsersInProjects] (
    [UserInProjectId] BIGINT   IDENTITY (1, 1) NOT NULL,
    [UserId]          BIGINT   NOT NULL,
    [ProjectId]       BIGINT   NOT NULL,
    [CreatedBy]       BIGINT   NULL,
    [CreatedDate]     DATETIME NOT NULL,
    CONSTRAINT [pkUsersInProjects] PRIMARY KEY CLUSTERED ([UserInProjectId] ASC),
    CONSTRAINT [fkUsersInProjectsCreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [fkUsersInProjectsProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([ProjectId]),
    CONSTRAINT [fkUsersInProjectsUserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [uniqueUsersInProjectsUserIdProjectId] UNIQUE NONCLUSTERED ([UserId] ASC, [ProjectId] ASC)
);

