-- NOTE: DUMMY is replaced by the name of the site

IF (OBJECT_ID('DUMMY.FK_Posts_PostTypeId__PostTypes_Id', 'F') IS NOT NULL)
  ALTER TABLE DUMMY.Posts DROP CONSTRAINT FK_Posts_PostTypeId__PostTypes_Id;
IF (OBJECT_ID('DUMMY.FK_Posts_ParentId__Posts_Id', 'F') IS NOT NULL)
  ALTER TABLE DUMMY.Posts DROP CONSTRAINT FK_Posts_ParentId__Posts_Id;
IF (OBJECT_ID('DUMMY.FK_Posts_OwnerUserId__Users_Id', 'F') IS NOT NULL)
  ALTER TABLE DUMMY.Posts DROP CONSTRAINT FK_Posts_OwnerUserId__Users_Id;
IF (OBJECT_ID('DUMMY.FK_Posts_AcceptedAnswerId__Posts_Id', 'F') IS NOT NULL)
  ALTER TABLE DUMMY.Posts DROP CONSTRAINT FK_Posts_AcceptedAnswerId__Posts_Id;
IF (OBJECT_ID('DUMMY.FK_Comments_PostId__Posts_Id', 'F') IS NOT NULL)
  ALTER TABLE DUMMY.Comments DROP CONSTRAINT FK_Comments_PostId__Posts_Id;
IF (OBJECT_ID('DUMMY.FK_Comments_UserId__Users_Id', 'F') IS NOT NULL)
  ALTER TABLE DUMMY.Comments DROP CONSTRAINT FK_Comments_UserId__Users_Id;
IF (OBJECT_ID('DUMMY.FK_PostLinks_PostId__Posts_Id', 'F') IS NOT NULL)
  ALTER TABLE DUMMY.PostLinks DROP CONSTRAINT FK_PostLinks_PostId__Posts_Id;
IF (OBJECT_ID('DUMMY.FK_PostLinks_RelatedPostId__Posts_Id', 'F') IS NOT NULL)
  ALTER TABLE DUMMY.PostLinks DROP CONSTRAINT FK_PostLinks_RelatedPostId__Posts_Id;
IF (OBJECT_ID('DUMMY.FK_PostLinks_LinkTypeId__LinkTypes_Id', 'F') IS NOT NULL)
  ALTER TABLE DUMMY.PostLinks DROP CONSTRAINT FK_PostLinks_LinkTypeId__LinkTypes_Id;
IF (OBJECT_ID('DUMMY.FK_PostTags_PostId__Posts_Id', 'F') IS NOT NULL)
  ALTER TABLE DUMMY.PostTags DROP CONSTRAINT FK_PostTags_PostId__Posts_Id;
IF (OBJECT_ID('DUMMY.FK_Votes_PostId__Posts_Id', 'F') IS NOT NULL)
  ALTER TABLE DUMMY.Votes DROP CONSTRAINT FK_Votes_PostId__Posts_Id;
IF (OBJECT_ID('DUMMY.FK_Votes_UserId__Users_Id', 'F') IS NOT NULL)
  ALTER TABLE DUMMY.Votes DROP CONSTRAINT FK_Votes_UserId__Users_Id;
IF (OBJECT_ID('DUMMY.FK_Votes_UserId__VoteTypes_Id', 'F') IS NOT NULL)
  ALTER TABLE DUMMY.Votes DROP CONSTRAINT FK_Votes_UserId__VoteTypes_Id;

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[Badges]') AND type in (N'U'))
  DROP TABLE DUMMY.[Badges];
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[Comments]') AND type in (N'U'))
  DROP TABLE DUMMY.[Comments];
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[Posts]') AND type in (N'U'))
  DROP TABLE DUMMY.[Posts];
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[PostTags]') AND type in (N'U'))
  DROP TABLE DUMMY.[PostTags];
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[PostTypes]') AND type in (N'U'))
  DROP TABLE DUMMY.[PostTypes];
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[Users]') AND type in (N'U'))
  DROP TABLE DUMMY.[Users];
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[Votes]') AND type in (N'U'))
  DROP TABLE DUMMY.[Votes];
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[VoteTypes]') AND type in (N'U'))
  DROP TABLE DUMMY.[VoteTypes];
