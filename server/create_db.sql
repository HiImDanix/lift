USE LosingIsFunToo;

CREATE TABLE Accounts (
	ID int IDENTITY(0,1) NOT NULL,
	username varchar(16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	email varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	passwordHash varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT account_pk PRIMARY KEY (ID),
	CONSTRAINT account_username_unique UNIQUE (username)
);

CREATE TABLE Questions (
	ID int IDENTITY(0,1) NOT NULL,
	question varchar(128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	category varchar(32) NULL,
	imgPath varchar(256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	CONSTRAINT question_pk PRIMARY KEY (ID)
);

CREATE TABLE Answers (
	ID int IDENTITY(0,1) NOT NULL,
	answer varchar(64) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	questionID int NOT NULL,
	isCorrect bit NOT NULL,
	CONSTRAINT answer_pk PRIMARY KEY (ID),
	CONSTRAINT Answer_FK FOREIGN KEY (questionID) REFERENCES Questions(ID)
);

CREATE TABLE Rooms (
	ID int IDENTITY(0,1) NOT NULL,
	code varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	logoQuizGameID int NULL,
	CONSTRAINT Lobby_PK PRIMARY KEY (ID)
);

CREATE TABLE LogoQuizGames (
	ID int IDENTITY(0,1) NOT NULL,
	durationMs int NOT NULL,
	maxPlayers int NOT NULL,
	startTime datetime2(0) NULL,
	scoreboardID int NULL,
	CONSTRAINT NewTable_PK PRIMARY KEY (ID)
);

CREATE TABLE Players (
	ID int IDENTITY(0,1) NOT NULL,
	[session] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	displayName varchar(16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	accountID int NULL,
	roomID int NOT NULL,
	CONSTRAINT player_pk PRIMARY KEY (ID),
	CONSTRAINT player_session_unique UNIQUE ([session])
);

CREATE TABLE QuizGameAnswers (
	ID int IDENTITY(0,1) NOT NULL,
	playerID int NOT NULL,
	answerID int NOT NULL,
	pointsEarned int NOT NULL,
	CONSTRAINT QuizGameAnswer_PK PRIMARY KEY (ID)
);

CREATE TABLE RoundQuestions (
	ID int IDENTITY(0,1) NOT NULL,
	questionID int NOT NULL,
	logoQuizGameID int NOT NULL,
	CONSTRAINT RoundQuestions_PK PRIMARY KEY (ID)
);

CREATE TABLE Scoreboards (
	ID int IDENTITY(0,1) NOT NULL,
	score int NOT NULL,
	playerID int NOT NULL,
	CONSTRAINT Scoreboard_PK PRIMARY KEY (ID)
);


-- LosingIsFunToo.dbo.Rooms foreign keys

ALTER TABLE LosingIsFunToo.dbo.Rooms ADD CONSTRAINT Lobby_FK_1 FOREIGN KEY (logoQuizGameID) REFERENCES LogoQuizGames(ID);


-- LosingIsFunToo.dbo.LogoQuizGame foreign keys

ALTER TABLE LosingIsFunToo.dbo.LogoQuizGames ADD CONSTRAINT NewTable_FK FOREIGN KEY (scoreboardID) REFERENCES Scoreboards(ID);


-- LosingIsFunToo.dbo.Player foreign keys

ALTER TABLE LosingIsFunToo.dbo.Players ADD CONSTRAINT Player_FK FOREIGN KEY (accountID) REFERENCES Accounts(ID);
ALTER TABLE LosingIsFunToo.dbo.Players ADD CONSTRAINT lobby_Fk FOREIGN KEY (roomID) REFERENCES Rooms(ID);


-- LosingIsFunToo.dbo.QuizGameAnswer foreign keys

ALTER TABLE LosingIsFunToo.dbo.QuizGameAnswers ADD CONSTRAINT QuizGameAnswer_FK FOREIGN KEY (playerID) REFERENCES Players(ID);
ALTER TABLE LosingIsFunToo.dbo.QuizGameAnswers ADD CONSTRAINT QuizGameAnswer_FK_1 FOREIGN KEY (answerID) REFERENCES Answers(ID);


-- LosingIsFunToo.dbo.RoundQuestions foreign keys

ALTER TABLE LosingIsFunToo.dbo.RoundQuestions ADD CONSTRAINT RoundQuestions_FK FOREIGN KEY (logoQuizGameID) REFERENCES LogoQuizGames(ID);
ALTER TABLE LosingIsFunToo.dbo.RoundQuestions ADD CONSTRAINT RoundQuestions_FK_1 FOREIGN KEY (questionID) REFERENCES Questions(ID);


-- LosingIsFunToo.dbo.Scoreboard foreign keys

ALTER TABLE LosingIsFunToo.dbo.Scoreboards ADD CONSTRAINT Scoreboard_FK FOREIGN KEY (playerID) REFERENCES Players(ID);