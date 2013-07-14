-- this script is used to create the staging files. No keys or indexes for speed.

PRAGMA FOREIGN_KEYS=OFF;
PRAGMA JOURNAL_MODE = OFF;
pragma count_changes=0; pragma synchronous=0;pragma journal_mode = 0;pragma locking_mode = normal;pragma page_size = 32768;pragma temp_store = memory;pragma foreign_keys=0;
BEGIN TRANSACTION;
DROP TABLE IF EXISTS [Badges_tmp] ;
CREATE TABLE [Badges_tmp] (
    [Id] int NOT NULL,
    [Name] nvarchar(40) NOT NULL,
    [UserId] int NOT NULL,
    [Date] datetime NOT NULL
);
DROP TABLE IF EXISTS [Comments_tmp] ;
CREATE TABLE [Comments_tmp] (
    [Id] int NOT NULL,
    [CreationDate] datetime NOT NULL,
    [PostId] int NOT NULL,
    [Score] int,
    [Text] nvarchar(700) NOT NULL,
    [UserId] int
);

DROP TABLE IF EXISTS [Posts_tmp];
CREATE TABLE [Posts_tmp] (
    [Id] int NOT NULL,
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
DROP TABLE IF EXISTS [PostTypes_tmp] ;
CREATE TABLE [PostTypes_tmp] (
    [Id] int NOT NULL,
    [Type] nvarchar(10) NOT NULL
);
DROP TABLE IF EXISTS [Users_tmp] ;
CREATE TABLE [Users_tmp] (
    [Id] int NOT NULL,
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
DROP TABLE IF EXISTS [Votes_tmp]  ;
CREATE TABLE [Votes_tmp] (
    [Id] int NOT NULL,
    [PostId] int NOT NULL,
    [UserId] int,
    [BountyAmount] int,
    [VoteTypeId] int NOT NULL,
    [CreationDate] datetime NOT NULL
);
DROP TABLE IF EXISTS [VoteTypes_tmp];
CREATE TABLE [VoteTypes_tmp] (
    [Id] int NOT NULL,
    [Name] varchar(40) NOT NULL
);

DROP TABLE IF EXISTS [PostTags_tmp];
CREATE TABLE [PostTags_tmp] (
    [PostId] int NOT NULL,
    [Tag] nvarchar(50) NOT NULL);
COMMIT;