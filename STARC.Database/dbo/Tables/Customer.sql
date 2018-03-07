CREATE TABLE [dbo].[Customer] (
    [CustomerId]      BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]            VARCHAR (200) NOT NULL,
    [DocumentId]      VARCHAR (20)  NOT NULL,
    [Address]         VARCHAR (200) NOT NULL,
    [Status]          BIT           NOT NULL,
    [CreatedBy]       BIGINT        NOT NULL,
    [CreatedDate]     DATETIME      NOT NULL,
    [LastUpdatedBy]   BIGINT        NOT NULL,
    [LastUpdatedDate] DATETIME      NOT NULL,
    CONSTRAINT [pkCustomer] PRIMARY KEY CLUSTERED ([CustomerId] ASC),
    CONSTRAINT [fkCustomerCreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [fkCustomerLastUpdatedBy] FOREIGN KEY ([LastUpdatedBy]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [uniqueCustomerDocumentId] UNIQUE NONCLUSTERED ([DocumentId] ASC)
);

