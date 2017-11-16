-- NOTE: DUMMY is replaced by the site name, e.g. 'so' or 'meta'
 
DROP SCHEMA IF EXISTS DUMMY ;
CREATE SCHEMA IF NOT EXISTS DUMMY DEFAULT CHARACTER SET latin1 ;
USE DUMMY;

--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
--  Table DUMMY.`votetypes`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`votetypes` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`votetypes` (
  `Id` INT(11) NOT NULL /* IDENTITY */,
  `Name` VARCHAR(40) CHARACTER SET 'utf8' NOT NULL 
    ,  PRIMARY KEY (`Id`) 
  )
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;


--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
--  Table DUMMY.`posttags`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`posttags` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`posttags` (
  `PostId` INT(11) NOT NULL /* IDENTITY */,
  `Tag` VARCHAR(50) CHARACTER SET 'utf8' NOT NULL 
    ,  PRIMARY KEY (`PostId`, `Tag`) 
  )
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;


--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
--  Table DUMMY.`posttypes`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`posttypes` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`posttypes` (
  `Id` INT(11) NOT NULL /* IDENTITY */,
  `Type` VARCHAR(10) CHARACTER SET 'utf8' NOT NULL 
    ,  PRIMARY KEY (`Id`) 
  )
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;


--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
--  Table DUMMY.`posthistorytypes`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`posthistorytypes` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`posthistorytypes` (
  `Id` INT(11) NOT NULL /* IDENTITY */,
  `Type` VARCHAR(50) CHARACTER SET 'utf8' NOT NULL,
  PRIMARY KEY (`Id`)
)
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;



INSERT DUMMY.`VoteTypes` (`Id`, `Name`) VALUES(1, 'AcceptedByOriginator');
INSERT DUMMY.`VoteTypes` (`Id`, `Name`) VALUES(2, 'UpMod');
INSERT DUMMY.`VoteTypes` (`Id`, `Name`) VALUES(3, 'DownMod');
INSERT DUMMY.`VoteTypes` (`Id`, `Name`) VALUES(4, 'Offensive');
INSERT DUMMY.`VoteTypes` (`Id`, `Name`) VALUES(5, 'Favorite');
INSERT DUMMY.`VoteTypes` (`Id`, `Name`) VALUES(6, 'Close');
INSERT DUMMY.`VoteTypes` (`Id`, `Name`) VALUES(7, 'Reopen');
INSERT DUMMY.`VoteTypes` (`Id`, `Name`) VALUES(8, 'BountyStart');
INSERT DUMMY.`VoteTypes` (`Id`, `Name`) VALUES(9, 'BountyClose');
INSERT DUMMY.`VoteTypes` (`Id`, `Name`) VALUES(10,'Deletion');
INSERT DUMMY.`VoteTypes` (`Id`, `Name`) VALUES(11,'Undeletion');
INSERT DUMMY.`VoteTypes` (`Id`, `Name`) VALUES(12,'Spam');
INSERT DUMMY.`VoteTypes` (`Id`, `Name`) VALUES(13,'InformModerator');
INSERT DUMMY.`PostTypes` (`Id`, `Type`) VALUES(1, 'Question') ;
INSERT DUMMY.`PostTypes` (`Id`, `Type`) VALUES(2, 'Answer') ;
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(1, 'InitialTitle');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(2, 'InitialBody');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(3, 'InitialTags');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(4, 'EditTitle');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(5, 'EditBody');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(6, 'EditTags');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(7, 'RollbackTitle');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(8, 'RollbackBody');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(9, 'RollbackTags');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(10, 'PostClosed');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(11, 'PostReopened');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(12, 'PostDeleted');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(13, 'PostUndeleted');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(14, 'PostLocked');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(15, 'PostUnlocked');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(16, 'CommunityOwned');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(17, 'PostMigrated');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(18, 'QuestionMerged');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(19, 'QuestionProtected');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(20, 'QuestionUnprotected');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(21, 'PostDisassociated');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(22, 'QuestionUnmerged');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(23, 'UnknownDevRelatedEvent');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(24, 'SuggestedEditApplied');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(25, 'PostTweeted');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(26, 'VoteNullificationByDev');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(27, 'PostUnmigrated');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(28, 'UnknownSuggestionEvent');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(29, 'UnknownModeratorEvent');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(30, 'UnknownEvent');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(31, 'CommentDiscussionMovedToChat');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(33, 'PostNoticeAdded');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(34, 'PostNoticeRemoved');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(35, 'PostMigratedAway');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(36, 'PostMigratedHere');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(37, 'PostMergeSource');
INSERT DUMMY.`PostHistoryTypes` (`Id`, `Type`) VALUES(38, 'PostMergeDestination');