IF EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'DUMMY.[PostLinks]') AND type IN (N'U'))
  DROP TABLE DUMMY.[PostLinks];
IF EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'DUMMY.[LinkTypes]') AND type IN (N'U'))
  DROP TABLE DUMMY.[LinkTypes];

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[Badges]') AND type in (N'U'))
DROP TABLE DUMMY.[Badges]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[Comments]') AND type in (N'U'))
DROP TABLE DUMMY.[Comments]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[Posts]') AND type in (N'U'))
DROP TABLE DUMMY.[Posts]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[PostTags]') AND type in (N'U'))
DROP TABLE DUMMY.[PostTags]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[PostTypes]') AND type in (N'U'))
DROP TABLE DUMMY.[PostTypes]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[Users]') AND type in (N'U'))
DROP TABLE DUMMY.[Users]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[Votes]') AND type in (N'U'))
DROP TABLE DUMMY.[Votes]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DUMMY.[VoteTypes]') AND type in (N'U'))
DROP TABLE DUMMY.[VoteTypes]
IF EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'DUMMY.[PostLinks]') AND type IN (N'U'))
DROP TABLE DUMMY.[PostLinks]
IF EXISTS (SELECT * FROM sys.objects WHERE OBJECT_ID = OBJECT_ID(N'DUMMY.[LinkTypes]') AND type IN (N'U'))
DROP TABLE DUMMY.[LinkTypes]

SET ansi_nulls  ON
SET quoted_identifier  ON
SET ansi_padding  ON

CREATE TABLE DUMMY.[LinkTypes] (
  Id INT NOT NULL,
  [Type] VARCHAR(50) NOT NULL,
  CONSTRAINT PK_LinkTypes__Id PRIMARY KEY CLUSTERED (Id ASC) 
);

CREATE TABLE DUMMY.[VoteTypes] (
  [Id]   [INT]    NOT NULL,
  [Name] [VARCHAR](50)    NOT NULL ,
  CONSTRAINT [PK_VoteType__Id] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]
  )ON [PRIMARY]

SET ansi_nulls  ON
SET quoted_identifier  ON

CREATE TABLE DUMMY.[PostTypes] (
  [Id]   [INT]    NOT NULL,
  [Type] [NVARCHAR](50)    NOT NULL
  , CONSTRAINT [PK_PostTypes__Id] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]
  ) ON [PRIMARY]

IF 0 = 1--SPLIT
  BEGIN
	SET ansi_nulls  ON
	SET quoted_identifier  ON

	CREATE TABLE DUMMY.[PostTags] (
	  [PostId] [INT]    NOT NULL,
	  [Tag]    [NVARCHAR](50)    NOT NULL
	  , CONSTRAINT [PK_PostTags__PostId_Tag] PRIMARY KEY CLUSTERED ( [PostId] ASC,[Tag] ASC ) ON [PRIMARY]
	  ) ON [PRIMARY]
  
  END

INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(1, N'AcceptedByOriginator')
INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(2, N'UpMod')
INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(3, N'DownMod')
INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(4, N'Offensive')
INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(5, N'Favorite')
INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(6, N'Close')
INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(7, N'Reopen')
INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(8, N'BountyStart')
INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(9, N'BountyClose')
INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(10,N'Deletion')
INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(11,N'Undeletion')
INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(12,N'Spam')
INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(13,N'InformModerator')
INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(15,N'ModeratorReview')
INSERT DUMMY.[VoteTypes] ([Id], [Name]) VALUES(16,N'ApproveEditSuggestion')
INSERT DUMMY.[PostTypes] ([Id], [Type]) VALUES(1, N'Question') 
INSERT DUMMY.[PostTypes] ([Id], [Type]) VALUES(2, N'Answer') 
INSERT DUMMY.[PostTypes] ([Id], [Type]) VALUES(3, N'Wiki') 
INSERT DUMMY.[PostTypes] ([Id], [Type]) VALUES(4, N'TagWikiExerpt') 
INSERT DUMMY.[PostTypes] ([Id], [Type]) VALUES(5, N'TagWiki') 
INSERT DUMMY.[PostTypes] ([Id], [Type]) VALUES(6, N'ModeratorNomination') 
INSERT DUMMY.[PostTypes] ([Id], [Type]) VALUES(7, N'WikiPlaceholder') 
INSERT DUMMY.[PostTypes] ([Id], [Type]) VALUES(8, N'PrivilegeWiki') 
INSERT DUMMY.[LinkTypes] ([Id], [Type]) VALUES(1, N'Linked')
INSERT DUMMY.[LinkTypes] ([Id], [Type]) VALUES(3, N'Duplicate')


