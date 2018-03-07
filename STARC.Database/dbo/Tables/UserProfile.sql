CREATE TABLE [dbo].[UserProfile] (
    [UserProfileId] INT           IDENTITY (1, 1) NOT NULL,
    [ProfileName]   VARCHAR (100) NULL,
    CONSTRAINT [pkUserProfile] PRIMARY KEY CLUSTERED ([UserProfileId] ASC)
);