--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
--  Table DUMMY.`badges`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`badges` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`badges` (
  `Id` INT(11) NOT NULL /* IDENTITY */,
  `Name` VARCHAR(40) CHARACTER SET 'utf8' NOT NULL ,
  `UserId` INT(11) NOT NULL ,
  `Date` DATETIME NOT NULL 
    ,  PRIMARY KEY (`Id`) 
  )
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;

--  INDICES CREATE INDEX `IX_Badges_Id_UserId` ON DUMMY.`badges` (`Id` ASC, `UserId` ASC) ;


--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
--  Table DUMMY.`comments`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`comments` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`comments` (
  `Id` INT(11) NOT NULL /* IDENTITY */,
  `CreationDate` DATETIME NOT NULL ,
  `PostId` INT(11) NOT NULL ,
  `Score` INT(11) NULL DEFAULT NULL ,
  `Text` TEXT CHARACTER SET 'utf8' NOT NULL ,
  `UserId` INT(11) NULL DEFAULT NULL 
    ,  PRIMARY KEY (`Id`) 
  )
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;

-- INDICES CREATE INDEX `IX_Comments_Id_PostId` ON DUMMY.`comments` (`Id` ASC, `PostId` ASC) ;
-- INDICES CREATE INDEX `IX_Comments_Id_UserId` ON DUMMY.`comments` (`Id` ASC, `UserId` ASC) ;

--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
--  Table DUMMY.`posthistory`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`posthistory` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`posthistory` (
  `Id` INT(11) NOT NULL /* IDENTITY */,
  `PostHistoryTypeId` INT(11) NOT NULL,
  `PostId` INT(11) NOT NULL,
  `RevisionGUID` CHAR(36) NOT NULL,
  `CreationDate` DATETIME NOT NULL,
  `UserId` INT NULL,
  `UserDisplayName` VARCHAR(40) CHARACTER SET 'utf8' NULL,
  `Comment` TEXT CHARACTER SET 'utf8' NULL,
  `Text` TEXT CHARACTER SET 'utf8' NULL,
  PRIMARY KEY (`Id`)
)
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;

--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
--  Table DUMMY.`postlinks`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`postlinks` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`postlinks` (
  `Id` INT(11) NOT NULL /* IDENTITY */,
  `CreationDate` DATETIME NOT NULL,
  `PostId` INT(11) NOT NULL,
  `RelatedPostId` INT(11) NOT NULL,
  `LinkTypeId` INT(11) NOT NULL,
  PRIMARY KEY (`Id`)
  )
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;

--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
--  Table DUMMY.`posts`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`posts` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`posts` (
  `Id` INT(11) NOT NULL /* IDENTITY */,
  `AcceptedAnswerId` INT(11) NULL DEFAULT NULL ,
  `AnswerCount` INT(11) NULL DEFAULT NULL ,
  `Body` LONGTEXT CHARACTER SET 'utf8' NOT NULL ,
  `ClosedDate` DATETIME NULL DEFAULT NULL ,
  `CommentCount` INT(11) NULL DEFAULT NULL ,
  `CommunityOwnedDate` DATETIME NULL DEFAULT NULL ,
  `CreationDate` DATETIME NOT NULL ,
  `FavoriteCount` INT(11) NULL DEFAULT NULL ,
  `LastActivityDate` DATETIME NOT NULL ,
  `LastEditDate` DATETIME NULL DEFAULT NULL ,
  `LastEditorDisplayName` VARCHAR(40) CHARACTER SET 'utf8' NULL DEFAULT NULL ,
  `LastEditorUserId` INT(11) NULL DEFAULT NULL ,
  `OwnerUserId` INT(11) NULL DEFAULT NULL ,
  `ParentId` INT(11) NULL DEFAULT NULL ,
  `PostTypeId` INT(11) NOT NULL ,
  `Score` INT(11) NOT NULL ,
  `Tags` VARCHAR(150) CHARACTER SET 'utf8' NULL DEFAULT NULL ,
  `Title` VARCHAR(250) CHARACTER SET 'utf8' NULL DEFAULT NULL ,
  `ViewCount` INT(11) NOT NULL 
    ,  PRIMARY KEY (`Id`) 
  )
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;