IF 0 = 1--FULLTEXT
  BEGIN
	IF  EXISTS (SELECT * FROM sys.fulltext_indexes fti WHERE fti.object_id = OBJECT_ID(N'DUMMY.[Posts]'))
	ALTER FULLTEXT INDEX ON DUMMY.[Posts] DISABLE
	IF  EXISTS (SELECT * FROM sys.fulltext_indexes fti WHERE fti.object_id = OBJECT_ID(N'DUMMY.[Posts]'))
	DROP FULLTEXT INDEX ON DUMMY.[Posts]
	IF  EXISTS (SELECT * FROM sysfulltextcatalogs ftc WHERE ftc.name = N'PostFullText')
	DROP FULLTEXT CATALOG [PostFullText]
	CREATE FULLTEXT CATALOG [PostFullText]WITH ACCENT_SENSITIVITY = ON
	AUTHORIZATION dbo
  END



SET ansi_padding  OFF
SET ansi_nulls  ON
SET quoted_identifier  ON

CREATE TABLE DUMMY.[Votes] (
  [Id]           [INT]    NOT NULL,
  [PostId]       [INT]    NOT NULL,
  [UserId]       [INT]    NULL,
  [BountyAmount] [INT]    NULL,
  [VoteTypeId]   [INT]    NOT NULL,
  [CreationDate] [DATETIME]    NOT NULL
  , CONSTRAINT [PK_Votes__Id] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]
  ) ON [PRIMARY]

IF 0 = 1-- INDICES
  BEGIN
    CREATE NONCLUSTERED INDEX [IX_Votes__Id_PostId] ON DUMMY.[Votes] (
          [Id] ASC,
          [PostId] ASC)
    ON [PRIMARY];

    CREATE NONCLUSTERED INDEX [IX_Votes__VoteTypeId] ON DUMMY.[Votes] (
          [VoteTypeId] ASC)
    ON [PRIMARY];
  END

SET ansi_nulls  ON
SET quoted_identifier  ON

CREATE TABLE DUMMY.[Users] (
  [Id]             [INT]    NOT NULL,
  [AboutMe]        [NVARCHAR](MAX)    NULL,
  [Age]            [INT]    NULL,
  [CreationDate]   [DATETIME]    NOT NULL,
  [DisplayName]    [NVARCHAR](40)    NOT NULL,
  [DownVotes]      [INT]    NOT NULL,
  [EmailHash]      [NVARCHAR](40)    NULL,
  [LastAccessDate] [DATETIME]    NOT NULL,
  [Location]       [NVARCHAR](100)    NULL,
  [Reputation]     [INT]    NOT NULL,
  [UpVotes]        [INT]    NOT NULL,
  [Views]          [INT]    NOT NULL,
  [WebsiteUrl]     [NVARCHAR](200)    NULL,
  [AccountId]	   [INT] NULL
  , CONSTRAINT [PK_Users_Id] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]
  ) ON [PRIMARY]

IF 0 = 1-- INDICES
  BEGIN
    CREATE NONCLUSTERED INDEX [IX_Users__DisplayName] ON DUMMY.[Users] (
          [DisplayName] ASC)
    ON [PRIMARY]
  END


SET ansi_nulls  ON
SET quoted_identifier  ON

