-- this script is used to create the final files

PRAGMA synchronous=OFF;
PRAGMA count_changes = FALSE;
PRAGMA journal_mode = OFF;
PRAGMA locking_mode = NORMAL;
PRAGMA page_size = 32768;
PRAGMA temp_store = MEMORY;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE [PostTypes] (
    [Id] INTEGER PRIMARY KEY /* IDENTITY */ NOT NULL,
    [Type] nvarchar(10) NOT NULL
);
CREATE TABLE [VoteTypes] (
    [Id] INTEGER PRIMARY KEY /* IDENTITY */ NOT NULL,
    [Name] varchar(40) NOT NULL
);
CREATE TABLE [PostTags] (
    [PostId] int NOT NULL,
    [Tag] nvarchar(50) NOT NULL
    ,CONSTRAINT [PK_PostTags] PRIMARY KEY ([PostId], [Tag]
    )
);


INSERT INTO VoteTypes (Id, Name) VALUES(1, 'AcceptedByOriginator');
INSERT INTO VoteTypes (Id, Name) VALUES(2, 'UpMod');
INSERT INTO VoteTypes (Id, Name) VALUES(3, 'DownMod');
INSERT INTO VoteTypes (Id, Name) VALUES(4, 'Offensive');
INSERT INTO VoteTypes (Id, Name) VALUES(5, 'Favorite');
INSERT INTO VoteTypes (Id, Name) VALUES(6, 'Close');
INSERT INTO VoteTypes (Id, Name) VALUES(7, 'Reopen');
INSERT INTO VoteTypes (Id, Name) VALUES(8, 'BountyStart');
INSERT INTO VoteTypes (Id, Name) VALUES(9, 'BountyClose');
INSERT INTO VoteTypes (Id, Name) VALUES(10,'Deletion');
INSERT INTO VoteTypes (Id, Name) VALUES(11,'Undeletion');
INSERT INTO VoteTypes (Id, Name) VALUES(12,'Spam');
INSERT INTO VoteTypes (Id, Name) VALUES(13,'InformModerator');
INSERT INTO PostTypes (Id, Type) VALUES(1, 'Question'); 
INSERT INTO PostTypes (Id, Type) VALUES(2, 'Answer'); 

CREATE TABLE [Votes] (
    [Id] INTEGER PRIMARY KEY /* IDENTITY */ NOT NULL,
    [PostId] int NOT NULL,
    [UserId] int,
    [BountyAmount] int,
    [VoteTypeId] int NOT NULL,
    [CreationDate] datetime NOT NULL
);
CREATE TABLE [Users] (
    [Id] INTEGER PRIMARY KEY /* IDENTITY */ NOT NULL,
    [AboutMe] nvarchar(2100),
    [Age] int,
    [CreationDate] datetime NOT NULL,
    [DisplayName] nvarchar(40) NOT NULL,
    [DownVotes] int NOT NULL,
    [EmailHash] nvarchar(40),
    [LastAccessDate] datetime NOT NULL,
    [Location] nvarchar(100),
    [Reputation] int NOT NULL,
    [UpVotes] int NOT NULL,
    [Views] int NOT NULL,
    [WebsiteUrl] nvarchar(200)
);
CREATE TABLE [Posts] (
    [Id] INTEGER PRIMARY KEY /* IDENTITY */ NOT NULL,
    [AcceptedAnswerId] int,
    [AnswerCount] int,
    [Body] ntext NOT NULL,
    [ClosedDate] datetime,
    [CommentCount] int,
    [CommunityOwnedDate] datetime,
    [CreationDate] datetime NOT NULL,
    [FavoriteCount] int,
    [LastActivityDate] datetime NOT NULL,
    [LastEditDate] datetime,
    [LastEditorDisplayName] nvarchar(40),
    [LastEditorUserId] int,
    [OwnerUserId] int,
    [ParentId] int,
    [PostTypeId] int NOT NULL,
    [Score] int NOT NULL,
    [Tags] nvarchar(150),
    [Title] nvarchar(250),
    [ViewCount] int NOT NULL
);
CREATE TABLE [Comments] (
    [Id] INTEGER PRIMARY KEY /* IDENTITY */ NOT NULL,
    [CreationDate] datetime NOT NULL,
    [PostId] int NOT NULL,
    [Score] int,
    [Text] nvarchar(700) NOT NULL,
    [UserId] int
);
CREATE TABLE [Badges] (
    [Id] INTEGER PRIMARY KEY /* IDENTITY */ NOT NULL,
    [Name] nvarchar(40) NOT NULL,
    [UserId] int NOT NULL,
    [Date] datetime NOT NULL
);

-- INDICES CREATE INDEX [IX_Votes_Id_PostId] ON [Votes] ([Id], [PostId]);
-- INDICES CREATE INDEX [IX_Votes_VoteTypeId] ON [Votes] ([VoteTypeId]);
-- INDICES CREATE INDEX [IX_Users_DisplayName] ON [Users] ([DisplayName]);
-- INDICES CREATE INDEX [IX_Posts_Id_AcceptedAnswerId] ON [Posts] ([Id], [AcceptedAnswerId]);
-- INDICES CREATE INDEX [IX_Posts_Id_OwnerUserId] ON [Posts] ([Id], [OwnerUserId]);
-- INDICES CREATE INDEX [IX_Posts_Id_ParentId] ON [Posts] ([Id], [ParentId]);
-- INDICES CREATE INDEX [IX_Posts_Id_PostTypeId] ON [Posts] ([Id], [PostTypeId]);
-- INDICES CREATE INDEX [IX_Posts_PostType] ON [Posts] ([PostTypeId]);
-- INDICES CREATE INDEX [IX_Comments_Id_PostId] ON [Comments] ([Id], [PostId]);
-- INDICES CREATE INDEX [IX_Comments_Id_UserId] ON [Comments] ([Id], [UserId]);
-- INDICES CREATE INDEX [IX_Badges_Id_UserId] ON [Badges] ([Id], [UserId]);
COMMIT;