-- INDICES CREATE UNIQUE INDEX `IX_Posts_Id_AcceptedAnswerId` ON DUMMY.`posts` (`Id` ASC, `AcceptedAnswerId` ASC) ;
-- INDICES CREATE UNIQUE INDEX `IX_Posts_Id_OwnerUserId` ON DUMMY.`posts` (`Id` ASC, `OwnerUserId` ASC) ;
-- INDICES CREATE UNIQUE INDEX `IX_Posts_Id_ParentId` ON DUMMY.`posts` (`Id` ASC, `ParentId` ASC) ;
-- INDICES CREATE INDEX `IX_Posts_Id_PostTypeId` ON DUMMY.`posts` (`Id` ASC, `PostTypeId` ASC) ;
-- INDICES CREATE INDEX `IX_Posts_PostType` ON DUMMY.`posts` (`PostTypeId` ASC) ;

--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
--  Table DUMMY.`tags`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`tags` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`tags` (
  `Id` INT(11) NOT NULL /* IDENTITY */,
  `TagName` VARCHAR(150) CHARACTER SET 'utf8' NOT NULL,
  `Count` INT(11) NOT NULL,
  `ExcerptPostId` INT(11) NOT NULL,
  `WikiPostId` INT(11) NOT NULL,
  PRIMARY KEY (`Id`)
  )
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;

--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
--  Table DUMMY.`users`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`users` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`users` (
  `Id` INT(11) NOT NULL /* IDENTITY */,
  `AboutMe` TEXT CHARACTER SET 'utf8' NULL DEFAULT NULL ,
  `Age` INT(11) NULL DEFAULT NULL ,
  `CreationDate` DATETIME NOT NULL ,
  `DisplayName` VARCHAR(40) CHARACTER SET 'utf8' NOT NULL ,
  `DownVotes` INT(11) NOT NULL ,
  `EmailHash` VARCHAR(40) CHARACTER SET 'utf8' NULL DEFAULT NULL ,
  `LastAccessDate` DATETIME NOT NULL ,
  `Location` VARCHAR(100) CHARACTER SET 'utf8' NULL DEFAULT NULL ,
  `Reputation` INT(11) NOT NULL ,
  `UpVotes` INT(11) NOT NULL ,
  `Views` INT(11) NOT NULL ,
  `WebsiteUrl` VARCHAR(200) CHARACTER SET 'utf8' NULL DEFAULT NULL 
    ,  PRIMARY KEY (`Id`) 
  )
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;

-- INDICES CREATE INDEX `IX_Users_DisplayName` ON DUMMY.`users` (`DisplayName` ASC) ;


--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
--  Table DUMMY.`votes`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`votes` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`votes` (
  `Id` INT(11) NOT NULL /* IDENTITY */,
  `PostId` INT(11) NOT NULL ,
  `UserId` INT(11) NULL DEFAULT NULL ,
  `BountyAmount` INT(11) NULL DEFAULT NULL ,
  `VoteTypeId` INT(11) NOT NULL ,
  `CreationDate` DATETIME NOT NULL 
    ,  PRIMARY KEY (`Id`) 
  )
ENGINE = MyISAM
DEFAULT CHARACTER SET = latin1;

-- INDICES CREATE INDEX `IX_Votes_Id_PostId` ON DUMMY.`votes` (`Id` ASC, `PostId` ASC) ;
-- INDICES CREATE INDEX `IX_Votes_VoteTypeId` ON DUMMY.`votes` (`VoteTypeId` ASC) ;



