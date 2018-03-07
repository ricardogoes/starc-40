CREATE TABLE [dbo].[User] (
    [UserId]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [FirstName]       VARCHAR (50)  NOT NULL,
    [LastName]        VARCHAR (50)  NOT NULL,
    [UserName]        VARCHAR (50)  NOT NULL,
    [Password]        VARCHAR (50)  NOT NULL,
	[PasswordHash]    VARBINARY(MAX)  NOT NULL,
    [Email]           VARCHAR (100) NOT NULL,
    [Status]          BIT           NOT NULL,
    [UserProfileId]   INT           NOT NULL,
    [CreatedBy]       BIGINT        NULL,
    [CreatedDate]     DATETIME      NOT NULL,
    [LastUpdatedBy]   BIGINT        NULL,
    [LastUpdatedDate] DATETIME      NOT NULL,
    [CustomerId]      BIGINT        NULL,
    CONSTRAINT [pkUser] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [fkUserCreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [fkUserCustomer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]),
    CONSTRAINT [fkUserLastUpdatedBy] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [fkUserProfileId] FOREIGN KEY ([UserProfileId]) REFERENCES [dbo].[UserProfile] ([UserProfileId]),
    CONSTRAINT [uniqueUserName] UNIQUE NONCLUSTERED ([UserName] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Username]
    ON [dbo].[User]([UserName] ASC);

