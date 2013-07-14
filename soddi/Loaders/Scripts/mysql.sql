-- NOTE: DUMMY is replaced by the site name, e.g. 'so' or 'meta'
 
DROP SCHEMA IF EXISTS DUMMY ;
CREATE SCHEMA IF NOT EXISTS DUMMY DEFAULT CHARACTER SET latin1 ;
USE DUMMY;

--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
--  Table DUMMY.`votetypes`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`votetypes` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`votetypes` (
  `Id` INT(11) NOT NULL ,
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
  `PostId` INT(11) NOT NULL ,
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
  `Id` INT(11) NOT NULL ,
  `Type` VARCHAR(10) CHARACTER SET 'utf8' NOT NULL 
    ,  PRIMARY KEY (`Id`) 
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

--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
--  Table DUMMY.`badges`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`badges` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`badges` (
  `Id` INT(11) NOT NULL ,
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
  `Id` INT(11) NOT NULL ,
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
--  Table DUMMY.`posts`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`posts` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`posts` (
  `Id` INT(11) NOT NULL ,
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
--  Table DUMMY.`users`
--  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -
DROP TABLE IF EXISTS DUMMY.`users` ;

CREATE  TABLE IF NOT EXISTS DUMMY.`users` (
  `Id` INT(11) NOT NULL ,
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
  `Id` INT(11) NOT NULL ,
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