CREATE TABLE DUMMY.[Posts] (
  [Id]                    [INT]    NOT NULL,
  [AcceptedAnswerId]      [INT]    NULL,
  [AnswerCount]           [INT]    NULL,
  [Body]                  [NVARCHAR](MAX)    NOT NULL,
  [ClosedDate]            [DATETIME]    NULL,
  [CommentCount]          [INT]    NULL,
  [CommunityOwnedDate]    [DATETIME]    NULL,
  [CreationDate]          [DATETIME]    NOT NULL,
  [FavoriteCount]         [INT]    NULL,
  [LastActivityDate]      [DATETIME]    NOT NULL,
  [LastEditDate]          [DATETIME]    NULL,
  [LastEditorDisplayName] [NVARCHAR](40)    NULL,
  [LastEditorUserId]      [INT]    NULL,
  [OwnerUserId]           [INT]    NULL,
  [ParentId]              [INT]    NULL,
  [PostTypeId]            [INT]    NOT NULL,
  [Score]                 [INT]    NOT NULL,
  [Tags]                  [NVARCHAR](150)    NULL,
  [Title]                 [NVARCHAR](250)    NULL,
  [ViewCount]             [INT]    NOT NULL
  , CONSTRAINT [PK_Posts__Id] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]
  -- INDICES ,CONSTRAINT [IX_Posts_Id_AcceptedAnswerId] UNIQUE NONCLUSTERED ([Id] ASC,[AcceptedAnswerId] ASC ) ON [PRIMARY],
  -- INDICES CONSTRAINT [IX_Posts_Id_OwnerUserId] UNIQUE NONCLUSTERED ([Id] ASC,[OwnerUserId] ASC ) ON [PRIMARY],
  -- INDICES CONSTRAINT [IX_Posts_Id_ParentId] UNIQUE NONCLUSTERED ([Id] ASC,[ParentId] ASC ) ON [PRIMARY]
  )ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

IF 0 = 1-- INDICES
  BEGIN
    CREATE NONCLUSTERED INDEX [IX_Posts__Id_PostTypeId] ON DUMMY.[Posts] (
          [Id] ASC,
          [PostTypeId] ASC)
    ON [PRIMARY];

    CREATE NONCLUSTERED INDEX [IX_Posts__PostType] ON DUMMY.[Posts] (
          [PostTypeId] ASC)
    ON [PRIMARY];
  END

IF 0 = 1--FULLTEXT
  BEGIN
	EXEC dbo.Sp_fulltext_table
	  @tabname = N'DUMMY.[Posts]' ,
	  @action = N'create' ,
	  @keyname = N'PK_Posts__Id' ,
	  @ftcat = N'PostFullText'

	DECLARE  @lcid INT

	SELECT @lcid = lcid
	FROM   MASTER.dbo.syslanguages
	WHERE  alias = N'English'

	EXEC dbo.Sp_fulltext_column
	  @tabname = N'DUMMY.[Posts]' ,
	  @colname = N'Body' ,
	  @action = N'add' ,
	  @language = @lcid

	SELECT @lcid = lcid
	FROM   MASTER.dbo.syslanguages
	WHERE  alias = N'English'

	EXEC dbo.Sp_fulltext_column
	  @tabname = N'DUMMY.[Posts]' ,
	  @colname = N'Title' ,
	  @action = N'add' ,
	  @language = @lcid

	EXEC dbo.Sp_fulltext_table
	  @tabname = N'DUMMY.[Posts]' ,
	  @action = N'start_change_tracking'

	EXEC dbo.Sp_fulltext_table
	  @tabname = N'DUMMY.[Posts]' ,
	  @action = N'start_background_updateindex'

  END

SET ansi_nulls  ON
SET quoted_identifier  ON

CREATE TABLE DUMMY.[Comments] (
  [Id]           [INT]    NOT NULL,
  [CreationDate] [DATETIME]    NOT NULL,
  [PostId]       [INT]    NOT NULL,
  [Score]        [INT]    NULL,
  [Text]         [NVARCHAR](700)    NOT NULL,
  [UserId]       [INT]    NULL
  , CONSTRAINT [PK_Comments__Id] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]
  ) ON [PRIMARY]

