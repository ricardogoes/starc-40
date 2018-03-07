CREATE TABLE [dbo].[Project] (
    [ProjectId]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]            VARCHAR (200)  NOT NULL,
    [Description]     VARCHAR (1000) NOT NULL,
    [StartDate]       DATETIME       NULL,
    [FinishDate]      DATETIME       NULL,
    [Status]          BIT            NOT NULL,
    [OwnerId]         BIGINT         NULL,
    [CustomerId]      BIGINT         NOT NULL,
    [CreatedBy]       BIGINT         NOT NULL,
    [CreatedDate]     DATETIME       NOT NULL,
    [LastUpdatedBy]   BIGINT         NOT NULL,
    [LastUpdatedDate] DATETIME       NOT NULL,
    CONSTRAINT [pkProject] PRIMARY KEY CLUSTERED ([ProjectId] ASC),
    CONSTRAINT [fkProjectCreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [fkProjectCustomer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]),
    CONSTRAINT [fkProjectLastUpdatedBy] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [fkProjectResponsable] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[User] ([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_CustomerId]
    ON [dbo].[Project]([CustomerId] ASC);