IF 0 = 1-- INDICES
  BEGIN
    CREATE NONCLUSTERED INDEX [IX_Comments__Id_PostId] ON DUMMY.[Comments] (
          [Id] ASC,
          [PostId] ASC)
    ON [PRIMARY];

    CREATE NONCLUSTERED INDEX [IX_Comments__Id_UserId] ON DUMMY.[Comments] (
          [Id] ASC,
          [UserId] ASC)
    ON [PRIMARY];
  END

SET ansi_nulls  ON
SET quoted_identifier  ON

CREATE TABLE DUMMY.[Badges] (
  [Id]     [INT]    NOT NULL,
  [Name]   [NVARCHAR](40)    NOT NULL,
  [UserId] [INT]    NOT NULL,
  [Date]   [DATETIME]    NOT NULL
  , CONSTRAINT [PK_Badges__Id] PRIMARY KEY CLUSTERED ( [Id] ASC ) ON [PRIMARY]
  ) ON [PRIMARY]

CREATE TABLE DUMMY.[PostLinks] (
  Id INT NOT NULL,
  CreationDate DATETIME NOT NULL,
  PostId INT NOT NULL,
  RelatedPostId INT NOT NULL,
  LinkTypeId INT NOT NULL,
  CONSTRAINT [PK_PostLinks__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
) 

IF 0 = 1-- INDICES
  BEGIN
    CREATE NONCLUSTERED INDEX [IX_Badges__Id_UserId] ON DUMMY.[Badges] (
          [Id] ASC,
          [UserId] ASC)
    ON [PRIMARY];
  END

IF 0 = 1-- INDICES
  BEGIN

    ALTER TABLE Posts
    ADD CONSTRAINT FK_Posts_PostTypeId__PostTypes_Id FOREIGN KEY (PostTypeId) REFERENCES PostTypes(Id)
    
    ALTER TABLE Posts
    ADD CONSTRAINT FK_Posts_ParentId__Posts_Id FOREIGN KEY (ParentId) REFERENCES Posts(Id)
    
    ALTER TABLE Posts
    ADD CONSTRAINT FK_Posts_OwnerUserId__Users_Id FOREIGN KEY (OwnerUserId) REFERENCES Users(Id)
    
    ALTER TABLE Posts
    ADD CONSTRAINT FK_Posts_AcceptedAnswerId__Posts_Id FOREIGN KEY (AcceptedAnswerId) REFERENCES Posts(Id)
    
    ALTER TABLE Comments
    ADD CONSTRAINT FK_Comments_PostId__Posts_Id FOREIGN KEY (PostId) REFERENCES Posts(Id)
    
    ALTER TABLE Comments
    ADD CONSTRAINT FK_Comments_UserId__Users_Id FOREIGN KEY (UserId) REFERENCES Users(Id)
    
    ALTER TABLE PostLinks
    ADD CONSTRAINT FK_PostLinks_PostId__Posts_Id FOREIGN KEY (PostId) REFERENCES Posts(Id)
    
    ALTER TABLE PostLinks
    ADD CONSTRAINT FK_PostLinks_RelatedPostId__Posts_Id FOREIGN KEY (RelatedPostId) REFERENCES Posts(Id)
    
    ALTER TABLE PostLinks
    ADD CONSTRAINT FK_PostLinks_LinkTypeId__LinkTypes_Id FOREIGN KEY (LinkTypeId) REFERENCES LinkTypes(Id)
 IF 0 = 1--SPLIT
  BEGIN
   
    ALTER TABLE PostTags
    ADD CONSTRAINT FK_PostTags_PostId__Posts_Id FOREIGN KEY (PostId) REFERENCES Posts(Id)

  END    
    ALTER TABLE Votes
    ADD CONSTRAINT FK_Votes_PostId__Posts_Id FOREIGN KEY (PostId) REFERENCES Posts(Id)
    
    ALTER TABLE Votes
    ADD CONSTRAINT FK_Votes_UserId__Users_Id FOREIGN KEY (UserId) REFERENCES Users(Id)
    
    ALTER TABLE Votes
    ADD CONSTRAINT FK_Votes_UserId__VoteTypes_Id FOREIGN KEY (VoteTypeId) REFERENCES VoteTypes(Id)

  END
  